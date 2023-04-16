﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Virtues
{
    public class VirtueInstance
    {
        public ArchVirtue Virtue { get; private set;}

        /**
         * Virtues can only have one journal entry that grants.  Unlike arts and abilities
         * */
        public string JournalID { get; private set;}

        public VirtueInstance(ArchVirtue virtue, string journalID)
        {
            JournalID = journalID;
            Virtue = virtue;
        }
    }
}