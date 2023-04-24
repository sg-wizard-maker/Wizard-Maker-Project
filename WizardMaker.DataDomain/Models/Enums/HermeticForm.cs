using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum HermeticForm
{
    // Living Things
    Animal,
    Herbam,
    Corpus,
    Mentem,

    // Classical Elements
    Aquam,
    Auram,
    Ignem,
    Terram,

    // Immaterial Things
    Imaginem,
    Vim,

    // Abbreviations (the JSON from FoundryVTT encodes using the 2-letter codes)
    An = Animal,
    He = Herbam,
    Co = Corpus,
    Me = Mentem,

    Aq = Aquam,
    Au = Auram,
    Ig = Ignem,
    Te = Terram,

    Im = Imaginem,
    Vi = Vim,
}
