using System.Runtime.CompilerServices;

using WizardMaker.DataDomain.Models.Journal;

[assembly: InternalsVisibleTo("WizardMakerTests")]
namespace WizardMaker.DataDomain.Models
{
    /**
     * TODO: Keep an eye that this class may better serve as a list of command instances to execute.
     * 
     * First journal entry for most characters.
     * The order (ie, that this journal entry is first) must be maintained in code.
     */
    public class NewCharacterInitJournalEntry : Journalable
    {
        private const string CHILDHOOD_LANGUAGE_POOL_NAME   = "Childhood language XP Pool";
        private const string CHILDHOOD_LANGUAGE_DESCRIPTION = "XP granted to starting characters that can be spent on one language";
        public  const int    CHILDHOOD_LANGUAGE_XP          = 75;

        private const string CHILDHOOD_POOL_NAME   = "Childhood XP Pool";
        private const string CHILDHOOD_DESCRIPTION = "XP granted to starting characters that can be spent on childhood skills";
        public  const int    CHILDHOOD_XP          = 45;

        public static string LATER_LIFE_POOL_NAME   = "Later life XP Pool";
        private const string LATER_LIFE_DESCRIPTION = "XP granted to starting characters that can be spent on anything the character can learn.  After age 5.";
        
        public static int CHILDHOOD_END_AGE = 5;

        // Sort before the SeasonYear
        public const int NEW_CHARACTER_INIT_SORT_ORDER = 50;

        public SimpleJournalEntry SingleJournalEntry { get; set; }

        public ArchAbility ChildhoodLanguage { get; set; }
        public int StartingAge { get; set; } = 25;

        public NewCharacterInitJournalEntry(int startingAge, ArchAbility childhoodLanguage, int sagaYearStart = 1220)
        {
            // TODO: Check that the starting language is a language.
            this.ChildhoodLanguage = childhoodLanguage;
            this.StartingAge       = startingAge;

            // TODO: Make this have to do with starting Age and user-specified year.
            SingleJournalEntry = new SimpleJournalEntry("Character initialized at age " + startingAge + " in " + (sagaYearStart - startingAge), new SeasonYear(sagaYearStart - startingAge, Season.SPRING));
        }

        public override void Execute(Character character)
        {
            // Validation
            if (StartingAge < CHILDHOOD_END_AGE)
            {
                throw new InvalidCharacterInitializationException("Starting age (" + StartingAge + ") is less than childhood age (" + CHILDHOOD_END_AGE + ").  We cannot support creation for characters that young.");
            }

            // Total for a starting age of 25 is:
            //  75 + 45 + 20*15
            // Characters will always need an overdrawn XP pool at the end.
            List<XPPool> tmp = new List<XPPool>() {
                new SpecificAbilitiesXpPool(
                    CHILDHOOD_LANGUAGE_POOL_NAME, 
                    CHILDHOOD_LANGUAGE_DESCRIPTION + " (" + ChildhoodLanguage.Name + ")", 
                    CHILDHOOD_LANGUAGE_XP, 
                    new List<ArchAbility>() { ChildhoodLanguage }
                ),
                new SpecificAbilitiesXpPool(CHILDHOOD_POOL_NAME, CHILDHOOD_DESCRIPTION, CHILDHOOD_XP, DetermineChildhoodAbilities()),
                new BasicXPPool(LATER_LIFE_POOL_NAME, LATER_LIFE_DESCRIPTION, DetermineLaterLifeXp(character.StartingAge, character.XpPerYear)),
                new AllowOverdrawnXpPool()
            };

            foreach (XPPool item in tmp)
            {
                character.XPPoolList.Add(item);
            }
        }

        private int DetermineLaterLifeXp(int startingAge, int xpPerYear)
        {
            return Math.Max(0, (startingAge - CHILDHOOD_END_AGE) * xpPerYear);
        }

        public static List<ArchAbility> DetermineChildhoodAbilities()
        {
            List<ArchAbility> result = new List<ArchAbility>();
            foreach (ArchAbility aa in ArchAbility.AllCommonAbilities)
            {
                if (aa.Type == AbilityType.GenChild)
                {
                    result.Add(aa);
                }
            }
            return result;
        }

        public override SeasonYear GetDate()
        {
            return SingleJournalEntry.GetDate();
        }

        public override string GetText()
        {
            return SingleJournalEntry.GetText();
        }

        public override void Undo()
        {
            throw new ShouldNotBeAbleToGetHereException("Attempting to undo initial character journl entry.");
        }

        public override string GetId()
        {
            return SingleJournalEntry.GetId();
        }

        public override bool IsSameSpecs(Journalable other)
        {
            if (!base.IsSameSpecs(other)) return false;
            NewCharacterInitJournalEntry o2 = (NewCharacterInitJournalEntry) other;
            if (o2.ChildhoodLanguage.Name != ChildhoodLanguage.Name)    return false;
            if (o2.StartingAge            != StartingAge)               return false;
            if (!o2.SingleJournalEntry.IsSameSpecs(SingleJournalEntry)) return false;
            return true;
        }

        public override int SortOrder()
        {
            return JournalSortingConstants.NEW_CHARACTER_INIT_SORT_ORDER;
        }
    }
}
