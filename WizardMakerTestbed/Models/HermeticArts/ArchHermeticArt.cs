using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.Skills;

namespace WizardMakerPrototype.Models.HermeticArts
{
    public class HermeticArtType
    {

        public string Abbreviation { get; private set; }
        public string Name { get; private set; }


        // This is only public so that it can be available for serialization
        public HermeticArtType(string name, string abbrev)
        {
            this.Abbreviation = abbrev;
            this.Name = name;
        }

        public static HermeticArtType Technique = new HermeticArtType("Technique", "Te");
        public static HermeticArtType Form = new HermeticArtType("Form", "Fo");
    }
    public class ArchHermeticArt
    {
        private ArchSkill Skill;

        //TODO: This seems odd that I need to repeat this for the aggregation.  Yet getting code reuse from inheritance seems messy.
        public decimal BaseXpCost { get { return Skill.BaseXpCost; } }
        public string Abbreviation { get { return Skill.Abbreviation; } }
        public string Name { get { return Skill.Name; } }

        public HermeticArtType Type { get; private set; }

        public ArchHermeticArt(string name, HermeticArtType type)
        {
            Skill = new ArchSkill(name.Substring(0,2), name, 1);
            Type = type;

        }

        #region Arch Hermetic Arts static instances
        public static ArchHermeticArt Creo = new ArchHermeticArt("Creo", HermeticArtType.Technique);
        public static ArchHermeticArt Intellego = new ArchHermeticArt("Intellego", HermeticArtType.Technique);
        public static ArchHermeticArt Muto = new ArchHermeticArt("Muto", HermeticArtType.Technique);
        public static ArchHermeticArt Perdo = new ArchHermeticArt("Perdo", HermeticArtType.Technique);
        public static ArchHermeticArt Rego = new ArchHermeticArt("Rego", HermeticArtType.Technique);
        public static ArchHermeticArt Animal = new ArchHermeticArt("Animal", HermeticArtType.Form);
        public static ArchHermeticArt Aquam = new ArchHermeticArt("Aquam", HermeticArtType.Form);
        public static ArchHermeticArt Auram = new ArchHermeticArt("Auram", HermeticArtType.Form);
        public static ArchHermeticArt Corpus = new ArchHermeticArt("Corpus", HermeticArtType.Form);
        public static ArchHermeticArt Herbam = new ArchHermeticArt("Herbam", HermeticArtType.Form);
        public static ArchHermeticArt Ignem = new ArchHermeticArt("Ignem", HermeticArtType.Form);
        public static ArchHermeticArt Imaginem = new ArchHermeticArt("Imaginem", HermeticArtType.Form);
        public static ArchHermeticArt Mentem = new ArchHermeticArt("Mentem", HermeticArtType.Form);
        public static ArchHermeticArt Terram = new ArchHermeticArt("Terram", HermeticArtType.Form);
        public static ArchHermeticArt Vim = new ArchHermeticArt("Vim", HermeticArtType.Form);
        #endregion
    }
}
