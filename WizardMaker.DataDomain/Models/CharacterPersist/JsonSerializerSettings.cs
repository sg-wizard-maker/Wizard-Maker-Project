using Newtonsoft.Json;

namespace WizardMaker.DataDomain.Models.CharacterPersist;

public static class WMJsonSerializerSettings
{
    public static JsonSerializerSettings CommonJsonSerializerSettings = new JsonSerializerSettings()
    {
        TypeNameHandling = TypeNameHandling.All,
        MaxDepth         = 128
    };
}
