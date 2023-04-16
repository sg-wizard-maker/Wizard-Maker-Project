using System.Collections;

namespace WizardMaker.DataDomain.Models
{
    // TODO:
    // We can probably get rid of this interface.
    // It adds a layer of abstraction that we likely do not need.
    internal interface IJournalableManager
    {
        // For when the manager is in advancement vs initial character generation.
        bool IsInCharacterGenerationMode();

        void SetCharacterGenerationMode(bool isCharacterGenerationMode);

        SortedSet<Journalable> GetJournalables();

        void AddJournalable(Journalable journalable);

        void RemoveJournalEntry(String id);
    }

    public class JournableComparator: IComparer<Journalable>
    {
        public int Compare(Journalable x, Journalable y)
        {
            // TODO: Refactor arrow code
            if (x.SortOrder() == y.SortOrder()) { 

                if (x.GetDate().Year == y.GetDate().Year)
                {
                    if (x.GetDate().season == y.GetDate().season)
                    {
                        if (x.GetId() == y.GetId())
                        {
                            return new CaseInsensitiveComparer().Compare(x.GetText(), y.GetText());
                        }
                        else
                        {
                            return new CaseInsensitiveComparer().Compare(x.GetId(), y.GetId());
                        }
                    }
                    else
                    {
                        return x.GetDate().season.CompareTo(y.GetDate().season);
                    }

                }
                return x.GetDate().Year.CompareTo(y.GetDate().Year);
            }
            return x.SortOrder().CompareTo(y.SortOrder());
        }
    }
}
