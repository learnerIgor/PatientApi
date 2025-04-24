using FluentValidation;

namespace Patient.Application.Handlers.Commands.CreatePatient
{
    public class CreatePatientCommandValidator: AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator() 
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
