using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Enums
{
    /// <summary>
    /// Includes all Hermetic spell/effect Ranges in the core ArM5 book.
    /// 
    /// This Enum-based scheme may persist for the sake of JSON (de)serialization, 
    /// but possibly a Class-based approach will be needful,
    /// as this would allow indicating (cost in magnitudes, who has access, requires Ritual, etc)
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HermeticSpellRanges
    {
        // TODO: Need to handle "non-standard" Ranges which can be created
        // TODO: Need to indicate that some of these require a Ritual
        // TODO: Need to indicate the cost in additional Magnitudes
        // TODO: Need to indicate which of these require Faerie Magic
        // 
        // These considerations may require using a Class rather than some Enums.
        Unknown = 0,

        Personal,
        Touch,
        Voice,
        Eye,
        Sight,
        Arcane,
        Road,  // Faerie Magic

        // Abbreviations (the JSON from FoundryVTT is encoded using these)
        AC = Arcane,  // Note: Did not see any with "AC"
    }

    // Useful for populating a dialog to choose a Range, without access to Faerie Magic
    public enum HermeticSpellRangesCore
    {
        Unknown = 0,

        Personal = HermeticSpellRanges.Personal,
        Touch    = HermeticSpellRanges.Touch,
        Voice    = HermeticSpellRanges.Voice,
        Eye      = HermeticSpellRanges.Eye,
        Sight    = HermeticSpellRanges.Sight,
        Arcane   = HermeticSpellRanges.Arcane,
    }

    public enum SpellRangesFaerieMagic
    {
        Unknown = 0,
        Road    = HermeticSpellRanges.Road,
    }
}
