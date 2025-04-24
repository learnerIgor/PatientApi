using MediatR;
using Patient.Application.DTOs;

namespace Patient.Application.Handlers.Queries.GetPatientByBirthDate
{
    public class GetPatientByBirthDateQuery : IRequest<BaseListDto<PatientByBirthDateDto>>
    {
        public string BirthDate { get; set; } = default!;
    }
}
