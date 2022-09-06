using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.Journal;

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
            // Create a wealthy character
            Character c = new("Foo", "Looks like a foo", STARTING_AGE);
            NewCharacterInitJournalEntry initEntry = new NewCharacterInitJournalEntry(25, ArchAbility.LangEnglish, 15);
            AddVirtueJournalEntry addVirtueJournalEntry = new AddVirtueJournalEntry(new SeasonYear(SAGA_START-STARTING_AGE, Season.SPRING), ArchVirtue.Wealthy);
            c.addJournalable(initEntry);
            c.addJournalable(addVirtueJournalEntry);
            CharacterRenderer.renderAllJournalEntries(c);

            // Assert that the character has 20 XP and that the Later Life XP is updated accordingly.
            Assert.AreEqual(20, c.XpPerSeasonForInitialCreation);

            
        }
    }
}