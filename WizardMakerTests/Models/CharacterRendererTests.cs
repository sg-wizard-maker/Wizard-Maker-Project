using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMaker.DataDomain.Models;
using WizardMaker.DataDomain.Models.Journal;
using WizardMaker.DataDomain.Models.Virtues;
using WizardMaker.DataDomain.Models.Virtues;

namespace WizardMakerTests.Models;

[TestClass]
public class CharacterRendererTests
{
    private const string XP_POOL_NAME = "dummy";
    private const string XP_POOL_DESCRIPTION = "dummy XP Pool";
    private const int XP_POOL_INITIAL_XP = 500;

    [TestMethod]
    public void RenderTwiceTest()
    {
        int STARTING_AGE = 25;
        int SAGA_START = 1220;
        SeasonYear initSeasonYear = new SeasonYear(SAGA_START - STARTING_AGE, Season.SPRING);
        // Create a wealthy character
        Character c = new("Foo", "Looks like a wealthy foo", STARTING_AGE);
        NewCharacterInitJournalEntry initEntry = new NewCharacterInitJournalEntry(25, ArchAbility.LangEnglish);
        AddVirtueJournalEntry addVirtueJournalEntry = new AddVirtueJournalEntry(initSeasonYear, ArchVirtue.NameToArchVirtue["Wealthy"]);
        c.AddJournalable(initEntry);
        c.AddJournalable(addVirtueJournalEntry);
        c.AddJournalable(new XpAbilitySpendJournalEntry("Ability spend Brawl", initSeasonYear, "Brawl", 25, "Fists"));
        CharacterRenderer.RenderAllJournalEntries(c);
        int initialXPPoolCount = c.XPPoolList.Count;
        int initialXPSpend = c.TotalRemainingXPWithoutOverdrawn();
        Assert.AreEqual(1, c.Abilities.Count);
        Assert.AreEqual(1, c.Virtues.Count);

        // Render again and see that counts do not change
        Assert.AreEqual(1, c.Abilities.Count);
        Assert.AreEqual(1, c.Virtues.Count);
        Assert.AreEqual(initialXPPoolCount, c.XPPoolList.Count);
        Assert.AreEqual(initialXPSpend, c.TotalRemainingXPWithoutOverdrawn());
    }

    // We expect the results going in to be the same as coming out.
    [DataTestMethod()]
    [DataRow("Bows", 75, "Short Bow", 5)]
    [DataRow("Bows", 55, "Short Bow", 4)]
    [DataRow("Brawl", 25, "Fist", 2)]
    public void AddAbilityTest(string n, int xp, string s, int expectedScore)
    {
        Character c = new ("Foo", "Looks like a foo", 25);
        c.XPPoolList.Add(new BasicXPPool(XP_POOL_NAME, XP_POOL_DESCRIPTION, XP_POOL_INITIAL_XP));
        CharacterRenderer.AddAbility(c, n, xp, s, c.IsInitialCharacterFinished(), "dummyID");
        CharacterData characterData = CharacterRenderer.RenderCharacterAsCharacterData(c);

        // Language abillity is added automatically.
        Assert.IsNotNull(characterData);
        Assert.AreEqual(1, characterData.Abilities.Count);
        Assert.AreEqual(1, characterData.XPPools.Count);
        Assert.AreEqual(n, characterData.Abilities[0].Name);
        Assert.AreEqual(xp, characterData.Abilities[0].XP);
        Assert.AreEqual(s, characterData.Abilities[0].Specialty);
        Assert.AreEqual(expectedScore, characterData.Abilities[0].Score);

        foreach (XPPoolData xppool in characterData.XPPools)
        {
            Assert.AreEqual(XP_POOL_NAME, xppool.Name);
            Assert.AreEqual(XP_POOL_DESCRIPTION, xppool.Description);
            Assert.AreEqual(XP_POOL_INITIAL_XP, xppool.InitialXP);
            Assert.AreEqual(XP_POOL_INITIAL_XP - xp, xppool.RemainingXP);
        }
    }

    [TestMethod]
    public void AbilitiesRenderingTimingTest()
    {
        Random rnd = new Random(1212);
        const int STARTING_AGE = 25;

        // Look at the timing for rendering an entire character.
        Character c = new("Foo", "Best mage ever.  They really know a lot.", STARTING_AGE);

        NewCharacterInitJournalEntry initEntry = new NewCharacterInitJournalEntry(STARTING_AGE, ArchAbility.LangEnglish);
        c.AddJournalable(initEntry);

        Dictionary<string, int> xpSpentMap = new Dictionary<string, int>();

        const int ENTRIES_PER_ABILITY = 50;
        const int XP_PER_ENTRY = 5;

        for (int i = 0; i < ENTRIES_PER_ABILITY; i++)
        {

            foreach (ArchAbility archAbility in ArchAbility.AllCommonAbilities)
            {
                int jitter = rnd.Next(0, 5);

                XpAbilitySpendJournalEntry xpAbilitySpend = new XpAbilitySpendJournalEntry("Spend on " + archAbility.Name, new SeasonYear(1220, Season.SPRING),
                    archAbility.Name, XP_PER_ENTRY + jitter, "Dummy specialty");
                c.AddJournalable(xpAbilitySpend);

                // Track the XP spending as we go.
                if (xpSpentMap.ContainsKey(archAbility.Name))
                {
                    xpSpentMap[archAbility.Name] = xpSpentMap[archAbility.Name] + XP_PER_ENTRY + jitter;
                }
                else
                {
                    xpSpentMap.Add(archAbility.Name, XP_PER_ENTRY + jitter);
                }
            }
            if (i == 0)
            {
                c.EndCharacterCreation();
            }
        }

        // Start a timer
        DateTime timeStart = DateTime.Now;

        // Render the character as a CharacterData
        CharacterRenderer.RenderAllJournalEntries(c);
        CharacterData cd = CharacterRenderer.RenderCharacterAsCharacterData(c);

        // Serialize the CharacterData
        string foo = CharacterRenderer.SerializeCharacterData(cd);

        // Stop the timer
        DateTime timeEnd = DateTime.Now;

        // Output some useful information to the console.
        long elapsedTicks = timeEnd.Ticks - timeStart.Ticks;
        TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
        Console.WriteLine("   {0:N0} nanoseconds", elapsedTicks * 100);
        Console.WriteLine("   {0:N0}", cd.Abilities.Count);

        // Do some basic testing that the XP spends are as expected.
        Assert.AreEqual(ArchAbility.AllCommonAbilities.Count, cd.Abilities.Count);
        for (int i = 0; i < cd.Abilities.Count; i++)
        {
            Assert.AreEqual(xpSpentMap.GetValueOrDefault(cd.Abilities[i].Name, -1), cd.Abilities[i].XP);
        }

        Console.WriteLine(foo);
    }
}


