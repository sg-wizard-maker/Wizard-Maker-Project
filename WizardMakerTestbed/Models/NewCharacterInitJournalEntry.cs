using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using WizardMakerPrototype.Models;

[assembly: InternalsVisibleTo("WizardMakerTests")]
namespace WizardMakerTestbed.Models
{
    /**
     * TODO: Keep an eye that this class may better serve as a list of command instances to execute.
     * 
     * First journal entry for most characters.
     */
    public class NewCharacterInitJournalEntry : IJournalable
    {

        private const string CHILDHOOD_LANGUAGE_POOL_NAME = "Childhood language XP Pool";
        private const string CHILDHOOD_LANGUAGE_DESCRIPTION = "XP granted to starting characters that can be spent on one language";
        public const int CHILDHOOD_LANGUAGE_XP = 75;

        private const string CHILDHOOD_POOL_NAME = "Childhood XP Pool";
        private const string CHILDHOOD_DESCRIPTION = "XP granted to starting characters that can be spent on childhood skills";
        public const int CHILDHOOD_XP = 45;

        private const string LATER_LIFE_POOL_NAME = "Later life XP Pool";
        private const string LATER_LIFE_DESCRIPTION = "XP granted to starting characters that can be spent on anything the character can learn.  After age 5.";
        
        public static int CHILDHOOD_END_AGE = 5;

        SingleJournalEntry singleJournalEntry;

        Character character;
        ArchAbility childhoodLanguage;
        int startingAge;

        // XP per year.  Eg, 20 for Wealthy.  Default is 15
        int xpPerYear;

        public NewCharacterInitJournalEntry(Character character, int startingAge, ArchAbility childhoodLanguage, int xpPerYear)
        {
            this.character = character;
            this.childhoodLanguage = childhoodLanguage;
            this.startingAge = startingAge;

            // TODO: Make this have to do with starting Age and user-specified year.
            singleJournalEntry = new SingleJournalEntry("Character initialized at age " + startingAge, new SeasonYear(1219, Season.SPRING));
            this.xpPerYear = xpPerYear;
        }

        public void Execute()
        {
            // Validation
            if (startingAge < CHILDHOOD_END_AGE)
            {
                throw new InvalidCharacterInitializationException("Starting age (" + startingAge + ") is less than childhood age (" + CHILDHOOD_END_AGE + ").  We cannot support creation for characters that young.");
            }

            // Total for a starting age of 25 is:
            //  75 + 45 + 20*15
            // Characters will always need an overdrawn XP pool at the end.
            List<XPPool> tmp = new List<XPPool>() {
                new SpecificAbilitiesXpPool(CHILDHOOD_LANGUAGE_POOL_NAME, CHILDHOOD_LANGUAGE_DESCRIPTION + " (" + childhoodLanguage.Name + ")", 
                                            CHILDHOOD_LANGUAGE_XP, new List<ArchAbility>() { childhoodLanguage }),
                new SpecificAbilitiesXpPool(CHILDHOOD_POOL_NAME, CHILDHOOD_DESCRIPTION, CHILDHOOD_XP, determineChildhoodAbilities()),
                new BasicXPPool(LATER_LIFE_POOL_NAME, LATER_LIFE_DESCRIPTION, determineLaterLifeXp(this.startingAge)),
                new AllowOverdrawnXpPool()
                };

            foreach (XPPool item in tmp)
            {
                character.XPPoolList.Add(item);
            }
        }

        private int determineLaterLifeXp(int startingAge)
        {
            return Math.Max(0, (startingAge - CHILDHOOD_END_AGE) * xpPerYear);
        }
        public static List<ArchAbility> determineChildhoodAbilities()
        {
            List<ArchAbility> result = new List<ArchAbility>();
            foreach (ArchAbility a in ArchAbility.AllCommonAbilities)
            {
                if (a.Type == AbilityType.GenChild)
                {
                    result.Add(a);
                }
            }
            return result;
        }

        public SeasonYear getDate()
        {
            return singleJournalEntry.getDate();
        }

        public string getText()
        {
            return singleJournalEntry.getText();
        }

        public void Undo()
        {
            throw new ShouldNotBeAbleToGetHereException("Attempting to undo initial character journl entry.");
        }

        public string getId()
        {
            return singleJournalEntry.getId();
        }
    }


}
