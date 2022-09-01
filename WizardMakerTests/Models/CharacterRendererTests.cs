using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models;

namespace WizardMakerTests.Models
{
    [TestClass]
    public class CharacterRendererTests
    {
        // We expect the results going in to be the same as coming out.
        [DataTestMethod()]
        [DataRow("Bows", 75, "Short Bow", 5)]
        [DataRow("Bows", 55, "Short Bow", 4)]
        [DataRow("Brawl", 25, "Fist", 2)]
        public void AddAbilityTest(string n, int xp, string s, int expectedScore)
        {
            Character c = new ("Foo", "Looks like a foo", 25);
            c.XPPoolList.Add(new BasicXPPool("dummy", "dummy XP Pool", 500));
            CharacterRenderer.addAbility(c, n, xp, s, c.IsInitialCharacterFinished());
            CharacterData characterData = CharacterRenderer.renderCharacterAsCharacterData(c);

            // Language abillity is added automatically.
            Assert.IsNotNull(characterData);
            Assert.AreEqual(1, characterData.Abilities.Count);
            Assert.AreEqual(n, characterData.Abilities[0].Name);
            Assert.AreEqual(xp, characterData.Abilities[0].XP);
            Assert.AreEqual(s, characterData.Abilities[0].Specialty);
            Assert.AreEqual(expectedScore, characterData.Abilities[0].Score);
        }

        [TestMethod]
        public void AbilitiesRenderingTimingTest()
        {
            Random rnd = new Random(1212);
            const int STARTING_AGE = 25;
            const int XP_PER_SEASON = 20;
            // Look at the timing for rendering an entire character.
            Character c = new("Foo", "Looks like a foo", STARTING_AGE);

            NewCharacterInitJournalEntry initEntry = new NewCharacterInitJournalEntry(STARTING_AGE, ArchAbility.LangEnglish, XP_PER_SEASON);
            c.addJournalable(initEntry);

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
                    c.addJournalable(xpAbilitySpend);
                    xpSpentMap.Add(archAbility.Name, xpSpentMap.GetValueOrDefault(archAbility.Name, 0) + XP_PER_ENTRY + jitter);
                }
                if (i == 0)
                {
                    c.EndCharacterCreation();
                }
            }

            DateTime timeStart = DateTime.Now;
            CharacterRenderer.renderAllJournalEntries(c);
            CharacterData cd = CharacterRenderer.renderCharacterAsCharacterData(c);
            
            DateTime timeEnd = DateTime.Now;
            long elapsedTicks = timeEnd.Ticks - timeStart.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            Console.WriteLine("   {0:N0} nanoseconds", elapsedTicks * 100);
            Console.WriteLine("   {0:N0}", cd.Abilities.Count);

            Assert.AreEqual(ArchAbility.AllCommonAbilities.Count, cd.Abilities.Count);
            for (int i = 0; i < cd.Abilities.Count; i++)
            {
                Assert.AreEqual(xpSpentMap.GetValueOrDefault(cd.Abilities[i].Name, -1), cd.Abilities[i].XP);
            }
        }
    }

   
}
