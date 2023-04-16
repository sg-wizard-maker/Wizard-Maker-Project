﻿using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpellRange
    {
        Personal = 0,
        Touch = 1,
        Voice,
        Eye = Voice,
        Sight,
        AC
    }
}