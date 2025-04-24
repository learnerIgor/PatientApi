using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace Patient.Domain.Enums
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
