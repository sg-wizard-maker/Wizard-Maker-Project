using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.CharacterPersist;
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
        public SortedSet<XPPool> XPPoolList { get; }

        public int XpPerSeasonForInitialCreation { get; set; } = 15;

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
    }
}
