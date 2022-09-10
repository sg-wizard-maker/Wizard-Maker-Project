using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTests.Models.Virtues.VirtueCommands;
using WizardMakerPrototype.Models.Journal;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands.Tests
{
    [TestClass()]
    public class WarriorCommandTests
    {
        [TestMethod()]
        public void ExecuteSimpleTest()
        {
            int STARTING_AGE = 25;
            int SAGA_START = 1220;
            
            // Create a Warrior character
            Character c = CommandTestUtilities.GenerateBasicTestCharacter(STARTING_AGE);
            AddVirtueJournalEntry virtueJournalEntry = new AddVirtueJournalEntry(new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING),
                ArchVirtue.NameToArchVirtue["Warrior"]);
            c.addJournalable(virtueJournalEntry);
            CharacterRenderer.RenderAllJournalEntries(c);

            // TODO: Assert that the Warrior pool is there.
            // TODO: Assert that the Warrior pool has 50XP.
            // TODO: Assert that the AbilityType.Martial is allowed
            // TODO: Assert that there were no validation errors

            // TODO: Add a martial ability and rerender the character.
            // TODO: Assert that the Warrior XP Pool has been debited appropriately
            // TODO: Assert that there were no validation errors
        }
    }
}