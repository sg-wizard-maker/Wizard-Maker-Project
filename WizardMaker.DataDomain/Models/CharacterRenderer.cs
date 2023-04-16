using Newtonsoft.Json;

using WizardMaker.DataDomain.Validation;

namespace WizardMaker.DataDomain.Models
{
    // Whereas the CharacterManager is responsible for interacting with the front end and adding journal entries,
    // this class handles the updates that arise from journal entries.
    // For example, the CharacterManager would add journal entries and then invoke the character renderer
    // to populate the character internals based on those journal entries.
    //
    // This class also renders the CharacterData.
    internal class CharacterRenderer
    {
        // This method assumes that the journal entries in a Character class are sorted by SeasonYear.
        public static void RenderAllJournalEntries(Character character)
        {
            // Reset the validation log
            ValidationLog.Reset();

            // Reset the Character
            character.ResetAbilities();
            // Perhaps we should be deleting the XP Pools rather than resetting?
            foreach (XPPool pool in character.XPPoolList) 
            {
                pool.Reset(); 
            }

            foreach (Journalable jj in character.GetJournal())
            {
                jj.Execute(character);
            }
        }

        /*
         * Ignores the specialty if the ability already exists.  
         * Note this assumes only one specialty per ability.
         * XP is always absolute XP, unless it is flagged as incremental.  
         * Then it will be added.
         */
        public static void AddAbility(Character character, string ability, int xp, string specialty, bool isIncrementalXP, string journalID)
        {
            bool isPuissant = false;
            bool isAffinity = false;
            if (character.PuissantAbilities.Contains(ability)) { isPuissant = true; }
            if (character.AffinityAbilities.Contains(ability)) { isAffinity = true; }

            if (!DoesCharacterHaveAbility(character, ability))
            {
                ArchAbility a = ArchAbility.LookupCommonAbilities(ability);
                if (!character.IsAbilityAllowedToBePurchased(a))
                {
                    ValidationLog.AddValidationMessage("Adding an ability to the character that is not available: " + ability);
                }
                // Add the ability to the character
                character.Abilities.Add(AbilityXPManager.CreateNewAbilityInstance(ability, xp, specialty, journalID, isPuissant, isAffinity));
            }
            else
            {
                // TODO:
                // Do we need to handle the journalID?
                // Yes, because if we want to support deletion of an ability, we want to delete all associated journal entries.
                if (isIncrementalXP) 
                {
                    RetrieveAbilityInstance(character, ability).XP += xp; 
                }
                else 
                {
                    RetrieveAbilityInstance(character, ability).XP = xp; 
                }
            }

            AbilityXPManager.DebitXPPoolsForAbility(RetrieveAbilityInstance(character, ability), xp, character.XPPoolList);
        }

        // TODO: Test Area Lore skills.
        private static bool DoesCharacterHaveAbility(Character character, string ability)
        {
            return GetCharacterAbilitiesAsList(character).Contains(ability);
        }
        private static AbilityInstance RetrieveAbilityInstance(Character character, string ability)
        {
            // TODO:
            // Test to see what happens if there are multiple instances of the same ability.
            // That should never happen, and an exception should be thrown.
            var result = GetCharacterAbilityInstancesAsList(character).Find(a => a.Name == ability);
            return result;
        }

        private static List<AbilityInstance> GetCharacterAbilityInstancesAsList(Character character)
        {
            return character.Abilities.ToList(); 
        }

        private static List<string> GetCharacterAbilitiesAsList(Character character) 
        {
            return character.Abilities.Select<AbilityInstance, string>(a => a.Name).ToList(); 
        }

        private static CharacterData ConvertCharacterToCharacterData(Character character)
        {
            List<AbilityInstanceData> abilities = new List<AbilityInstanceData>();
            foreach (AbilityInstance abilityInstance in character.Abilities)
            {
                abilities.Add(ConvertAbilityInstanceData(abilityInstance));
            }

            List<XPPoolData> xpPools = new List<XPPoolData>();
            foreach (XPPool xPPool in character.XPPoolList)
            {
                xpPools.Add(ConvertXPPoolData(xPPool));
            }

            var result = new CharacterData(character.Name, character.Description, abilities, xpPools);
            return result;
        }

        private static AbilityInstanceData ConvertAbilityInstanceData(AbilityInstance abilityInstance)
        {
            // Note that for the front end the ID of the ability is also the name.
            // This may need to be changed in the future.
            var result = new AbilityInstanceData(
                abilityInstance.Category,
                abilityInstance.Type, 
                abilityInstance.TypeAbbrev, 
                abilityInstance.Name, 
                abilityInstance.XP, 
                abilityInstance.Score,
                abilityInstance.Specialty, 
                abilityInstance.JournalIDs
            );
            return result;
        }

        private static XPPoolData ConvertXPPoolData(XPPool pool)
        {
            var result = new XPPoolData(pool.Name, pool.Description, pool.InitialXP, pool.RemainingXP);
            return result;
        }

        public static CharacterData RenderCharacterAsCharacterData(Character character)
        {
            var result = ConvertCharacterToCharacterData(character);
            return result;
        }

        public static string SerializeCharacterData(CharacterData cd)
        {
            var result = JsonConvert.SerializeObject(cd, Formatting.Indented);
            return result;
        }

        public static CharacterData? DeserializeCharacterData(string json)
        {
            var result = JsonConvert.DeserializeObject<CharacterData>(json);
            return result;
        }
    }
}
