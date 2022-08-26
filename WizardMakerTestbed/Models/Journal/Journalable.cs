using Newtonsoft.Json;
using System;
using WizardMakerPrototype.Models;

namespace WizardMakerTestbed.Models
{
    // TODO: Give it an ID field.  This way we can support searching and easier deletion.
    public abstract class Journalable: ICharacterCommand
    {
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
            if (this.getDate() != other.getDate()) return false;
            if (!this.getId().Equals(other.getId())) return false;

            return true;
        }
    }
}
