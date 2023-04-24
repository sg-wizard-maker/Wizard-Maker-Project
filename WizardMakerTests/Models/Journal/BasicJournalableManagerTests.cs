using WizardMaker.DataDomain.Models;

namespace WizardMakerTests.Models.Journal;

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
            journalableManager.AddJournalable(new SimpleJournalEntry("Random entry " + i, seasonYears[i]));
        }
        SortedSet<Journalable> journalables = journalableManager.GetJournalables();
        Assert.AreEqual(seasonYears.Length, journalables.Count);

        int[] sortedIndexes = { 0, 4, 1, 3, 2 };
        for (int i = 0; i < sortedIndexes.Length; i++)
        {
            int index = sortedIndexes[i];
            Assert.AreEqual(seasonYears[index], journalables.ElementAt(i).GetDate());
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
            journalableManager.AddJournalable(journalable);
            if (i == 2)
            {
                idToRemove = journalable.Id;
            }
        }

        journalableManager.RemoveJournalEntry(idToRemove);

        Assert.AreEqual(3, journalableManager.GetJournalables().Count);
        for (int i = 0; i < 3; i++)
        {
            Assert.AreNotEqual(TEST_STRING + "2", journalableManager.GetJournalables().ElementAt(i).GetText());
        }

    }
}