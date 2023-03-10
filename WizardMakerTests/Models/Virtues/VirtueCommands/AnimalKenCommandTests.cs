using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMaker.DataDomain.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTests.Models.Virtues.VirtueCommands;
using WizardMaker.DataDomain.Validation;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands.Tests
{
    [TestClass()]
    public class AnimalKenCommandTests
    {
        [TestMethod()]
        public void ExecuteBasicTest()
        {
            // If we are going to test Validation, we must reset the ValidationLog.
            //  Otherwise, it will get polluted with validation messages from other tests.
            ValidationLog.reset();

            int STARTING_AGE = 25;
            int SAGA_START = 1220;
            string virtueName = "AnimalKen";

            Character c = CommandTestUtilities.GenerateBasicTestCharacterWithStartingVirtue(STARTING_AGE, virtueName, SAGA_START);
            CharacterRenderer.RenderAllJournalEntries(c);
            Assert.IsTrue(c.AllowedAbilities.Where(a => a == ArchAbility.AnimalKen).Any());

            // Assert that there were no validation errors
            Assert.AreEqual(0, ValidationLog.GetMessages().Count);

            // Assert that the XP Pool was created and has the correct amount of XP (initial and remaining)
            Assert.IsTrue(c.XPPoolList.Where(x => x.name.Equals(ArchAbility.AnimalKen.Name + GenericGrantAbilityCommand.XPPOOL_NAME_SUFFIX)).Any());
            Assert.AreEqual(0, c.XPPoolList.Where(x => x.name.Equals(ArchAbility.AnimalKen.Name + GenericGrantAbilityCommand.XPPOOL_NAME_SUFFIX)).First().remainingXP);

        }
    }
}