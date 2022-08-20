using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    public class BasicJournalableManager : IJournalableManager
    {
        private bool isIsInCharacterGenerationMode = true;
        
        private SortedSet<IJournalable> journalables = new SortedSet<IJournalable>(new JournableComparator());

        public void addJournalable(IJournalable journalable)
        {
            // If we are in character creation mode, then overwrite this with any existing journal entry.
            if (this.isIsInCharacterGenerationMode)
            {

                List<IJournalable> journalEntriesToRemove = new List<IJournalable>();
                // Check if this is in our existing list (matching by text).  If so, remove it (it will get replaced next)
                foreach (IJournalable journalable2 in this.journalables)
                {
                    if (journalable2.getText() == journalable.getText())
                    {
                        journalEntriesToRemove.Add(journalable2);
                    }
                }

                foreach (IJournalable journalableToRemove in journalEntriesToRemove) { journalables.Remove(journalableToRemove); }
            }
            // Add a new one.
            journalables.Add(journalable);
        }
            

        public SortedSet<IJournalable> getJournalables()
        {
            return journalables;
        }

        public bool isInCharacterGenerationMode()
        {
            return isIsInCharacterGenerationMode;
        }

        public void setCharacterGenerationMode(bool isCharacterGenerationMode)
        {
            isIsInCharacterGenerationMode = isCharacterGenerationMode;
        }

        public void removeJournalEntry(String id) 
        {
            SortedSet<IJournalable> result = new SortedSet<IJournalable>(new JournableComparator());
            foreach (IJournalable journalable in this.journalables)
            {
                if (!journalable.getId().Equals(id))
                {
                     result.Add(journalable);
                }
            }
            journalables = result;
        }
    }
}
