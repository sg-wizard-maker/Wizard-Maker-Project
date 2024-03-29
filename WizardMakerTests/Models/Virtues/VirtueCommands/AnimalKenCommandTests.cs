﻿using WizardMaker.DataDomain.Validation;

using WizardMakerTests.Models.Virtues.VirtueCommands;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands.Tests;

[TestClass()]
public class AnimalKenCommandTests
{
    [TestMethod()]
    public void ExecuteBasicTest()
    {
        // If we are going to test Validation, we must reset the ValidationLog.
        //  Otherwise, it will get polluted with validation messages from other tests.
        ValidationLog.Reset();

        int STARTING_AGE = 25;
        int SAGA_START = 1220;
        string virtueName = "AnimalKen";

        Character c = CommandTestUtilities.GenerateBasicTestCharacterWithStartingVirtue(STARTING_AGE, virtueName, SAGA_START);
        CharacterRenderer.RenderAllJournalEntries(c);
        Assert.IsTrue(c.AllowedAbilities.Where(a => a == ArchAbility.AnimalKen).Any());

        // Assert that there were no validation errors
        Assert.AreEqual(0, ValidationLog.GetMessages().Count);

        // Assert that the XP Pool was created and has the correct amount of XP (initial and remaining)
        Assert.IsTrue(c.XPPoolList.Where(x => x.Name.Equals(ArchAbility.AnimalKen.Name + GenericGrantAbilityCommand.XPPOOL_NAME_SUFFIX)).Any());
        Assert.AreEqual(0, c.XPPoolList.Where(x => x.Name.Equals(ArchAbility.AnimalKen.Name + GenericGrantAbilityCommand.XPPOOL_NAME_SUFFIX)).First().RemainingXP);

    }
}