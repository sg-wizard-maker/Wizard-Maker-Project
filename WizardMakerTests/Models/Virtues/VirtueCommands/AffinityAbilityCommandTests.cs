using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMaker.DataDomain.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMaker.DataDomain.Models.Journal;
using WizardMakerTests.Models.Virtues.VirtueCommands;   

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands.Tests;

[TestClass()]
public class AffinityAbilityCommandTests
{
    [TestMethod()]
    public void ExecuteSimpleTest()
    {
        int STARTING_AGE = 25;
        string ABILITY = "Brawl";
        int SAGA_START = 1220;
        string virtueName = ArchVirtue.AFFINITY_PREFIX + ABILITY;

        Character c = CommandTestUtilities.GenerateBasicTestCharacterWithStartingVirtue(STARTING_AGE, virtueName, SAGA_START);

        XpAbilitySpendJournalEntry xpSpend = new XpAbilitySpendJournalEntry("XP spent on Brawl", new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING), ABILITY, 
            20, "Fist");
        c.AddJournalable(xpSpend);
 
        CharacterRenderer.RenderAllJournalEntries(c);

        Assert.IsTrue(c.Abilities.Where(a => a.Name == ABILITY).First().HasAffinity);

        // Please note that we still treat the score as if it was 2, not 4.  This is because we need to render it later as "2+2", not "4".
        Assert.AreEqual(3, c.Abilities.Where(a => a.Name == ABILITY).First().Score);
    }
}