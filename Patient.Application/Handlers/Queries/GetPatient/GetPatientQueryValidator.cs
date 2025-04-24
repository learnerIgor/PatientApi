using FluentValidation;
using Patient.Application.ValidatorsExtensions;
using Patient.Application.Handlers.Queries.GetPatient;

namespace Booking.Application.Handlers.Booking.Queries.GetBooking
{
    public class GetPatientQueryValidator : AbstractValidator<GetPatientQuery>
    {
        public GetPatientQueryValidator()
        {
            RuleFor(i => i.PatientId).NotEmpty().IsGuid();
        }
    }
}
