using System;
using System.Collections.Generic;
using System.ComponentModel;  // various Attributes

namespace WizardMaker.DataDomain.Models
{
    public class AbilityInstance
    {
        #region Properties
        // To make a public property NOT present in the DataGridView, apply an attribute such as:
        //     [System.ComponentModel.Browsable(false)]
        // 
        //[Browsable(false)]
        //public string PropertyNotAppearingInThisDataGridView { get; set; }
        // 
        // To make a public property present in the DataGridView (and thus accessible to business logic), but NOT displayed:
        //     dataGridView1.Columns[0].Visible = false;

        [Browsable(false)]
        public ArchAbility Ability    { get; private set; }

        [Browsable(false)]
        public decimal     BaseXPCost { get { return Ability.BaseXpCost; } }

        [Browsable(false)]
        public string Category   { get { return this.Ability.Category;          } }

        [Browsable(false)]
        public string Type       { get { return this.Ability.Type.Name;         } }

        [DisplayName("Type")]
        public string TypeAbbrev { get { return this.Ability.Type.Abbreviation; } }

        public string Name       { get { return this.Ability.Name;              } }
        public int    XP         { get; set; }
        public int    Score      { get => AbilityXpCosts.ScoreForXP(XP, DetermineXpCost(Ability)); set => throw new NotImplementedException(); }

        [DisplayName(" ")]
        public string AddToScore { get { return this.HasPuissance ? this.PuissantBonus.ToString() : ""; } }

        public string Specialty  { get; set; }

        public List<string> CommonSpecializations { get { return this.Ability.CommonSpecializations; } }

        [Browsable(false)]
        public bool HasThisAbility { get; set; } = false;  // If false, display a blank value for XP / Score, rather than 0 / 0

        [DisplayName("Aff")]
        public bool HasAffinity    { get; set; } = false;

        [DisplayName("Pui")]
        public bool HasPuissance   { get; set; } = false;

        [Browsable(false)]
        public int  PuissantBonus  { get; private set; } = 2;

        // Assumption:
        // Each of these journal entries are ones that spend XP,
        // and that no single XP-spending journal entry will involve more than this ability instance
        // (ie, only one ability instance).
        public List<string> JournalIDs { get; private set; }

        // TODO:
        // Something more will be needed to represent how Languages
        // get a Puissant-like bonus from a related Language with a higher Score...
        #endregion

        #region Constructors
        public AbilityInstance ( ArchAbility ability, string journalID, int xp = 0, string specialty = "", 
            bool hasAffinity = false, bool hasPuissance = false, int puissantBonus = 2)
        {
            decimal xpCost = DetermineXpCost(ability);

            this.Ability = ability;
            this.XP      = xp;
            this.Specialty     = specialty ?? "";
            this.HasAffinity   = hasAffinity;
            this.HasPuissance  = hasPuissance;
            this.PuissantBonus = puissantBonus;
            this.JournalIDs = new List<string>() { journalID };
        }
        #endregion

        #region Methods (various)
        public override string ToString ()
        {
            string str = string.Format("{0} '{1}' XP={2} / S={3} ({4}) A={5}, P={6}",
                this.TypeAbbrev, this.Name, this.XP, this.Score, this.Specialty, HasAffinity, HasPuissance);
            return str;
        }

        public decimal DetermineXpCost(ArchAbility archAbility)
        {
            var result = HasAffinity ? AbilityXpCosts.BaseXpCostWithAffinity(archAbility.BaseXpCost) : archAbility.BaseXpCost;
            return result;
        }

        public void AddJournalID(string journalID)
        {
            this.JournalIDs.Add(journalID);
        }
        #endregion

        #region Static Methods (various)
        // This is used mostly for testing.
        // In cases where we do not have an ID from the Journal Entry.
        public static string CreateID()
        {
            Guid myGuid = Guid.NewGuid();
            var  result = myGuid.ToString();
            return result;
        }
        #endregion
    }
}
