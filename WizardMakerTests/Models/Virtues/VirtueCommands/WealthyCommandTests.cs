using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMaker.DataDomain.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMaker.DataDomain.Models.Journal;
using WizardMakerTests.Models.Virtues.VirtueCommands;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands.Tests;

[TestClass()]
public class WealthyCommandTests
{
    [TestMethod()]
    public void ExecuteTest()
    {
        int STARTING_AGE = 25;
        int SAGA_START = 1220;
        string virtueName = "Wealthy";
        // Create a wealthy character
        Character c = CommandTestUtilities.GenerateBasicTestCharacterWithStartingVirtue(STARTING_AGE, virtueName, SAGA_START);
        
        CharacterRenderer.RenderAllJournalEntries(c);

        // Assert that the character has 20 XP and that the Later Life XP is updated accordingly.
        Assert.AreEqual(WealthyCommand.WEALTHY_ANNUAL_XP, c.XpPerYear);

        foreach (XPPool xppool in c.XPPoolList)
        {
            if (xppool.Name.Equals(NewCharacterInitJournalEntry.LATER_LIFE_POOL_NAME))
            {
                Assert.AreEqual((STARTING_AGE - NewCharacterInitJournalEntry.CHILDHOOD_END_AGE) * WealthyCommand.WEALTHY_ANNUAL_XP, xppool.InitialXP);
            }
        }
    }
}