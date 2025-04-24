using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace PatientConsoleApp.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Gender
    {
        Male,
        Female,
        Other,
        Unknown
    }
}
