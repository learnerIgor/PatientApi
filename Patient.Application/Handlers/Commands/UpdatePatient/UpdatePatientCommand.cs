using MediatR;
using Patient.Application.DTOs;
using Patient.Domain.Enums;

namespace Patient.Application.Handlers.Commands.UpdatePatient
{
    public class UpdatePatientCommand : IRequest<PatientDto>
    {
        public string patientId { get; init; } = default!;
        public Gender? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public NameDto Name { get; set; } = default!;
    }
}
