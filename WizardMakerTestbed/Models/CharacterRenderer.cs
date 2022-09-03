using Newtonsoft.Json;
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
    //
    // This class also renders the CharacterData.
    internal class CharacterRenderer
    {

        public static void renderAllJournalEntries(Character character)
        {
            //Reset the Character
            character.resetAbilities();
            foreach (XPPool xPPool in character.XPPoolList) { xPPool.reset(); }

            foreach (Journalable journalable in character.GetJournal())
            {
                journalable.Execute(character);
            }
        }

        /** 
         * Ignores the specialty if the ability already exists.  Note this assumes only one specialty per ability.
         * XP is always absolute XP, unless it is flagged as incremental.  Then it will be added.
         */
        public static void addAbility(Character character, string ability, int xp, string specialty, bool isIncrementalXP, string journalID)
        {
            if (!doesCharacterHaveAbility(character, ability))
            {

                // add the ability to the character
                character.abilities.Add(AbilityXPManager.createNewAbilityInstance(ability, xp, specialty, journalID));
            }
            else
            {
                if (isIncrementalXP) { retrieveAbilityInstance(character, ability).XP += xp; }
                else { retrieveAbilityInstance(character, ability).XP = xp; }
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

        private static List<string> getCharacterAbilitiesAsList(Character character) { return character.abilities.Select<AbilityInstance, string>(a => a.Name).ToList(); }

        private static CharacterData convertCharacterToCharacterData(Character character)
        {

            List<AbilityInstanceData> abilities = new List<AbilityInstanceData>();

            foreach (AbilityInstance abilityInstance in character.abilities)
            {
                abilities.Add(convertAbilityInstanceData(abilityInstance));
            }

            return new CharacterData(character.Name, character.Description, abilities);
        }

        private static AbilityInstanceData convertAbilityInstanceData(AbilityInstance abilityInstance)
        {
            // Note that for the front end the ID of the ability is also the name.  This may need to be cahnged in the future.
            return new AbilityInstanceData(abilityInstance.Category,
                abilityInstance.Type, abilityInstance.TypeAbbrev.ToString(), abilityInstance.Name, abilityInstance.XP, abilityInstance.Score,
                abilityInstance.Specialty, abilityInstance.journalID);
        }

        public static CharacterData renderCharacterAsCharacterData(Character character)
        {
            return convertCharacterToCharacterData(character);
        }

        public static string serializeCharacterData(CharacterData cd)
        {
            return JsonConvert.SerializeObject(cd, Formatting.Indented);
        }

        public static CharacterData deserializeCharacterData(string json)
        {
            return JsonConvert.DeserializeObject<CharacterData>(json);
        }


    }
}
