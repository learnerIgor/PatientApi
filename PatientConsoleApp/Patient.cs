using PatientConsoleApp.Enums;

namespace PatientConsoleApp
{
    public class Patient
    {
        public Guid Id { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
        public Name Name { get; set; }
    }
}
