using MediatR;
using Patient.Application.DTOs;
using Patient.Domain.Enums;

namespace Patient.Application.Handlers.Commands.CreatePatient
{
    public class CreatePatientCommand : IRequest<PatientDto>
    {
        public Gender? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public NameDto Name { get; set; } = default!;
    }
}
