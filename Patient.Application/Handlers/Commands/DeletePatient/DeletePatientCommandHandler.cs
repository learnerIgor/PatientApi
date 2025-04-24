using MediatR;
using Patient.Application.Abstractions.Persistence;
using Patient.Application.Exceptions;

namespace Patient.Application.Handlers.Commands.DeletePatient
{
    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, Unit>
    {
        public IBaseRepository<Domain.Patient> _patients;

        public DeletePatientCommandHandler(IBaseRepository<Domain.Patient> patients)
        {
            _patients = patients;
        }

        public async Task<Unit> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var patientById = await _patients.SingleOrDefaultAsync(x => x.Id == idGuid,
            cancellationToken);
            if (patientById == null)
            {
                throw new NotFoundException($"Patient with id = {request.Id} not found");
            }

            patientById.Active = false; //не удаляем пациента, а делаем его неактивным

            await _patients.UpdateAsync(patientById, cancellationToken);

            return default;
        }
    }
}
