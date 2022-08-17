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
    public class CharacterManager
    {
        private Character character;

        private SortedSet<XPPool> XPPoolList;

        private Dictionary<string, Journalable> abilityNameToJournalEntry;

        public const string ABILITY_CREATION_NAME_PREFIX = "Initial: ";

        public CharacterManager(int startingAge)
        {
            //TODO: This needs to be an input, not hardcoded
            ArchAbility childhoodLanguage = ArchAbility.LangEnglish;

            this.character = new Character("New Character", "", new List<AbilityInstance>(), new List<Journalable>()
            {
                //TODO: Re-assess whether initializing a journal entry with "this" has a smell.
                new NewCharacterInitJournalEntry(this, startingAge, childhoodLanguage)
            }, startingAge);

            // Characters will always need an overdrawn XP pool at the end.
            // TODO: Replace with mechanism of journal entries in a refactoring.  This will be a pretty large refactoring.
            // TODO: Need code that will take all journal spending entries and redo all of the XPPool allocations.
            // TODO: Need a layer that will judge what abilities a character is even allowed to choose at any time (given that virtues and flaws can change this access).
            this.XPPoolList = new SortedSet<XPPool>(new XPPoolComparer());

            this.abilityNameToJournalEntry = new Dictionary<string, Journalable>();

            updateAbilityDuringCreation(childhoodLanguage.Name, NewCharacterInitJournalEntry.CHILDHOOD_LANGUAGE_XP, "");

            // Last step:  Render the character with all journal entries.
            renderAllJournalEntries();
        }

        //TODO: Make class to wrap character pools.  This way we can just obtain the pool for childhood, etc, through that interface.  And look at aggregate information.
      

        //TODO: Implement:  Make sure to disallow removal of the overdrawn and some basic pools, eg childhood.
        public void removeXPPool(string xpPoolName)
        {
            throw new NotImplementedException();
        }

        public void addXPPool(XPPool xPPool) { this.XPPoolList.Add(xPPool); }

        public void renderAllJournalEntries()
        {
            // Reset the experience pools
            foreach(XPPool xPPool in this.XPPoolList) { xPPool.reset(); }   

            // Reset the abilities
            character.resetAbilities();

            foreach(Journalable journalable in character.GetJournal())
            {
                journalable.Execute();
            }
        }
        // TODO: Implement this if we need it
        public void renderAllJournalEntriesThroughSeasonYear(SeasonYear seasonYear)
        {
            throw new NotImplementedException();
        }

        public string getCharacterName()
        {
            return character.Name;
        }

        //TODO: Need to create a way to modify/remove journal entries
        //  This will include rerendering the character in its entirety
        /** 
         * Ignores the specialty if the ability already exists.  Note this assumes only one specialty per ability.
         * XP is always absolute XP.  
         */
        public void addAbility(string ability, int xp, string specialty, string id)
        {
            if (!doesCharacterHaveAbility(ability))
            {

                // add the ability to the character
                character.abilities.Add(AbilityXPManager.createNewAbilityInstance(ability, xp, specialty, id));
            }
            else
            {
                retrieveAbilityInstance(ability).XP = xp;
             
            }

            AbilityXPManager.debitXPPoolsForAbility(retrieveAbilityInstance(ability), xp, this.XPPoolList);
        }

        /**
         * Use during character creation, not as part of advancement.
         * 
         * This method can handle a new ability or an existing one.
         */
        public void updateAbilityDuringCreation(string ability, int absoluteXp, string specialty)
        {
            XpAbilitySpendJournalEntry xpAbilitySpendJournalEntry = new XpAbilitySpendJournalEntry(ABILITY_CREATION_NAME_PREFIX + ability,
                new SeasonYear(1219, Season.SPRING), this, ability, absoluteXp, specialty);

            character.addJournalable(xpAbilitySpendJournalEntry);
            renderAllJournalEntries();
        }

        public AbilityInstance retrieveAbilityInstance(string ability)
        {
            // TODO: Test to see what happens if there are multiple instances of the same ability
            return getCharacterAbilityInstancesAsList().Find(a => a.Name == ability);
        }

        public List<string> getCharacterAbilitiesAsList() { return character.abilities.Select<AbilityInstance, string>(a => a.Name).ToList(); }

        private List<AbilityInstance> getCharacterAbilityInstancesAsList() { return character.abilities.ToList(); }

        public string[] getCharacterAbilities()
        {
            return getCharacterAbilitiesAsList().ToArray();
        }

        // TODO: Test Area Lore skills.
        private bool doesCharacterHaveAbility(string ability)
        {
            return getCharacterAbilitiesAsList().Contains(ability);
        }

        public void deleteAbility(string id)
        {
            character.removeJournalable(id);

            // TODO: Return XP to the appropriate pools.

            renderAllJournalEntries();
        }

        public List<string> retrieveCommonSpecializations(string ability)
        {
            AbilityInstance abilityInstance =  retrieveAbilityInstance(ability);
            return abilityInstance.CommonSpecializations;
        }

        private CharacterData convertCharacterToCharacterData()
        {
            
            List<AbilityInstanceData> abilities = new List<AbilityInstanceData>();
            
            foreach (AbilityInstance abilityInstance in character.abilities)
            {
                abilities.Add(convertAbilityInstanceData(abilityInstance));
            }

            return new CharacterData(character.Name, character.Description, abilities);
        }

        private AbilityInstanceData convertAbilityInstanceData(AbilityInstance abilityInstance)
        {
            // Note that for the front end the ID of the ability is also the name.  This may need to be cahnged in the future.
            return new AbilityInstanceData(abilityInstance.Category,
                abilityInstance.Type, abilityInstance.TypeAbbrev.ToString(), abilityInstance.Name, abilityInstance.XP, abilityInstance.Score,
                abilityInstance.Specialty, abilityInstance.id);
        }

        public CharacterData renderCharacterAsCharacterData()
        {
            return convertCharacterToCharacterData();
        }

        public string renderXPPoolsAsJson()
        {
            return JsonConvert.SerializeObject(this.XPPoolList, Formatting.Indented);
        }

        public int getXPPoolCount() { return XPPoolList.Count; }

        /** This will always return a number >= 0.  This will not include overdrawn
         * Assumes that the overdrawn pool is the last one on the list.
         */
        public int totalRemainingXPWithoutOverdrawn()
        {
            int result = 0;
            for (int i = 0; i < XPPoolList.Count - 1; i ++)
            {
                result += XPPoolList.ElementAt(i).remainingXP;
            }
            return result;
        }

        public int getJournalSize() { return character.GetJournal().Count; }
    }
}
