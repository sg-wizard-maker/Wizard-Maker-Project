using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HermeticSpellTargets
    {
        // TODO: Need to handle "non-standard" Ranges which can be created
        // TODO: Need to indicate that some of these require a Ritual
        // TODO: Need to indicate the cost in additional Magnitudes
        // TODO: Need to indicate which of these require Faerie Magic
        // TODO: May need to represent "Targets and Creo" (ArM5 113) which restricts some Creo effects to (Individual or Group)
        // 
        // These considerations may require using a Class rather than some Enums.
        Unknown = 0,

        Individual,
        Circle,
        Part,
        Group,
        Room,
        Structure,
        Boundary,

        // Sensory Targets, primarily for Intellego spells
        Taste,
        Touch,
        Smell,
        Hearing,
        Sight,

        Bloodline,  // Faerie Magic

        // Abbreviations (the JSON from FoundryVTT is encoded using these)
        Ind   = Individual,
        Bound = Boundary,
    }

    // Useful for populating a dialog to choose a Target, without access to Faerie Magic
    public enum HermeticSpellTargetCore
    {
        Unknown = 0,

        Individual = HermeticSpellTargets.Individual,
        Circle     = HermeticSpellTargets.Circle,
        Part       = HermeticSpellTargets.Part,
        Group      = HermeticSpellTargets.Group,
        Room       = HermeticSpellTargets.Room,
        Structure  = HermeticSpellTargets.Structure,
        Boundary   = HermeticSpellTargets.Boundary,
    }

    public enum HermeticSpellTargetFaerieMagic
    {
        Unknown   = 0,
        Bloodline = HermeticSpellTargets.Bloodline,

    }
}
