using Patient.Application.DTOs;
using Patient.Domain.Enums;

namespace Booking.Application.Handlers.Booking.Commands.UpdateBooking
{
    public class UpdatePatientPayload
    {
        public Gender? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public NameDto Name { get; set; } = default!;
    }
}