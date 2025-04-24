using FluentValidation;
using Patient.Application.ValidatorsExtensions;

namespace Patient.Application.Handlers.Queries.GetPatientByBirthDate
{
    public class GetPatientByBirthDateQueryValidator:AbstractValidator<GetPatientByBirthDateQuery>
    {
        public GetPatientByBirthDateQueryValidator()
        {
            RuleFor(r => r.BirthDate).IsDateTime();
        }
    }
}
