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

            // Assert that the XP Pool was created and has the correct amount of XP (initial and remaining)
            Assert.IsTrue(c.XPPoolList.Where(x => x.name.Equals(WarriorCommand.WARRIOR_POOL_NAME)).Any());
            Assert.AreEqual(WarriorCommand.WARRIOR_POOL_INITIAL_XP, c.XPPoolList.Where(x => x.name.Equals(WarriorCommand.WARRIOR_POOL_NAME)).First().remainingXP);
            Assert.AreEqual(WarriorCommand.WARRIOR_POOL_INITIAL_XP, c.XPPoolList.Where(x => x.name.Equals(WarriorCommand.WARRIOR_POOL_NAME)).First().initialXP);

            // Assert that the AbilityType.Martial is allowed
            Assert.IsTrue(c.AllowedAbilityTypes.Where(a => a == AbilityType.Martial).Any());

            // TODO: Assert that there were no validation errors

            // Add a martial ability and rerender the character.
            XpAbilitySpendJournalEntry xpSpend = new XpAbilitySpendJournalEntry("Test XP Spend", new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING),
                "Great Weapon", 25, "Poleaxe");
            c.addJournalable(xpSpend);
            CharacterRenderer.RenderAllJournalEntries(c);

            // Assert that the Warrior XP Pool has been debited appropriately
            Assert.AreEqual(WarriorCommand.WARRIOR_POOL_INITIAL_XP-25, c.XPPoolList.Where(x => x.name.Equals(WarriorCommand.WARRIOR_POOL_NAME)).First().remainingXP);
            Assert.AreEqual(WarriorCommand.WARRIOR_POOL_INITIAL_XP, c.XPPoolList.Where(x => x.name.Equals(WarriorCommand.WARRIOR_POOL_NAME)).First().initialXP);

            // TODO: Assert that there were no validation errors
        }
    }
}