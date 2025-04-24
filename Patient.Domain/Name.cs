namespace Patient.Domain
{
    public class Name
    {
        public Guid Id { get; set; }
        public string? Use { get; set; }
        public string Family { get; set; } = default!;
        public ICollection<GivenName>? GivenNames { get; set; } = [];
        public Patient? Patient { get; set; }
    }
}
