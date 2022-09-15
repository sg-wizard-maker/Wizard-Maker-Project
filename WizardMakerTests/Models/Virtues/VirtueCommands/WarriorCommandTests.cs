using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTests.Models.Virtues.VirtueCommands;
using WizardMakerPrototype.Models.Journal;
using WizardMakerPrototype.Validation;

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
            string virtueName = "Warrior";

            // If we are going to test Validation, we must reset the ValidationLog.
            //  Otherwise, it will get polluted with validation messages from other tests.
            ValidationLog.reset();

            // Create a Warrior character
            Character c = CommandTestUtilities.GenerateBasicTestCharacterWithStartingVirtue(STARTING_AGE, virtueName, SAGA_START);

            CharacterRenderer.RenderAllJournalEntries(c);

            CommandTestUtilities.AssertXPPoolInitialization(c, WarriorCommand.WARRIOR_POOL_NAME, WarriorCommand.WARRIOR_POOL_DESCRIPTION, WarriorCommand.WARRIOR_POOL_INITIAL_XP);

            // Assert that the AbilityType.Martial is allowed
            Assert.IsTrue(c.AllowedAbilityTypes.Where(a => a == AbilityType.Martial).Any());

            // Assert that there were no validation errors
            Assert.AreEqual(0, ValidationLog.GetMessages().Count);

            // Add a martial ability and rerender the character.
            XpAbilitySpendJournalEntry xpSpend = new XpAbilitySpendJournalEntry("Test XP Spend", new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING),
                "Great Weapon", 25, "Poleaxe");
            c.addJournalable(xpSpend);
            CharacterRenderer.RenderAllJournalEntries(c);

            // Assert that the Warrior XP Pool has been debited appropriately
            Assert.AreEqual(WarriorCommand.WARRIOR_POOL_INITIAL_XP - 25, c.XPPoolList.Where(x => x.name.Equals(WarriorCommand.WARRIOR_POOL_NAME)).First().remainingXP);
            Assert.AreEqual(WarriorCommand.WARRIOR_POOL_INITIAL_XP, c.XPPoolList.Where(x => x.name.Equals(WarriorCommand.WARRIOR_POOL_NAME)).First().initialXP);

            // Assert that there were no validation errors
            Assert.AreEqual(0, ValidationLog.GetMessages().Count);
        }


    }
}