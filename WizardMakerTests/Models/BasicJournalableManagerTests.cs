using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models.Tests
{
    [TestClass()]
    public class BasicJournalableManagerTests
    {
        // TODO: Convert to using DataRow
        [TestMethod()]
        
        public void addJournalableTest()
        {
            IJournalableManager journalableManager = new BasicJournalableManager();

            SeasonYear[] seasonYears = {
                new SeasonYear(1220, Season.SPRING),
                new SeasonYear(1220, Season.SUMMER),
                new SeasonYear(1221, Season.SPRING),
                new SeasonYear(1220, Season.WINTER),
                new SeasonYear(1220, Season.SPRING)
            };
            for (int i = 0; i < seasonYears.Length; i++) {
                journalableManager.addJournalable(new SingleJournalEntry("Random entry " + i, seasonYears[i]));
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
    }
}