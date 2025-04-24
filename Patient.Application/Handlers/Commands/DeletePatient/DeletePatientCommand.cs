using MediatR;

namespace Patient.Application.Handlers.Commands.DeletePatient
{
    public class DeletePatientCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
