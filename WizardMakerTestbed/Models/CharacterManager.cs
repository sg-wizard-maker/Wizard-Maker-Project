using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WizardMakerTestbed.Models;

[assembly: InternalsVisibleTo("WizardMakerTests")]
namespace WizardMakerPrototype.Models
{
    public class CharacterManager
    {
        private Character character;

        private SortedSet<XPPool> XPPoolList;

        private const string INITIAL_POOL_NAME = "Initial XP Pool";
        private const string INITIAL_POOL_DESCRIPTION = "XP granted to starting characters that can be spent on anything";
        private const int INITIAL_POOL_XP = 300;

        private const string CHILDHOOD_LANGUAGE_POOL_NAME = "Childhood language XP Pool";
        private const string CHILDHOOD_LANGUAGE_DESCRIPTION = "XP granted to starting characters that can be spent on one language";
        private const int CHILDHOOD_LANGUAGE_XP = 75;

        private const string CHILDHOOD_POOL_NAME = "Childhood XP Pool";
        private const string CHILDHOOD_DESCRIPTION = "XP granted to starting characters that can be spent on childhood skills";
        private const int CHILDHOOD_XP = 45;

        private const string LATER_LIFE_POOL_NAME = "Later life XP Pool";
        private const string LATER_LIFE_DESCRIPTION = "XP granted to starting characters that can be spent on anything the character can learn.  After age 5.";

        private const int CHILDHOOD_END_AGE = 5;

        public CharacterManager()
        {
            this.character = new Models.Character("New Character", "", new List<AbilityInstance>(), new List<Journalable>());

            //TODO: This needs to be an input, not hardcoded
            ArchAbility childhoodLanguage = ArchAbility.LangEnglish;

            // Characters will always need an overdrawn XP pool at the end.
            // TODO: Find class that automatically sorts this list and sort it from least versatile to most versatile.  (Use sortOrder attribute for now)
            // TODO: Need to initialize a character with the journal entries that simply create these XP Pools.
            // TODO: Replace with mechanism of journal entries in a refactoring.  This will be a pretty large refactoring.
            // TODO: Need code that will take all journal spending entries and redo all of the XPPool allocations.
            // TODO: Need a layer that will judge what abilities a character is even allowed to choose at any time (given that virtues and flaws can change this access).
            this.XPPoolList = initializeXPPools(childhoodLanguage);
        }

        //TODO: Make class to wrap character pools.  This way we can just obtain the pool for childhood, etc, through that interface.  And look at aggregate information.
        private SortedSet<XPPool> initializeXPPools(ArchAbility childhoodLanguage)
        {
            return new SortedSet<XPPool>(new XPPoolComparer()) {
                new SpecificAbilitiesXpPool(CHILDHOOD_LANGUAGE_POOL_NAME, CHILDHOOD_LANGUAGE_DESCRIPTION, CHILDHOOD_LANGUAGE_XP, new List<ArchAbility>() {childhoodLanguage}),
                new SpecificAbilitiesXpPool(CHILDHOOD_POOL_NAME, CHILDHOOD_DESCRIPTION, CHILDHOOD_XP, determineChildhoodAbilities()),
                new BasicXPPool(LATER_LIFE_POOL_NAME, LATER_LIFE_DESCRIPTION, determineLaterLifeXp(character.startingAge)),
                new AllowOverdrawnXpPool()
            };
        }

        //TODO: Implement:  Make sure to disallow removal of the overdrawn and some basic pools, eg childhood.
        public void removeXPPool(string xpPoolName)
        {
            throw new NotImplementedException();
        }

        private int determineLaterLifeXp(int startingAge)
        {
            //TODO: This will not necessarily still be a constant once we build virtues and flaws (wealthy and poor change the "15").
            return Math.Max(0, (startingAge - CHILDHOOD_END_AGE) * 15);
        }

        public string getCharacterName()
        {
            return character.Name;
        }

        /** 
         * Ignores the specialty if the ability already exists.  Note this assumes only one specialty per ability.
         */
        public void addAbility(string ability, int xp, string specialty, bool isActualXP = false)
        {
            if (!doesCharacterHaveAbility(ability))
            {
                // add the ability to the character
                character.abilities.Add(AbilityXPManager.createNewAbilityInstance(ability, xp, specialty));
            }
            else
            {
                if (isActualXP)
                {
                    retrieveAbilityInstance(ability).XP = xp;
                }
                else
                {
                    retrieveAbilityInstance(ability).XP += xp;
                }
            }

            AbilityXPManager.debitXPPoolsForAbility(retrieveAbilityInstance(ability), xp, this.XPPoolList);
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

        public void deleteAbility(string ability)
        {
            // TODO: Return XP to the appropriate pools.

            // Delete the ability
            AbilityInstance abilityInstance = retrieveAbilityInstance(ability);
            character.abilities.Remove(abilityInstance);
        }

        public void setAbilityXp(string ability, int xp, string specialty)
        {
            addAbility(ability, xp, specialty, true);
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
            return new AbilityInstanceData(abilityInstance.Category,
                abilityInstance.Type, abilityInstance.TypeAbbrev.ToString(), abilityInstance.Name, abilityInstance.XP, abilityInstance.Score,
                abilityInstance.Specialty);
        }

        public CharacterData renderCharacterAsCharacterData()
        {
            return convertCharacterToCharacterData();
        }

        /**
         * TODO: Move elsewhere
         */
        public static List<ArchAbility> determineChildhoodAbilities()
        {
            List<ArchAbility> result = new List<ArchAbility> ();
            foreach (ArchAbility a in ArchAbility.AllCommonAbilities) { 
                if (a.Type == AbilityType.GenChild)
                {
                    result.Add(a);
                }
            }
            return result;
        }

        public string renderXPPoolsAsJson()
        {
            return JsonConvert.SerializeObject(this.XPPoolList, Formatting.Indented);
        }
    }
}
