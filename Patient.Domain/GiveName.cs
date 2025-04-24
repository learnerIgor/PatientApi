namespace Patient.Domain
{
    public class GivenName
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = default!;
        public ICollection<Name> Names { get; set; } = [];
    }
}
