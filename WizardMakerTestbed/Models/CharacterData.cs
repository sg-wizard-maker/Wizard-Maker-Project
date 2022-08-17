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

        public List<AbilityInstanceData> abilities { get; set; }

        public CharacterData(string name, string description, List<AbilityInstanceData> abilities)
        {
            Name = name;
            Description = description;
            this.abilities = abilities;
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

        public string Id { get; }

        public AbilityInstanceData(string category, string type, string typeAbbrev, string name, int xP, int score, string specialty, string id)
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
    }
}
