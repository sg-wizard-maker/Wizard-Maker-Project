using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Category
    {
        General,
        Social,
        Hermetic,
        Supernatural,
        Martial,
        Academic,
        Arcane,
        Personality,
        Story,
        Special,
        Other
    }
}
