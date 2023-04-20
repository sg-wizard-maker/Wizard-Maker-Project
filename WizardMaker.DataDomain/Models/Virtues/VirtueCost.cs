using System;
using System.Collections.Generic;

using WizardMaker.DataDomain.Models.Virtues.VirtueCommands;

namespace WizardMaker.DataDomain.Models.Virtues
{
    public enum VirtueCost 
    {
        Free        = 0,
        Minor       = 1,
        DoubleMinor = 2,  // There are some very severe Minor flaws that might be house-ruled as costing "x2 Minor", such as "Hobbled" from (HoH:MC pg 136)
        Major       = 3, 
        DoubleMajor = 6,  // Cost for Magus characters for "Great Noble", from (LoM pg 31)
    }
}
