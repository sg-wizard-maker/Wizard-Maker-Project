using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum EquipmentCost
{
    None,
    Standard,
    Inexpensive,
    Expensive,
}
