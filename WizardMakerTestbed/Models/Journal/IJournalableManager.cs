using System;
using System.Collections;
using System.Collections.Generic;

namespace WizardMakerPrototype.Models
{
    internal interface IJournalableManager
    {
        /**
         * For when the manager is in advancement vs initial character generation.
         */
        bool isInCharacterGenerationMode();

        void setCharacterGenerationMode(bool isCharacterGenerationMode);

        SortedSet<Journalable> getJournalables();

        void addJournalable(Journalable journalable);

        void removeJournalEntry(String id);
    }

    public class JournableComparator: IComparer<Journalable>
    {
        public int Compare(Journalable x, Journalable y)
        {

            if (x.getDate().Year == y.getDate().Year)
            {
                if (x.getDate().season == y.getDate().season)
                {
                    if (x.getId() == y.getId())
                    {
                        return new CaseInsensitiveComparer().Compare(x.getText(), y.getText());
                    } else
                    {
                        return new CaseInsensitiveComparer().Compare(x.getId(), y.getId());
                    }
                } else
                {
                    return x.getDate().season.CompareTo(y.getDate().season);
                }

            }
            return x.getDate().Year.CompareTo(y.getDate().Year);
        }
    }


}
