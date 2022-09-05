using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using WizardMakerTestbed.Models;
using WizardMakerPrototype.Models.CharacterPersist;

[assembly: InternalsVisibleTo("WizardMakerTests")]
[assembly: InternalsVisibleTo("WizardMakerTests.Models")]
[assembly: InternalsVisibleTo("WizardMakerTests.Models.Tests")]
namespace WizardMakerPrototype.Models
{
    /**
     * This class is meant to interface witha front end, be it API calls or a GUI.
     * This happens by adding journal entries and then rendering the Character from the assembled list of journal entries.
     * */
    public class CharacterManager
    {
        private Character Character;

        public const string ABILITY_CREATION_NAME_PREFIX = "Initial: ";

        public CharacterManager(int startingAge)
        {
            //TODO: This needs to be an input, not hardcoded
            ArchAbility childhoodLanguage = ArchAbility.LangEnglish;

            this.Character = new Character("New Character", "", startingAge);
            
            //TODO: Re-assess whether initializing a journal entry with "this" has a smell.
            this.Character.addJournalable(new NewCharacterInitJournalEntry(startingAge, childhoodLanguage, Character.XpPerSeasonForInitialCreation));

            // TODO: Need a layer that will judge what abilities a character is even allowed to choose at any time (given that virtues and flaws can change this access).
            updateAbilityDuringCreation(childhoodLanguage.Name, NewCharacterInitJournalEntry.CHILDHOOD_LANGUAGE_XP, "");

            // Last step:  Render the character with all journal entries.
            CharacterRenderer.renderAllJournalEntries(Character);
        }

        //TODO: Make class to wrap character pools.  This way we can just obtain the pool for childhood, etc, through that interface.  And look at aggregate information.
      
        //TODO: Delete this method.  Caller should render the CharacterData and get the name there.
        public string getCharacterName()
        {
            return Character.Name;
        }

        /**
         * Use during character creation, not as part of advancement.
         * 
         * This method can handle a new ability or an existing one.
         */
        public void updateAbilityDuringCreation(string ability, int absoluteXp, string specialty)
        {
            //TODO: Fix the ordering because the SeasonYear must always be later than the NewCharacterJournalInit
            XpAbilitySpendJournalEntry xpAbilitySpendJournalEntry = new XpAbilitySpendJournalEntry(ABILITY_CREATION_NAME_PREFIX + ability,
                new SeasonYear(1219, Season.SUMMER), ability, absoluteXp, specialty);

            Character.addJournalable(xpAbilitySpendJournalEntry);
            CharacterRenderer.renderAllJournalEntries(Character);
        }

        public void DeleteJournalEntry(string id)
        {
            Character.removeJournalable(id);

            // Render, which will handle the resetting of XP Pools.
            CharacterRenderer.renderAllJournalEntries(Character);
        }

        public CharacterData renderCharacterAsCharacterData()
        {
            return CharacterRenderer.renderCharacterAsCharacterData(Character);
        }

        public int getXPPoolCount() { return Character.XPPoolList.Count; }

        /** This will always return a number >= 0.  This will not include overdrawn
         * Assumes that the overdrawn pool is the last one on the list.
         */
        public int totalRemainingXPWithoutOverdrawn()
        {
            return Character.totalRemainingXPWithoutOverdrawn();
        }

        public int getJournalSize() { return Character.GetJournal().Count; }

        // TODO: A lot more error checking needs to take place here
        // TODO: How would this work for API front end? 
        // Note: This will overwrite any file
        public static void WriteToFile(Character c, string absoluteFilename)
        {
            FileStream fs = new FileStream(absoluteFilename, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            RawCharacter rc = new RawCharacter(c);
            sw.Write(rc.serializeJson());
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static Character ReadFromFile(string absoluteFilename)
        {
            FileStream fs = new FileStream(absoluteFilename, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            RawCharacter rc = RawCharacter.deserializeJson(sw.ReadToEnd());
            sw.Close();
            fs.Close();

            return new Character(rc);
        }
    }
}
