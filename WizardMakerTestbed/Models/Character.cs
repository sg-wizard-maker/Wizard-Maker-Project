using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<AbilityInstance> abilities { get; set; }

        public int startingAge { get; set; }

        private IJournalableManager journalableManager { get; set; }

        private SortedSet<Journalable> journalEntries { get { return journalableManager.getJournalables(); } }

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
        }

        public void addJournalable(Journalable journalable) { journalableManager.addJournalable(journalable); }
        public void removeJournalable(string text) { journalableManager.removeJournalEntry(text); }

        public void resetAbilities() { abilities = new List<AbilityInstance>(); }

        public SortedSet<Journalable> GetJournal() { return journalableManager.getJournalables(); }
    }
}
