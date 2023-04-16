using System;

namespace WizardMaker.DataDomain.Models
{
    /**
     * This class is meant as a way to have a simple representation of the character for use by front ends.
     * The Character class is used solely by the backend.
     * This class allows us to encapsulate backend data model changes from the front end.
     * The cost is typically that there is more code to handle conversions.
     */
    public class CharacterData
    {
        public string Name        { get; set; }
        public string Description { get; set; }

        public List<AbilityInstanceData> Abilities { get; set; }
        public List<XPPoolData>          XPPools   { get; set; }

        public CharacterData(string name, string description, List<AbilityInstanceData> abilities, List<XPPoolData> xPPools)
        {
            Name        = name;
            Description = description;
            Abilities   = abilities;
            XPPools     = xPPools;
        }

        public bool IsSameSpec(CharacterData other)
        {
            if (other == null) return false;
            if (this.GetType()       != other.GetType())       return false;
            if (this.Name            != other.Name)            return false;
            if (this.Description     != other.Description)     return false;
            if (this.Abilities.Count != other.Abilities.Count) return false;
            for (int ii = 0; ii < this.Abilities.Count; ii++)
            {
                if (!this.Abilities[ii].IsSameSpec(other.Abilities[ii]))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class XPPoolData
    {
        public string Name        { get; }
        public string Description { get; }
        public int    InitialXP   { get; }
        public int    RemainingXP { get; }

        public XPPoolData(string name, string description, int initialXP, int remainingXP)
        {
            this.Name        = name;
            this.Description = description;
            this.InitialXP   = initialXP;
            this.RemainingXP = remainingXP;
        }
    }

    public class AbilityInstanceData
    {
        public string Category   { get; }
        public string Type       { get; }
        public string TypeAbbrev { get; }

        public string Name      { get; }
        public int    XP        { get; }
        public int    Score     { get; }
        public string Specialty { get; }

        public List<string> Id { get; private set; }

        public AbilityInstanceData(string category, string type, string typeAbbrev, string name, int xp, int score, string specialty, List<string> id)
        {
            Category   = category;
            Type       = type;
            TypeAbbrev = typeAbbrev;
            Name       = name;
            XP         = xp;
            Score      = score;
            Specialty  = specialty;
            Id         = id;
        }

        // Note: This includes a check on ID
        public bool IsSameSpec(AbilityInstanceData other)
        {
            if (other == null) return false;
            if (this.GetType()  != other.GetType())  return false;
            if (this.Name       != other.Name)       return false;
            if (this.Category   != other.Category)   return false;
            if (this.Type       != other.Type)       return false;
            if (this.TypeAbbrev != other.TypeAbbrev) return false;
            if (this.Score      != other.Score)      return false;
            if (this.Specialty  != other.Specialty)  return false;
            if (this.XP         != other.XP)         return false;
            for (int ii = 0; ii < Id.Count; ii++)
            {
                if (!this.Id[ii].Equals(other.Id[ii])) return false;
            }
            return true;
        }
    }
}
