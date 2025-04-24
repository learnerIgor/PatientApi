using FluentValidation;
using Patient.Application.ValidatorsExtensions;


namespace Patient.Application.Handlers.Commands.DeletePatient
{
    public class DeletePatientCommandValidator:AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientCommandValidator() 
        {
            RuleFor(i => i.Id).NotEmpty().IsGuid();
        }
    }
}
