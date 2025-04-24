using AutoMapper;
using MediatR;
using Patient.Application.Abstractions.Persistence;
using Patient.Application.DTOs;
using Patient.Application.Exceptions;
using Patient.Domain;

namespace Patient.Application.Handlers.Commands.UpdatePatient
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, PatientDto>
    {
        private readonly IBaseRepository<Domain.Patient> _repository;
        private readonly IBaseRepository<GivenName> _repositoryGivenNames;
        private readonly IMapper _mapper;
        public UpdatePatientCommandHandler(IBaseRepository<Domain.Patient> repository, IBaseRepository<GivenName> repositoryGivenNames, IMapper mapper) 
        {
            _repository = repository;
            _repositoryGivenNames = repositoryGivenNames;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var idPatient = Guid.Parse(request.patientId);
            var patient = await _repository.SingleOrDefaultAsync(p => p.Id == idPatient && p.Active, cancellationToken);
            if (patient is null)
            {
                throw new NotFoundException($"Patient with id = {request.patientId} not found");
            }

            // Обновляем свойства пациента
            patient.Gender = request.Gender.HasValue ? request.Gender.Value : null;
            patient.BirthDate = request.BirthDate;
            patient.Active = request.Active;

            // Обновляем Name
            patient.Name.Use = request.Name.Use;
            patient.Name.Family = request.Name.Family;

            // Удаление старых GivenNames
            var oldGivenNames = await _repositoryGivenNames.GetListAsync(expression: n => n.Names.Single().Id == patient.NameId);
            foreach (var givenName in oldGivenNames)
            {
                await _repositoryGivenNames.DeleteAsync(givenName);
            }

            // Добавление новых GivenNames из команды
            List<GivenName> newGivenNames = new List<GivenName>();
            if (request.Name.GivenNames != null)
            {
                foreach (var givenNameDto in request.Name.GivenNames)
                {
                    var givenName = new GivenName
                    {
                        Id = Guid.NewGuid(),
                        Value = givenNameDto.Value,
                        Names = new List<Name> { patient.Name } // Устанавливаем связь с текущим объектом Name
                    };
                    newGivenNames.Add(givenName);
                }
            }

            // Добавление новых GivenNames в базу данных
            foreach (var newGivenName in newGivenNames)
            {
                await _repositoryGivenNames.AddAsync(newGivenName);
            }

            // Сохранение изменений в базе данных
            await _repository.UpdateAsync(patient, cancellationToken);

            return _mapper.Map<PatientDto>(patient);
        }
    }
}
