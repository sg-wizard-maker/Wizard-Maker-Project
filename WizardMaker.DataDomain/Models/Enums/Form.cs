using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Form
    {
        Ig,
        Co,
        He,
        An,
        Aq,
        Au,
        Im,
        Me,
        Te,
        Vi
    }
}
