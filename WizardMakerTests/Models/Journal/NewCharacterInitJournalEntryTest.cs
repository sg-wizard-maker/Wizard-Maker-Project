using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models;
using WizardMakerTestbed.Models;
using Newtonsoft.Json;

namespace WizardMakerTests.Models.Journal
{
    [TestClass]
    public class NewCharacterInitJournalEntryTest
    {
        [TestMethod]
        public void SimpleInitTest()
        {
            Character c = new Character("New Character", "Test new character", new List<AbilityInstance>(), new List<Journalable>(), 25);
            NewCharacterInitJournalEntry NewCharacterInitJournalEntry = new NewCharacterInitJournalEntry(25, ArchAbility.LangEnglish);

            // Normally, call cm.renderAllJournalEntries(), rather than executing the journal entry directly.
            NewCharacterInitJournalEntry.Execute(c);
            Assert.IsTrue(c.XPPoolList.Count() == 4);
        }

        [DataTestMethod()]
        [DataRow(20, 25)]
        [DataRow(20, 15)]
        [DataRow(15, 25)]
        [DataRow(15, 15)]
        [DataRow(15, 6)]
        [DataRow(10, 6)]
        [DataRow(10, 25)]
        public void XpPerSeasonTest(int xpPerSeason, int startingAge)
        {
            Character c = new Character("New Character", "Test new character", new List<AbilityInstance>(), new List<Journalable>(), startingAge);
            c.XpPerYear = xpPerSeason;

            NewCharacterInitJournalEntry NewCharacterInitJournalEntry = new NewCharacterInitJournalEntry(startingAge, ArchAbility.LangEnglish);

            // Normally, call cm.renderAllJournalEntries(), rather than executing the journal entry directly.
            NewCharacterInitJournalEntry.Execute(c);
            Assert.IsTrue(c.XPPoolList.Count() == 4);

            // Count the childhood language and childhood XP and the later life.
            Assert.AreEqual(NewCharacterInitJournalEntry.CHILDHOOD_LANGUAGE_XP + NewCharacterInitJournalEntry.CHILDHOOD_XP +
                (startingAge - NewCharacterInitJournalEntry.CHILDHOOD_END_AGE) * xpPerSeason, c.totalRemainingXPWithoutOverdrawn());
        }

        [TestMethod()]
        public void SerializableTest()
        {
            const int startingAge = 25;

            Journalable entry = new NewCharacterInitJournalEntry(startingAge, ArchAbility.LangEnglish);

            string tmp = entry.SerializeJson();
            Journalable deserialized = Journalable.DeserializeJson(tmp);


            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(tmp);

            // Note that if this is not calling the derived class IsSameSpecs, you will get erroneous passing of this test.  But
            //  this should not happen due to usage of GetType
            Assert.IsTrue(entry.IsSameSpecs(deserialized));
        }
    }
}
