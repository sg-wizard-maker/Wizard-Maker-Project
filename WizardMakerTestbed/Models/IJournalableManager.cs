using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    internal interface IJournalableManager
    {
        /**
         * For when the manager is in advancement vs initial character generation.
         */
        bool isInCharacterGenerationMode();

        void setCharacterGenerationMode(bool isCharacterGenerationMode);

        SortedSet<IJournalable> getJournalables();

        void addJournalable(IJournalable journalable);

        void removeJournalEntry(String id);
    }

    public class JournableComparator: IComparer<IJournalable>
    {
        public int Compare(IJournalable x, IJournalable y)
        {

            if (x.getDate().Year == y.getDate().Year)
            {
                if (x.getDate().season == y.getDate().season)
                {
                    return new CaseInsensitiveComparer().Compare(x.getText(), y.getText());
                } else
                {
                    return x.getDate().season.CompareTo(y.getDate().season);
                }

            }
            return x.getDate().Year.CompareTo(y.getDate().Year);
        }
    }


}
