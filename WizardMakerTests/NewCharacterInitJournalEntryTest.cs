using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models;
using WizardMakerTestbed.Models;
using Newtonsoft.Json;
namespace WizardMakerTests
{
    [TestClass]
    public class NewCharacterInitJournalEntryTest
    {
        [TestMethod]
        public void SimpleInitTest()
        {
            Character c = new Character("New Character", "Test new character", new List<AbilityInstance>(), new List<IJournalable>(), 25) ;
            NewCharacterInitJournalEntry NewCharacterInitJournalEntry = new NewCharacterInitJournalEntry(c, 25, ArchAbility.LangEnglish, 15);
            
            // Normally, call cm.renderAllJournalEntries(), rather than executing the journal entry directly.
            NewCharacterInitJournalEntry.Execute();
            Assert.IsTrue(c.XPPoolList.Count() == 4);
        }

        [DataTestMethod()]
        [DataRow(20,25)]
        [DataRow(20,15)]
        [DataRow(15,25)]
        [DataRow(15,15)]
        [DataRow(15,6)]
        [DataRow(10,6)]
        [DataRow(10,25)]
        public void XpPerSeasonTest(int xpPerSeason, int startingAge)
        {

            Character c = new Character("New Character", "Test new character", new List<AbilityInstance>(), new List<IJournalable>(), startingAge);

            NewCharacterInitJournalEntry NewCharacterInitJournalEntry = new NewCharacterInitJournalEntry(c, startingAge, ArchAbility.LangEnglish, xpPerSeason);

            // Normally, call cm.renderAllJournalEntries(), rather than executing the journal entry directly.
            NewCharacterInitJournalEntry.Execute();
            Assert.IsTrue(c.XPPoolList.Count() == 4);

            // Count the childhood language and childhood XP and the later life.
            Assert.AreEqual(NewCharacterInitJournalEntry.CHILDHOOD_LANGUAGE_XP + NewCharacterInitJournalEntry.CHILDHOOD_XP +
                ((startingAge-NewCharacterInitJournalEntry.CHILDHOOD_END_AGE)*xpPerSeason) , c.totalRemainingXPWithoutOverdrawn());
        }
    }
}
