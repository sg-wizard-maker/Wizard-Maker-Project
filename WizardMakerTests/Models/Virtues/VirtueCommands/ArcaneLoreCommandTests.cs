using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMaker.DataDomain.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTests.Models.Virtues.VirtueCommands;
using WizardMaker.DataDomain.Models.Journal;
using WizardMaker.DataDomain.Validation;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands.Tests
{
    [TestClass()]
    public class ArcaneLoreCommandTests
    {
        [TestMethod()]
        public void ExecuteSimpleTest()
        {
            int STARTING_AGE = 25;
            int SAGA_START = 1220;
            string virtueName = "ArcaneLore";

            // If we are going to test Validation, we must reset the ValidationLog.
            //  Otherwise, it will get polluted with validation messages from other tests.
            ValidationLog.Reset();

           
            Character c = CommandTestUtilities.GenerateBasicTestCharacterWithStartingVirtue(STARTING_AGE, virtueName, SAGA_START);
            CharacterRenderer.RenderAllJournalEntries(c);

            CommandTestUtilities.AssertXPPoolInitialization(c, ArcaneLoreCommand.ARCANE_LORE_POOL_NAME, 
                ArcaneLoreCommand.ARCANE_LORE_POOL_DESCRIPTION, ArcaneLoreCommand.ARCANE_LORE_INITIAL_XP);

            // Assert that the AbilityType.Martial is allowed
            Assert.IsTrue(c.AllowedAbilityTypes.Where(a => a == AbilityType.Arcane).Any());

            // Assert that there were no validation errors
            Assert.AreEqual(0, ValidationLog.GetMessages().Count);

            // Add an arcane ability and rerender the character.
            XpAbilitySpendJournalEntry xpSpend = new XpAbilitySpendJournalEntry("Test XP Spend", new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING),
                "Magic Lore", 25, "Beasts");
            c.AddJournalable(xpSpend);
            CharacterRenderer.RenderAllJournalEntries(c);

            // Assert that the Arcane XP Pool has been debited appropriately
            Assert.AreEqual(ArcaneLoreCommand.ARCANE_LORE_INITIAL_XP - 25, c.XPPoolList.Where(x => x.Name.Equals(ArcaneLoreCommand.ARCANE_LORE_POOL_NAME)).First().RemainingXP);
            Assert.AreEqual(ArcaneLoreCommand.ARCANE_LORE_INITIAL_XP, c.XPPoolList.Where(x => x.Name.Equals(ArcaneLoreCommand.ARCANE_LORE_POOL_NAME)).First().InitialXP);

            // Add a non arcane ability and rerender the character.
            XpAbilitySpendJournalEntry xpSpend2 = new XpAbilitySpendJournalEntry("Test XP Spend on non-arcane", new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING),
                "Brawl", 25, "Fists");
            c.AddJournalable(xpSpend2);
            CharacterRenderer.RenderAllJournalEntries(c);

            // Assert that the Arcane XP Pool was not debited on this second skill spend.
            Assert.AreEqual(ArcaneLoreCommand.ARCANE_LORE_INITIAL_XP - 25, c.XPPoolList.Where(x => x.Name.Equals(ArcaneLoreCommand.ARCANE_LORE_POOL_NAME)).First().RemainingXP);
            Assert.AreEqual(ArcaneLoreCommand.ARCANE_LORE_INITIAL_XP, c.XPPoolList.Where(x => x.Name.Equals(ArcaneLoreCommand.ARCANE_LORE_POOL_NAME)).First().InitialXP);

            // Assert that there were no validation errors
            Assert.AreEqual(0, ValidationLog.GetMessages().Count);
        }
    }
}