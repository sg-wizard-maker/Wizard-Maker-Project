using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.CharacterPersist;
using WizardMakerPrototype.Models.HermeticArts;
using WizardMakerPrototype.Models.Virtues;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    //TODO:  We could separate out a character as a set of journal entries (and name, startingAge, and description) from a rendered character (with ability instances and XP Pools).  This might simplify the design -- we could probably get rid of CharacterData and only need to serialize the Character.
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<AbilityInstance> abilities { get; set; }
        public List<string> puissantSkills { get; set; } = new List<string>();

        // TODO: We may want to make this a dictionary of ability names to a list of XP cost modifiers.
        public List<string> affinitySkills { get; set; } = new List<string>();
        public SortedSet<XPPool> XPPoolList { get; }

        // TODO: Refactor and do the whole allowed ability logic in a separate stateful instance.
        // The default list here are what all characters can have w/o additional virtues.
        public HashSet<AbilityType> AllowedAbilityTypes { get; private set; } = new HashSet<AbilityType>() { 
            AbilityType.General, AbilityType.GenChild
        };

        // This list is in addition (OR'ed) with the ability type list.  Anything on this list is allowed to be selected by the character without a validation error.
        public HashSet<ArchAbility> AllowedAbilities { get; private set; } = new HashSet<ArchAbility>();

        public int XpPerYear { get; set; } = 15;

        public List<VirtueInstance> virtues { get; private set; }

        public int startingAge { get; set; }

        private IJournalableManager journalableManager { get; set; }

        private SortedSet<Journalable> journalEntries { get { return journalableManager.getJournalables(); } }

        public Character(RawCharacter rc) : this(rc.Name, rc.Description, new List<AbilityInstance>(), rc.journalEntries, rc.startingAge) { }
            
        public Character(string name, string description, int startingAge) : this(name, description, new List<AbilityInstance>(), new List<Journalable>(), startingAge) { }

        public Character(string name, string description, List<AbilityInstance> abilities, List<Journalable> journalEntries, int startingAge)
        {
            Name = name;
            Description = description;
            this.abilities = abilities;
            
            this.journalableManager = new BasicJournalableManager();
            foreach(Journalable journalable in journalEntries)
            {
                this.journalableManager.addJournalable(journalable);
            }
            this.startingAge = startingAge;
            this.XPPoolList = new SortedSet<XPPool>(new XPPoolComparer());
            this.virtues = new List<VirtueInstance>();
        }
        
        public void EndCharacterCreation()
        {
            journalableManager.setCharacterGenerationMode(false);
        }

        public bool IsInitialCharacterFinished()
        {
            return !journalableManager.isInCharacterGenerationMode();
        }

        public void addJournalable(Journalable journalable) { journalableManager.addJournalable(journalable); }
        public void removeJournalable(string text) { journalableManager.removeJournalEntry(text); }

        public SortedSet<Journalable> GetJournal() { return journalableManager.getJournalables(); }

        // Assumes that the last element in the XP Pool list is the overdrawn pool.
        public int totalRemainingXPWithoutOverdrawn()
        {
            int result = 0;
            for (int i = 0; i < XPPoolList.Count - 1; i++)
            {
                result += XPPoolList.ElementAt(i).remainingXP;
            }
            return result;
        }

        public void resetAbilities() { abilities = new List<AbilityInstance>(); }

        public bool IsSkillAllowedToBePurchased(ArchAbility a)
        {
            if (this.AllowedAbilityTypes.Contains(a.Type)) return true;
            if (this.AllowedAbilities.Contains(a)) return true;
            return false;
        }

        public bool IsSkillAllowedToBePurchased(ArchHermeticArt a)
        {
            // If the character has the gift and is a Hermetic Magus then the answer is "true"
            if (this.virtues.Select(v => v.Virtue).ToList().Contains(ArchVirtue.NameToArchVirtue[ArchVirtue.THE_GIFT]) &
                this.virtues.Select(v => v.Virtue).ToList().Contains(ArchVirtue.NameToArchVirtue[ArchVirtue.HERMETIC_MAGUS]))
            {
                return true;
            }
            return false;
        }
    }
}
