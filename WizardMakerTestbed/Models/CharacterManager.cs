using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WizardMakerTestbed.Models;

[assembly: InternalsVisibleTo("WizardMakerTests")]
[assembly: InternalsVisibleTo("WizardMakerTests.Models")]
namespace WizardMakerPrototype.Models
{
    /**
     * This class is meant to interface witha front end, be it API calls or a GUI.
     * This happens by adding journal entries and then rendering the Character from the assembled list of journal entries.
     * */
    public class CharacterManager
    {
        private Character character;

        public const string ABILITY_CREATION_NAME_PREFIX = "Initial: ";

        public CharacterManager(int startingAge)
        {
            //TODO: This needs to be an input, not hardcoded
            ArchAbility childhoodLanguage = ArchAbility.LangEnglish;

            this.character = new Character("New Character", "", new List<AbilityInstance>(), new List<IJournalable>(), startingAge);
            
            //TODO: Re-assess whether initializing a journal entry with "this" has a smell.
            this.character.addJournalable(new NewCharacterInitJournalEntry(character, startingAge, childhoodLanguage, 15));
            

            // TODO: Need a layer that will judge what abilities a character is even allowed to choose at any time (given that virtues and flaws can change this access).
            updateAbilityDuringCreation(childhoodLanguage.Name, NewCharacterInitJournalEntry.CHILDHOOD_LANGUAGE_XP, "");

            // Last step:  Render the character with all journal entries.
            CharacterRenderer.renderAllJournalEntries(character);
        }

        //TODO: Make class to wrap character pools.  This way we can just obtain the pool for childhood, etc, through that interface.  And look at aggregate information.
      

        //TODO: Implement:  Make sure to disallow removal of the overdrawn and some basic pools, eg childhood.
        public void removeXPPool(string xpPoolName)
        {
            throw new NotImplementedException();
        }

        public string getCharacterName()
        {
            return character.Name;
        }

        /**
         * Use during character creation, not as part of advancement.
         * 
         * This method can handle a new ability or an existing one.
         */
        public void updateAbilityDuringCreation(string ability, int absoluteXp, string specialty)
        {
            XpAbilitySpendJournalEntry xpAbilitySpendJournalEntry = new XpAbilitySpendJournalEntry(ABILITY_CREATION_NAME_PREFIX + ability,
                new SeasonYear(1219, Season.SPRING), character, ability, absoluteXp, specialty);

            character.addJournalable(xpAbilitySpendJournalEntry);
            CharacterRenderer.renderAllJournalEntries(character);
        }

        public void deleteAbilityInstance(string id)
        {
            character.removeJournalable(id);

            // Render, which will handle the resetting of XP Pools.
            CharacterRenderer.renderAllJournalEntries(character);
        }

        public CharacterData renderCharacterAsCharacterData()
        {
            return CharacterRenderer.renderCharacterAsCharacterData(character);
        }

        public string renderXPPoolsAsJson()
        {
            return JsonConvert.SerializeObject(character.XPPoolList, Formatting.Indented);
        }

        public int getXPPoolCount() { return character.XPPoolList.Count; }

        /** This will always return a number >= 0.  This will not include overdrawn
         * Assumes that the overdrawn pool is the last one on the list.
         */
        public int totalRemainingXPWithoutOverdrawn()
        {
            return character.totalRemainingXPWithoutOverdrawn();
        }

        public int getJournalSize() { return character.GetJournal().Count; }
    }
}
