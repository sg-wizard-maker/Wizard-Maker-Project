using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.Journal;
using WizardMakerTests.Models.Virtues.VirtueCommands;   

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands.Tests
{
    [TestClass()]
    public class AffinityAbilityCommandTests
    {
        [TestMethod()]
        public void ExecuteSimpleTest()
        {
            int STARTING_AGE = 25;
            string ABILITY = "Brawl";
            int SAGA_START = 1220;
            // Create a  character
            Character c = CommandTestUtilities.GenerateBasicTestCharacter(STARTING_AGE);
            AddVirtueJournalEntry virtueJournalEntry = new AddVirtueJournalEntry(new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING),
                ArchVirtue.NameToArchVirtue[ArchVirtue.AFFINITY_PREFIX + ABILITY]);
            XpAbilitySpendJournalEntry xpSpend = new XpAbilitySpendJournalEntry("XP spent on Brawl", new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING), ABILITY, 
                20, "Fist");
            c.addJournalable(xpSpend);
            c.addJournalable(virtueJournalEntry);
            CharacterRenderer.RenderAllJournalEntries(c);

            Assert.IsTrue(c.abilities.Where(a => a.Name == ABILITY).First().HasAffinity);

            // Please note that we still treat the score as if it was 2, not 4.  This is because we need to render it later as "2+2", not "4".
            Assert.AreEqual(3, c.abilities.Where(a => a.Name == ABILITY).First().Score);
        }
    }
}