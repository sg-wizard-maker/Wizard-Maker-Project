using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum HermeticTechnique
{
    // Hermetic Techniques
    Creo,
    Perdo,
    Muto,
    Rego,
    Intellego,

    // Abbreviations (the JSON from FoundryVTT encodes using the 2-letter codes)
    Cr = Creo,
    Re = Rego,
    Pe = Perdo,
    Mu = Muto,
    In = Intellego,
}
