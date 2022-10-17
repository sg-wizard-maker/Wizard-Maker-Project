using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.HermeticArts
{
    internal class HermeticArtInstance
    {
        // TODO: Refactor to extract dupe code w/ abilities
        public ArchHermeticArt HermeticArt { get; private set; }
        public decimal BaseXPCost { get { return HermeticArt.BaseXpCost; } }

        public string Type { get { return this.HermeticArt.Type.Name; } }

        public string TypeAbbrev { get { return this.HermeticArt.Type.Abbreviation; } }

        public string Name { get { return this.HermeticArt.Name; } }
        public int XP { get; set; }
        public int Score { get => AbilityXpCosts.ScoreForXP(XP, determineXpCost(HermeticArt)); set => throw new NotImplementedException(); }

        [DisplayName(" ")]
        public string AddToScore { get { return this.HasPuissance ? this.PuissantBonus.ToString() : null; } }

        public bool HasAffinity { get; set; } = false;

        public bool HasPuissance { get; set; } = false;

        public int PuissantBonus { get; private set; } = 2;

        // Assumption:  Each of these journal entries are ones that spend XP and that no single XP-spending journal entry will involve more than this ability instance (ie, only one ability instance).
        public List<string> journalIDs { get; private set; }

        public decimal determineXpCost(ArchHermeticArt archHermeticArt)
        {
            return HasAffinity ? AbilityXpCosts.BaseXpCostWithAffinity(archHermeticArt.BaseXpCost) : archHermeticArt.BaseXpCost;
        }

    }
}
