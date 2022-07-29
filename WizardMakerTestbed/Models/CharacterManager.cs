using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    public class CharacterManager
    {
        private Character character;
        private AbilityXPManager abilityXPManager;

        private SortedSet<XPPool> XPPoolList;

        private const string INITIAL_POOL_NAME = "Initial XP Pool";
        private const string INITIAL_POOL_DESCRIPTION = "XP granted to starting characters that can be spent on anything";
        private const int INITIAL_POOL_XP = 300;

        public CharacterManager()
        {
            this.character = new Models.Character("New Character", "", new List<AbilityInstance>(), new List<Journalable>());
            this.abilityXPManager = new AbilityXPManager();

            // Characters will always need an overdrawn XP pool at the end.
            // TODO: Find class that automatically sorts this list and sort it from least versatile to most versatile.  (Use sortOrder attribute for now)
            // TODO: The AllowOverdrawnPool must always appear last.  This seems a bit fragile.
            // TODO: Need to initialize a character with the journal entries that simply create these XP Pools.
            this.XPPoolList = new SortedSet<XPPool>();
            this.XPPoolList.Add(new BasicXPPool(INITIAL_POOL_NAME, INITIAL_POOL_DESCRIPTION, INITIAL_POOL_XP));
            this.XPPoolList.Add(new AllowOverdrawnXpPool());
        }

        public string getCharacterName()
        {
            return character.Name;
        }

        private void debitXPPoolsForAbility(AbilityInstance a, int xp)
        {
            int remainingXPToAllocate = xp;

            // Allocate the XP cost to the remaining pools.
            foreach (XPPool p in XPPoolList)
            {
                if (p.CanSpendOnAbility(a.Ability))
                {
                    int allocatedXP = Math.Min(p.remainingXP, remainingXPToAllocate);

                    // Adjust the pool
                    p.remainingXP -= allocatedXP;

                    // Track whether we have allocated all of the necessary XP.
                    remainingXPToAllocate -= allocatedXP;

                    if (remainingXPToAllocate == 0)
                    {
                        break;
                    }
                }
            }

            if (remainingXPToAllocate > 0)
            {
                throw new ShouldNotBeAbleToGetHereException("Could not allocate XP for the ability " + a.Name + ".  Please send this error to a developer.");
            }
        } 

        /** 
         * Ignores the specialty if the ability already exists.  Note this assumes only one specialty per ability.
         */
        public void addAbility(string ability, int xp, string specialty, bool isActualXP = false)
        {
            if (!doesCharacterHaveAbility(ability))
            {
                // add the ability to the character
                character.abilities.Add(abilityXPManager.createNewAbilityInstance(ability, xp, specialty));
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

            debitXPPoolsForAbility(retrieveAbilityInstance(ability), xp);
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
    }
}
