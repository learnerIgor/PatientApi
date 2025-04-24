namespace PatientConsoleApp
{
    public class Name
    {
        public Guid Id { get; set; }
        public string Use { get; set; }
        public string Family { get; set; }
        public List<GivenName> GivenNames { get; set; } = new List<GivenName>();
    }
}
