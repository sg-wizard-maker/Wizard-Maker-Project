﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTests.Models.Virtues.VirtueCommands;
using WizardMakerPrototype.Validation;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands.Tests
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

            CommandTestUtilities.AssertXPPoolInitialization(c, ArchAbility.AnimalKen + GenericGrantAbilityCommand.XPPOOL_NAME_SUFFIX, "", 5);

        }
    }
}