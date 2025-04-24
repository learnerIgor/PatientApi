using FluentValidation;

namespace Patient.Application.Handlers.Commands.UpdatePatient
{
    public class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(command => command.BirthDate)
                .NotEmpty()
                .WithMessage("BirthDate is required.")
                .LessThan(DateTime.Now).WithMessage("BirthDate must be in the past.");
            RuleFor(name => name.Name.Family)
                .NotEmpty().WithMessage("Family is required.");
        }
    }
}
