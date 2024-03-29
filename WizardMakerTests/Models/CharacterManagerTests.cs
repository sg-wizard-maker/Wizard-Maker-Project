﻿namespace WizardMaker.DataDomain.Models.Tests;

[TestClass()]
public class CharacterManagerTests
{
    [DataTestMethod()]
    [DataRow("Bows", 75, 50, "Short Bow", 4)]
    [DataRow("Bows", 55, 50, "Short Bow", 4)]
    [DataRow("Bows", 15, 50, "Short Bow", 4)]
    [DataRow("Bows", 15, 10, "Short Bow", 1)]
    [DataRow("Bows", 15,  0, "Short Bow", 0)]
    public void UpdateAbilityTwiceWithAbsoluteXpTest(string n, int xp1, int xp2, string s, int expectedScore)
    {
        CharacterManager cm = new(25, ArchAbility.LangEnglish);

        int initialXP = cm.TotalRemainingXPWithoutOverdrawn();

        cm.UpdateAbilityDuringCreation(n, xp1, s);
        cm.UpdateAbilityDuringCreation(n, xp2, s);

        // We expect that one of the first entry has been overwritten by the second.
        // And since there is two there by default (starting character + starting language), we expect 3.
        Assert.AreEqual(3, cm.GetJournalSize());

        // Render the character in a way we can easily run our assertions.
        CharacterData cd = cm.RenderCharacterAsCharacterData();
        Assert.IsNotNull(cd);
        Assert.AreEqual(2, cd.Abilities.Count);

        AbilityInstanceData testedAbility = cd.Abilities.Where(a => a.Name == n).First();

        Assert.IsTrue(cd.Abilities.Select(a => a.Name.Equals(n)).ToList().Count > 0);
        Assert.AreEqual(xp2,           testedAbility.XP);
        Assert.AreEqual(s,             testedAbility.Specialty);
        Assert.AreEqual(expectedScore, testedAbility.Score);

        // We should only have one Bow ability listed.
        Assert.AreEqual(1, cd.Abilities.Count(a => a.Name == n));

        // Test correct amount of XP was spent.
        Assert.AreEqual(initialXP - xp2, cm.TotalRemainingXPWithoutOverdrawn());
    }

    [TestMethod()]
    public void SimpleRoundTripFileTest()
    {
        Character c1 = new("Foo", "Looks like a foo", 25);
        NewCharacterInitJournalEntry initEntry = new NewCharacterInitJournalEntry(25, ArchAbility.LangEnglish);
        c1.AddJournalable(initEntry);

        string tempPath = Path.GetTempFileName();
        CharacterManager.WriteToFile(c1, tempPath);

        Character c2 = CharacterManager.ReadFromFile(tempPath);

        CharacterRenderer.RenderAllJournalEntries(c1);
        CharacterRenderer.RenderAllJournalEntries(c2);

        Assert.AreEqual(c1.Name,               c2.Name);
        Assert.AreEqual(c1.Description,        c2.Description);
        Assert.AreEqual(c1.Abilities.Count,    c2.Abilities.Count);
        Assert.AreEqual(c1.GetJournal().Count, c2.GetJournal().Count);
        Assert.IsTrue(c1.GetJournal().Count > 0);

        for (int i=0; i<c2.GetJournal().Count; i++)
        {
            Assert.IsTrue(c2.GetJournal().ElementAt(i).IsSameSpecs(c1.GetJournal().ElementAt(i)));
        }
    }
}