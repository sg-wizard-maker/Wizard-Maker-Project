using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.CharacterPersist
{
    // This character is just basic metadata and journal entries and must always be serializable.
    //  This class if for loading and saving of characters.
    //  TODO: Do we need this class if we have CharacterData?  Could we drop this class and just make a custom serialization/deserialization scheme in Character?
    //  TODO: Make this class a private class to Character?
    public class RawCharacter
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int startingAge { get; set; }

        public List<Journalable> journalEntries { get; }
        public string serializeJson()
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;

            return JsonConvert.SerializeObject(this, Formatting.Indented, settings);
        }

        public static RawCharacter deserializeJson(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                MaxDepth = 128
            };

            return (RawCharacter) JsonConvert.DeserializeObject(json, settings);
        }

        [JsonConstructor]
        public RawCharacter(string name, string description, int startingAge, List<Journalable> journalEntries)
        {
            Name = name;
            Description = description;
            this.startingAge = startingAge;
            this.journalEntries = journalEntries;
        }

        public RawCharacter(Character c)
        {
            Name = c.Name;
            Description = c.Description;
            startingAge = c.startingAge;
            journalEntries = c.GetJournal().ToList();
        }
    }
}
