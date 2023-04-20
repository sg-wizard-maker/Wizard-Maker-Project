using System;

namespace WizardMaker.DataDomain.Models
{
    // This class handles cases where we need intelligence around adding journal entires.  
    // For example, we may want to overwrite journal entries during character creation,
    // which would be necessary to support changes when a user selects a virtue or flaw (eg, wealthy)
    public class BasicJournalableManager : IJournalableManager
    {
        #region Properties and Fields
        private bool IsIsInCharacterGenerationMode = true;
        
        private SortedSet<Journalable> Journalables = new SortedSet<Journalable>(new JournalableComparator());
        #endregion

        #region Methods (various)
        public void AddJournalable(Journalable journalable)
        {
            // If we are in character creation mode, then overwrite this with any existing journal entry.
            if (this.IsIsInCharacterGenerationMode)
            {
                List<Journalable> journalEntriesToRemove = new List<Journalable>();
                // Check if this is in our existing list (matching by text).
                // If so, remove it (it will get replaced next)
                foreach (Journalable jj in this.Journalables)
                {
                    if (jj.GetText() == journalable.GetText())
                    {
                        journalEntriesToRemove.Add(jj);
                    }
                }

                foreach (Journalable journalableToRemove in journalEntriesToRemove) 
                {
                    Journalables.Remove(journalableToRemove); 
                }
            }
            // Add a new one.
            Journalables.Add(journalable);
        }

        public SortedSet<Journalable> GetJournalables()
        {
            return Journalables;
        }

        public bool IsInCharacterGenerationMode()
        {
            return IsIsInCharacterGenerationMode;
        }

        public void SetCharacterGenerationMode(bool isCharacterGenerationMode)
        {
            IsIsInCharacterGenerationMode = isCharacterGenerationMode;
        }

        public void RemoveJournalEntry(string id) 
        {
            SortedSet<Journalable> result = new SortedSet<Journalable>(new JournalableComparator());
            foreach (Journalable jj in this.Journalables)
            {
                if (!jj.GetId().Equals(id))
                {
                     result.Add(jj);
                }
            }
            Journalables = result;
        }

        // TODO: Do we need this?  Delete if not.
        public NewCharacterInitJournalEntry RetrieveCharacterInitJournalEntry()
        {
            try
            {
                NewCharacterInitJournalEntry result = (NewCharacterInitJournalEntry)Journalables.ElementAt(0);
                if (result == null)
                {
                    throw new InvalidCharacterInitializationException("Attempting to retrieve the character initialization journal entry when it does not exist.");
                }
                return result;
            } 
            catch (InvalidCastException ice)
            {
                throw new InvalidCharacterInitializationException(
                    "Attempting to retrieve the character initialization journal entry, but found another type instead: " 
                    + Journalables.ElementAt(0).GetType().Name + "(" + ice.Message +")"
                );
            }
        }
        #endregion
    }
}
