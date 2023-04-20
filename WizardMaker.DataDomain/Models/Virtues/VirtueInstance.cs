using System;

namespace WizardMaker.DataDomain.Models.Virtues
{
    public class VirtueInstance
    {
        #region Properties
        public ArchVirtue Virtue { get; private set; }

        // Virtues can only have one journal entry that grants.  Unlike arts and abilities
        public string JournalID { get; private set; }
        #endregion

        #region Constructors
        public VirtueInstance(ArchVirtue virtue, string journalID)
        {
            JournalID = journalID;
            Virtue    = virtue;
        }
        #endregion
    }
}
