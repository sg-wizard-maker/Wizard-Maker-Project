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
        // TODO: need comparator
        private SortedSet<Journalable> journalables = new SortedSet<Journalable>(new JournableComparator());

        public void addJournalable(Journalable journalable)
        {
            // If we are in character creation mode, then overwrite this with any existing journal entry.
            if (this.isIsInCharacterGenerationMode)
            {

                List<Journalable> journalEntriesToRemove = new List<Journalable>();
                // Check if this is in our existing list (matching by text).  If so, remove it (it will get replaced next)
                foreach (Journalable journalable2 in this.journalables)
                {
                    if (journalable2.getText() == journalable.getText())
                    {
                        journalEntriesToRemove.Add(journalable2);
                    }
                }

                foreach (Journalable journalableToRemove in journalEntriesToRemove) { journalables.Remove(journalableToRemove); }
            }
            // Add a new one.
            journalables.Add(journalable);
        }
            

        public SortedSet<Journalable> getJournalables()
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
    }
}
