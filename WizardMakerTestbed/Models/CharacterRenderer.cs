using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    // Whereas the CharacterManager is responsible for interacting with the front end and adding journal entries, this class handles the
    //  updates that arise from journal entries.
    //  for example, the CharacterManager would add journal entries and then invoke the character renderer to populate the character internals
    //  based on those journal entries.
    internal class CharacterRenderer
    {

        public static void renderAllJournalEntries(Character character)
        {
            // Reset the experience pools
            foreach (XPPool xPPool in character.XPPoolList) { xPPool.reset(); }

            // Reset the abilities
            character.resetAbilities();

            foreach (IJournalable journalable in character.GetJournal())
            {
                journalable.Execute();
            }
        }

        //TODO: Need to create a way to modify/remove journal entries
        //  This will include rerendering the character in its entirety
        /** 
         * Ignores the specialty if the ability already exists.  Note this assumes only one specialty per ability.
         * XP is always absolute XP.  
         */
        public static void addAbility(Character character, string ability, int xp, string specialty)
        {
            if (!doesCharacterHaveAbility(character, ability))
            {

                // add the ability to the character
                character.abilities.Add(AbilityXPManager.createNewAbilityInstance(ability, xp, specialty));
            }
            else
            {
                retrieveAbilityInstance(character, ability).XP = xp;

            }

            AbilityXPManager.debitXPPoolsForAbility(retrieveAbilityInstance(character, ability), xp, character.XPPoolList);
        }

        // TODO: Test Area Lore skills.
        private static bool doesCharacterHaveAbility(Character character, string ability)
        {
            return getCharacterAbilitiesAsList(character).Contains(ability);
        }
        private static AbilityInstance retrieveAbilityInstance(Character character, string ability)
        {
            // TODO: Test to see what happens if there are multiple instances of the same ability
            return getCharacterAbilityInstancesAsList(character).Find(a => a.Name == ability);
        }

        private static List<AbilityInstance> getCharacterAbilityInstancesAsList(Character character) { return character.abilities.ToList(); }

        public static List<string> getCharacterAbilitiesAsList(Character character) { return character.abilities.Select<AbilityInstance, string>(a => a.Name).ToList(); }
    }
}
