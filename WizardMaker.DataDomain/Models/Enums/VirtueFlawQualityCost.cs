using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VirtueFlawQualityCost
    {
        Free    = 0,
        Special = Free,
        Minor   = 1,
        Major   = 3,
    }
}
