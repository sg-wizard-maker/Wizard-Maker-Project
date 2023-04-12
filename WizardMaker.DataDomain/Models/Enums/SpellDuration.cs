using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpellDuration
    {
        Moment,
        Conc,
        Diam = Conc,
        Sun,
        Ring = Sun,
        Moon,
        Year
    }
}
