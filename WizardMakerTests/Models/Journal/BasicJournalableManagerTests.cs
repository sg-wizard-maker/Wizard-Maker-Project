using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerTests.Models.Journal
{
    [TestClass()]
    public class BasicJournalableManagerTests
    {
        // TODO: Convert to using DataRow
        [TestMethod()]

        public void AddJournalableTest()
        {
            IJournalableManager journalableManager = new BasicJournalableManager();

            SeasonYear[] seasonYears = {
                new SeasonYear(1220, Season.SPRING),
                new SeasonYear(1220, Season.SUMMER),
                new SeasonYear(1221, Season.SPRING),
                new SeasonYear(1220, Season.WINTER),
                new SeasonYear(1220, Season.SPRING)
            };
            for (int i = 0; i < seasonYears.Length; i++)
            {
                journalableManager.addJournalable(new SimpleJournalEntry("Random entry " + i, seasonYears[i]));
            }
            SortedSet<Journalable> journalables = journalableManager.getJournalables();
            Assert.AreEqual(seasonYears.Length, journalables.Count);

            int[] sortedIndexes = { 0, 4, 1, 3, 2 };
            for (int i = 0; i < sortedIndexes.Length; i++)
            {
                int index = sortedIndexes[i];
                Assert.AreEqual(seasonYears[index], journalables.ElementAt(i).getDate());
            }
        }

        [TestMethod()]
        public void RemoveJournalableTest()
        {
            IJournalableManager journalableManager = new BasicJournalableManager();
            string TEST_STRING = "ENTRY_";
            string idToRemove = "THIS IS DEFINITELY WRONG";
            for (int i = 0; i < 4; i++)
            {
                SimpleJournalEntry journalable = new SimpleJournalEntry(TEST_STRING + i, new SeasonYear(1220, Season.SPRING));
                journalableManager.addJournalable(journalable);
                if (i == 2)
                {
                    idToRemove = journalable.Id;
                }
            }

            journalableManager.removeJournalEntry(idToRemove);

            Assert.AreEqual(3, journalableManager.getJournalables().Count);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreNotEqual(TEST_STRING + "2", journalableManager.getJournalables().ElementAt(i).getText());
            }

        }
    }
}