using AutoMapper;
using MediatR;
using Patient.Application.Abstractions.Persistence;
using Patient.Application.DTOs;

namespace Patient.Application.Handlers.Commands.CreatePatient
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDto>
    {
        private readonly IBaseRepository<Domain.Patient> _baseRepository;
        private readonly IMapper _mapper;

        public CreatePatientCommandHandler(IBaseRepository<Domain.Patient> baseRepository, IMapper mapper) 
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = _mapper.Map<Domain.Patient>(request);

            await _baseRepository.AddAsync(patient, cancellationToken);

            return _mapper.Map<PatientDto>(patient);
        }
    }
}
