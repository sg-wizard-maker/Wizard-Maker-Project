using WizardMaker.DataDomain.Models.Virtues;

namespace WizardMaker.DataDomain.Models.Journal
{
    public class AddVirtueJournalEntry : Journalable
    {
        #region Properties and Fields and Constants
        public SimpleJournalEntry Text;
        public ArchVirtue         ArchVirtue;

        public const string PREFIX = "Added virtue: ";
        #endregion

        #region Constructors
        // Secondary info is for cases where we have multiple virtues, such as:
        // Puissant Ability can actually be  "Puissant Brawl" or "Puissant Area Lore: England"
        public AddVirtueJournalEntry(SeasonYear sy, ArchVirtue archVirtue)
        {
            this.Text       = new SimpleJournalEntry(PREFIX + archVirtue.Name, sy);
            this.ArchVirtue = archVirtue;
        }
        #endregion

        #region Methods (various)
        public override void Execute(Character character)
        {
            VirtueInstance virtue = new VirtueInstance(ArchVirtue, this.GetId());
            character.Virtues.Add(virtue);
            virtue.Virtue.CharacterCommand.Execute(character);
        }

        public override SeasonYear GetDate()
        {
            return Text.GetDate();
        }

        public override string GetId()
        {
            return Text.GetId();
        }

        public override string GetText()
        {
            return Text.GetText();
        }

        public bool IsMatch(string virtueName)
        {
            return (PREFIX + ArchVirtue.Name) == (PREFIX + virtueName);
        }

        public override int SortOrder()
        {
            return JournalSortingConstants.ADD_VIRTUE_SORT_ORDER;
        }
        #endregion
    }
}
