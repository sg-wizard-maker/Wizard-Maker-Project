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
    public class NewCharacterInitJournalEntry : Journalable
    {

        private const string CHILDHOOD_LANGUAGE_POOL_NAME = "Childhood language XP Pool";
        private const string CHILDHOOD_LANGUAGE_DESCRIPTION = "XP granted to starting characters that can be spent on one language";
        public const int CHILDHOOD_LANGUAGE_XP = 75;

        private const string CHILDHOOD_POOL_NAME = "Childhood XP Pool";
        private const string CHILDHOOD_DESCRIPTION = "XP granted to starting characters that can be spent on childhood skills";
        private const int CHILDHOOD_XP = 45;

        private const string LATER_LIFE_POOL_NAME = "Later life XP Pool";
        private const string LATER_LIFE_DESCRIPTION = "XP granted to starting characters that can be spent on anything the character can learn.  After age 5.";
        
        private const int CHILDHOOD_END_AGE = 5;

        CharacterManager characterManager;
        ArchAbility childhoodLanguage;
        int startingAge;

        public NewCharacterInitJournalEntry(CharacterManager characterManager, int startingAge, ArchAbility childhoodLanguage)
        {
            this.characterManager = characterManager;
            this.childhoodLanguage = childhoodLanguage;
            this.startingAge = startingAge;
        }
    
        public void Execute()
        {
            // Total for a starting age of 25 is:
            //  75 + 45 + 20*15
            List<XPPool> tmp = new List<XPPool>() {
                new SpecificAbilitiesXpPool(CHILDHOOD_LANGUAGE_POOL_NAME, CHILDHOOD_LANGUAGE_DESCRIPTION + " (" + childhoodLanguage.Name + ")", CHILDHOOD_LANGUAGE_XP, new List<ArchAbility>() { childhoodLanguage }),
                new SpecificAbilitiesXpPool(CHILDHOOD_POOL_NAME, CHILDHOOD_DESCRIPTION, CHILDHOOD_XP, determineChildhoodAbilities()),
                new BasicXPPool(LATER_LIFE_POOL_NAME, LATER_LIFE_DESCRIPTION, determineLaterLifeXp(this.startingAge)),
                new AllowOverdrawnXpPool()
                };

            foreach (XPPool item in tmp)
            {
                characterManager.addXPPool(item);
            }
        }

        private int determineLaterLifeXp(int startingAge)
        {
            //TODO: This will not necessarily still be a constant once we build virtues and flaws (wealthy and poor change the "15").
            return Math.Max(0, (startingAge - CHILDHOOD_END_AGE) * 15);
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
            // TODO: Make this have to do with starting Age and user-specified year.
            return new SeasonYear(1219, Season.SPRING);
        }

        public string getText()
        {
            return "Character initialized at age " + startingAge;
        }

        public void Undo()
        {
            throw new ShouldNotBeAbleToGetHereException("Attempting to undo initial character journl entry.");
        }
    }


}
