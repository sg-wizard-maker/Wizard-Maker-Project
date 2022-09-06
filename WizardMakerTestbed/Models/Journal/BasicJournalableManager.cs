using System;
using System.Collections.Generic;
using System.Linq;

namespace WizardMakerPrototype.Models
{
    // This class handles cases where we need intelligence around adding journal entires.  
    //  For example, we may want to overwrite journal entries during character creation, which would be necessary to support changes 
    //  when a user selects a virtue or flaw (eg, wealthy)
    public class BasicJournalableManager : IJournalableManager
    {
        private bool isIsInCharacterGenerationMode = true;
        
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

        public void removeJournalEntry(String id) 
        {
            SortedSet<Journalable> result = new SortedSet<Journalable>(new JournableComparator());
            foreach (Journalable journalable in this.journalables)
            {
                if (!journalable.getId().Equals(id))
                {
                     result.Add(journalable);
                }
            }
            journalables = result;
        }

        //TODO: Do we need this?  Delete if not.
        public NewCharacterInitJournalEntry RetrieveCharacterInitJournalEntry()
        {
            try
            {
                NewCharacterInitJournalEntry result = (NewCharacterInitJournalEntry)journalables.ElementAt(0);
                if (result == null)
                {
                    throw new InvalidCharacterInitializationException("Attempting to retrieve the character initialization journal entry when it does not exist.");
                }
                return result;
            } catch (InvalidCastException ice)
            {
                throw new InvalidCharacterInitializationException("Attempting to retrieve the character initialization journal entry, but found another type instead: " 
                    + journalables.ElementAt(0).GetType().Name + "(" + ice.Message +")");
            }
        }
    }
}
