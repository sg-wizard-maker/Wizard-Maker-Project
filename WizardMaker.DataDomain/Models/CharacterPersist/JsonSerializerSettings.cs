using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.CharacterPersist
{
    public static class WMJsonSerializerSettings
    {
        public static JsonSerializerSettings CommonJsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            MaxDepth         = 128
        };
    }
}
