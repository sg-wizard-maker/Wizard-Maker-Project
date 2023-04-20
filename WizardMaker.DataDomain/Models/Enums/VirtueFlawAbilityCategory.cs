using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums;

// Note:
// Hmmm...this is used for (Virtues, Flaws) but some of the values relate to Abilities...
// It might be more coherent to split this into two things,
//     one for (Virtues,Flaws,Qualities)
//     and the other for (Abilities)
[JsonConverter(typeof(StringEnumConverter))]
public enum VirtueFlawAbilityCategory
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
