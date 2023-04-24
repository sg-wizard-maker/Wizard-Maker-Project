using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Enums;

/// <summary>
/// Includes all Hermetic spell/effect Durations in the core ArM5 book.
/// 
/// This Enum-based scheme may persist for the sake of JSON (de)serialization, 
/// but possibly a Class-based approach will be needful,
/// as this would allow indicating (cost in magnitudes, who has access, requires Ritual, etc)
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum HermeticSpellDurations
{
    // TODO: Need to handle "non-standard" Ranges which can be created
    // TODO: Need to indicate that some of these require a Ritual
    // TODO: Need to indicate the cost in additional Magnitudes
    // TODO: Need to indicate which of these require Faerie Magic
    // 
    // These considerations may require using a Class rather than some Enums.
    Unknown = 0,

    Momentary,
    Concentration,
    Diameter,
    Sun,
    Ring,
    Moon,
    Year,

    Bargain,      // Faerie Magic
    Fire,         // Faerie Magic
    Until,        // Faerie Magic
    YearPlusOne,  // Faerie Magic

    // Abbreviations (the JSON from FoundryVTT is encoded using these)
    Moment = Momentary,
    Conc   = Concentration,
    Diam   = Diameter,
}

// Useful for populating a dialog to choose a Duration, without access to Faerie Magic
public enum HermeticSpellDurationsCore
{
    Unknown = 0,

    Momentary     = HermeticSpellDurations.Momentary,
    Concentration = HermeticSpellDurations.Concentration,
    Diameter      = HermeticSpellDurations.Diameter,
    Sun           = HermeticSpellDurations.Sun,
    Ring          = HermeticSpellDurations.Ring,
    Moon          = HermeticSpellDurations.Moon,
    Year          = HermeticSpellDurations.Year,
}

public enum HermeticSpellDurationsFaerieMagic
{
    Unknown     = 0,

    Bargain     = HermeticSpellDurations.Bargain,
    Fire        = HermeticSpellDurations.Fire,
    Until       = HermeticSpellDurations.Until,
    YearPlusOne = HermeticSpellDurations.YearPlusOne,
}
