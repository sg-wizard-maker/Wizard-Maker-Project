using Newtonsoft.Json;
using System;
using WizardMakerPrototype.Models;

namespace WizardMakerPrototype.Models
{
    // TODO: Give it an ID field.  This way we can support searching and easier deletion.
    public abstract class Journalable: ICharacterCommand
    {
        // This indicates that the sorting should be by the SeasonYear
        //  For now, this is all journal entries.  Note that this also determines the 
        //   order of rendering.
        public const int SEASON_YEAR_SORT_ORDER = 100;

        public abstract string getText();

        public abstract SeasonYear getDate();

        public abstract string getId();

        public virtual string SerializeJson()
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;

            return JsonConvert.SerializeObject(this, Formatting.Indented, settings);
        }

        public static Journalable DeserializeJson(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                MaxDepth = 128
            };

            return (Journalable)JsonConvert.DeserializeObject(json, settings);
        }

        public abstract void Execute(Character character);
        

        public virtual void Undo()
        {
            throw new System.NotImplementedException();
        }

        public virtual Boolean IsSameSpecs(Journalable other)
        {
            if (other == null) return false;
            if (this.GetType() != other.GetType()) return false;
            if (this.getText() != other.getText()) return false;
            if (!this.getDate().Equals(other.getDate())) return false;

            //Note that we do not check ID, since this is just comparing equality across instances
            //  and each instance should have its own ID

            return true;
        }

        public virtual int sortOrder()
        {
            return SEASON_YEAR_SORT_ORDER;
        }
    }
}
