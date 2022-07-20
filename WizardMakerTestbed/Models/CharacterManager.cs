using System.Collections.Generic;
using System.Linq;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    public class CharacterManager
    {
        private Character character;
        public AbilityXPManager abilityXPManager = new AbilityXPManager();

        public CharacterManager(Character character)
        {
            this.character = character;
        }

        public CharacterManager()
        {
            this.character = new Models.Character("New Character", "", new List<AbilityInstance>(), new List<Journalable>()); ;
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
    }
}
