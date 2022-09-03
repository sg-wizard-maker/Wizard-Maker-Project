using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models
{
    /**
     * This class is meant as a way to have a simple representation of the character for use by front ends.  The Character class is used solely
     *  by the backend.  This class allows us to encapsulate backend data model changes from the front end.  The cost is typically that there is 
     *  more code to handle conversions.
     */
    public class CharacterData
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<AbilityInstanceData> Abilities { get; set; }

        public CharacterData(string name, string description, List<AbilityInstanceData> abilities)
        {
            Name = name;
            Description = description;
            this.Abilities = abilities;
        }

        public bool IsSameSpec(CharacterData other)
        {
            if (other == null) return false;
            if (this.GetType() != other.GetType()) return false;
            if (this.Name != other.Name) return false;
            if (this.Description != other.Description) return false;
            if (this.Abilities.Count != other.Abilities.Count) return false;
            for (int i = 0; i < this.Abilities.Count; i++)
            {
                if (!this.Abilities[i].IsSameSpec(other.Abilities[i])) return false;
            }

            return true;
        }
    }

    public class AbilityInstanceData
    {
        public string Category { get; }

        public string Type { get; }

        public string TypeAbbrev { get; }

        public string Name { get; }
        public int XP { get; }
        public int Score { get; }
        public string Specialty { get; }

        public List<string> Id { get; private set; }

        public AbilityInstanceData(string category, string type, string typeAbbrev, string name, int xP, int score, string specialty, List<string> id)
        {
            Category = category;
            Type = type;
            TypeAbbrev = typeAbbrev;
            Name = name;
            XP = xP;
            Score = score;
            Specialty = specialty;
            Id = id;
        }

        // Note: This includes a check on ID
        public bool IsSameSpec(AbilityInstanceData other)
        {
            if (other == null) return false;
            if (this.GetType() != other.GetType()) return false;
            if (this.Name != other.Name) return false;
            if (this.Category != other.Category) return false;
            if (this.Type != other.Type) return false;
            if (this.TypeAbbrev != other.TypeAbbrev) return false;
            if (this.Score != other.Score) return false;
            if (this.Specialty != other.Specialty) return false;
            if (this.XP != other.XP) return false;
            if (this.Id != other.Id) return false;

            return true;

        }
    }
}
