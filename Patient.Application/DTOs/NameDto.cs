namespace Patient.Application.DTOs
{
    public class NameDto
    {
        public string? Use { get; set; } = default!;
        public string Family { get; set; } = default!;
        public List<GivenNameDto>? GivenNames { get; set; } = new List<GivenNameDto>();
    }
}
