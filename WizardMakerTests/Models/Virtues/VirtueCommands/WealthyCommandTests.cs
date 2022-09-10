using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.Journal;
using WizardMakerTests.Models.Virtues.VirtueCommands;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands.Tests
{
    [TestClass()]
    public class WealthyCommandTests
    {
        [TestMethod()]
        public void ExecuteTest()
        {
            int STARTING_AGE = 25;
            int SAGA_START = 1220;
            Character c = CommandTestUtilities.GenerateBasicTestCharacter(STARTING_AGE, SAGA_START);
            AddVirtueJournalEntry addVirtueJournalEntry = new AddVirtueJournalEntry(new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING), ArchVirtue.NameToArchVirtue["Wealthy"]);
            c.addJournalable(addVirtueJournalEntry);
            CharacterRenderer.renderAllJournalEntries(c);

            // Assert that the character has 20 XP and that the Later Life XP is updated accordingly.
            Assert.AreEqual(WealthyCommand.WEALTHY_XP, c.XpPerYear);

            foreach (XPPool xppool in c.XPPoolList)
            {
                if (xppool.name.Equals(NewCharacterInitJournalEntry.LATER_LIFE_POOL_NAME))
                {
                    Assert.AreEqual((STARTING_AGE - NewCharacterInitJournalEntry.CHILDHOOD_END_AGE) * WealthyCommand.WEALTHY_XP, xppool.initialXP);
                }
            }
        }
    }
}