﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMaker.DataDomain.Validation;
using WizardMaker.DataDomain.Models;

namespace WizardMaker.DataDomain.Models
{
    // Whereas the CharacterManager is responsible for interacting with the front end and adding journal entries, this class handles the
    //  updates that arise from journal entries.
    //  for example, the CharacterManager would add journal entries and then invoke the character renderer to populate the character internals
    //  based on those journal entries.
    //
    // This class also renders the CharacterData.
    internal class CharacterRenderer
    {

        // This method assumes that the journal entries in a Character class are sorted by SeasonYear.
        public static void RenderAllJournalEntries(Character character)
        {
            // Reset the validation log
            ValidationLog.reset();

            //Reset the Character
            character.resetAbilities();
            // Perhaps we should be deleting the XP Pools rather than resetting?
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
            bool isPuissant = false;
            bool isAffinity = false;
            if (character.puissantAbilities.Contains(ability)) { isPuissant = true; }
            if (character.affinityAbilities.Contains(ability)) { isAffinity = true; }

            if (!doesCharacterHaveAbility(character, ability))
            {
                ArchAbility a = ArchAbility.lookupCommonAbilities(ability);
                if (!character.IsAbilityAllowedToBePurchased(a)) ValidationLog.AddValidationMessage("Adding an ability to the character that is not available: " + ability);
                // add the ability to the character
                character.abilities.Add(AbilityXPManager.createNewAbilityInstance(ability, xp, specialty, journalID, isPuissant, isAffinity));
            }
            else
            {
                // TODO: Do we need to handle the journalID?  Yes, because if we want to support deletion of an ability, we want to delete all associated journal entries.
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
            // TODO: Test to see what happens if there are multiple instances of the same ability.  That should never happen and exception should be thrown.
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

            List<XPPoolData> xpPools = new List<XPPoolData>();

            foreach (XPPool xPPool in character.XPPoolList)
            {
                xpPools.Add(convertXPPoolData(xPPool));
            }

            return new CharacterData(character.Name, character.Description, abilities, xpPools);
        }

        private static AbilityInstanceData convertAbilityInstanceData(AbilityInstance abilityInstance)
        {
            // Note that for the front end the ID of the ability is also the name.  This may need to be cahnged in the future.
            return new AbilityInstanceData(abilityInstance.Category,
                abilityInstance.Type, abilityInstance.TypeAbbrev.ToString(), abilityInstance.Name, abilityInstance.XP, abilityInstance.Score,
                abilityInstance.Specialty, abilityInstance.journalIDs);
        }

        private static XPPoolData convertXPPoolData(XPPool xPPool)
        {
            return new XPPoolData(xPPool.name, xPPool.description, xPPool.initialXP, xPPool.remainingXP);
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