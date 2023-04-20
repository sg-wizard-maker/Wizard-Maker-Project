using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ObjType
{
    Ability,
    Virtue,
    Flaw,

    Spell,

    Armor,
    Weapon,
    // Text,

    // ...possible that other entities from ArM5 will belong here...
}
