using Patient.Domain.Enums;

namespace Patient.Application.DTOs
{
    public class PatientDto
    {
        public Gender? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public NameDto Name { get; set; } = default!;
    }
}
