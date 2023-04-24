using WizardMaker.DataDomain.Validation;

using WizardMakerTests.Models.Virtues.VirtueCommands;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands.Tests;

[TestClass()]
public class PrivilegedUpbringingCommandTests
{
    [TestMethod()]
    public void ExecuteTest()
    {
        // If we are going to test Validation, we must reset the ValidationLog.
        //  Otherwise, it will get polluted with validation messages from other tests.
        ValidationLog.Reset();

        Character c = CommandTestUtilities.GenerateBasicTestCharacterWithStartingVirtue(25, "PrivilegedUpbringing", 1220);
        CharacterRenderer.RenderAllJournalEntries(c);

        CommandTestUtilities.AssertXPPoolInitialization(c, PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_NAME,
            PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_DESCRIPTION, PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_XP);

        // Assert that the ability types are NOT available to the character generally.
        Assert.IsTrue(!c.AllowedAbilityTypes.Where(a => a == AbilityType.Martial).Any());
        Assert.IsTrue(!c.AllowedAbilityTypes.Where(a => a == AbilityType.Academic).Any());

        // Add a martial ability and rerender the character.
        XpAbilitySpendJournalEntry xpSpend = new XpAbilitySpendJournalEntry("Test XP Spend Great Weapon", new SeasonYear(1220 - 25, Season.SPRING),
            "Great Weapon", 25, "Poleaxe");
        c.AddJournalable(xpSpend);
        CharacterRenderer.RenderAllJournalEntries(c);

        // Assert that the XP Pool has been debited appropriately
        Assert.AreEqual(PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_XP - 25, c.XPPoolList.Where(x => x.Name.Equals(PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_NAME)).First().RemainingXP);
        Assert.AreEqual(PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_XP, c.XPPoolList.Where(x => x.Name.Equals(PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_NAME)).First().InitialXP);

        // Now add an academic ability and make sure the XP Pool was debited (make sure to include previous martial ability in test)
        XpAbilitySpendJournalEntry xpSpend2 = new XpAbilitySpendJournalEntry("Test XP Spend Artes Liberales", new SeasonYear(1220 - 25, Season.SPRING),
"Artes Liberales", 10, "Genomics");
        c.AddJournalable(xpSpend2);
        CharacterRenderer.RenderAllJournalEntries(c);

        Assert.AreEqual(PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_XP - 25 - 10, c.XPPoolList.Where(x => x.Name.Equals(PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_NAME)).First().RemainingXP);
        Assert.AreEqual(PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_XP, c.XPPoolList.Where(x => x.Name.Equals(PrivilegedUpbringingCommand.PRIVILEGED_UPBRINGING_XP_POOL_NAME)).First().InitialXP);

        // Assert that there were no validation errors
        // TODO: We cannot do that now, since we only rely on the allowed abilities. See issue 10 (https://github.com/sg-wizard-maker/Wizard-Maker-Project/issues/10).  Uncomment when issue is resolved.
        /**
        System.Console.WriteLine(ValidationLog.GetMessages().First().message);
        Assert.AreEqual(0, ValidationLog.GetMessages().Count);
        */
        // TODO: Test that spending on non-allowed ability types that are covered by this virtue only generate validation log messages
        //      when XP from another pool is used.  See issue 10 (https://github.com/sg-wizard-maker/Wizard-Maker-Project/issues/10)
    }
}