using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models.Virtues.VirtueCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.Journal;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands.Tests
{
    [TestClass()]
    public class PuissantAbilityCommandTests
    {
        [TestMethod()]
        public void ExecuteSimpleTest()
        {
            int STARTING_AGE = 25;
            int SAGA_START = 1220;
            string ABILITY = "Brawl";
            // Create a wealthy character
            Character c = new("Foo", "Looks like a wealthy foo", STARTING_AGE);
            NewCharacterInitJournalEntry initEntry = new NewCharacterInitJournalEntry(25, ArchAbility.LangEnglish);
            AddVirtueJournalEntry addVirtueJournalEntry = new AddVirtueJournalEntry(new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING),
                ArchVirtue.NameToArchVirtue[ArchVirtue.PUISSANT_PREFIX + ABILITY]);
            XpAbilitySpendJournalEntry xpSpend = new XpAbilitySpendJournalEntry("XP spent on Brawl", new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING), ABILITY, 15, "Fist");
            c.addJournalable(initEntry);
            c.addJournalable(addVirtueJournalEntry);
            c.addJournalable(xpSpend);
            CharacterRenderer.renderAllJournalEntries(c);

            Assert.IsTrue(c.abilities.Where(a => a.Name == ABILITY).First().HasPuissance);

            // Please note that we still treat the score as if it was 2, not 4.  This is because we need to render it later as "2+2", not "4".
            Assert.AreEqual(2, c.abilities.Where(a => a.Name == ABILITY).First().Score);

            
        }

        // TODO: Add test where no XP has been spent
    }
}