using MediatR;
using Patient.Application.DTOs;

namespace Patient.Application.Handlers.Queries.GetPatient
{
    public class GetPatientQuery : IRequest<PatientDto>
    {
        public string PatientId { get; init; } = default!;
    }
}
