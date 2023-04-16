﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WizardMaker.DataDomain.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SpellTarget
    {
        Ind,
        Circle = Ind,
        Part,
        Group,
        Room = Group,
        Structure,
        Bound,

        // intellego spells
        Taste = Ind,
        Touch = Part,
        Smell = Group,
        Hearing = Structure,
        Sight = Bound
    }
}