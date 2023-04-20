using Newtonsoft.Json;

namespace WizardMaker.DataDomain.Models.CharacterPersist
{
    // This character is just basic metadata and journal entries and must always be serializable.
    // This class if for loading and saving of characters.
    // TODO: Do we need this class if we have CharacterData?
    //       Could we drop this class and just make a custom serialization/deserialization scheme in Character?
    // TODO: Make this class private to Character?
    public class RawCharacter
    {
        #region Properties
        public string Name        { get; set; }
        public string Description { get; set; }
        public int    StartingAge { get; set; }

        public List<Journalable> JournalEntries { get; }
        #endregion

        #region Constructors
        [JsonConstructor]
        public RawCharacter(string name, string description, int startingAge, List<Journalable> journalEntries)
        {
            this.Name           = name;
            this.Description    = description;
            this.StartingAge    = startingAge;
            this.JournalEntries = journalEntries;
        }

        public RawCharacter(Character c)
        {
            this.Name           = c.Name;
            this.Description    = c.Description;
            this.StartingAge    = c.StartingAge;
            this.JournalEntries = c.GetJournal().ToList();
        }
        #endregion

        #region Methods (various)
        public string SerializeJson()
        {
            var settings = WMJsonSerializerSettings.CommonJsonSerializerSettings;
            var result   = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
            return result;
        }

        public static RawCharacter? DeserializeJson(string json)
        {
            var settings = WMJsonSerializerSettings.CommonJsonSerializerSettings;
            var result   = JsonConvert.DeserializeObject(json, settings) as RawCharacter;
            return result;
        }
        #endregion
    }
}
