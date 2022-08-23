using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Tests
{
    [TestClass()]
    public class CharacterManagerTests
    {
       

        [DataTestMethod()]
        [DataRow("Bows", 75, 50, "Short Bow", 4)]
        [DataRow("Bows", 55, 50, "Short Bow", 4)]
        [DataRow("Bows", 15, 50, "Short Bow", 4)]
        [DataRow("Bows", 15, 10, "Short Bow", 1)]
        [DataRow("Bows", 15, 0, "Short Bow", 0)]
        public void UpdateAbilityTwiceWithAbsoluteXpTest(string n, int xp1, int xp2, string s, int expectedScore)
        {
            CharacterManager cm = new (25);

            int initialXP = cm.totalRemainingXPWithoutOverdrawn();
            
            cm.updateAbilityDuringCreation(n, xp1, s);
            cm.updateAbilityDuringCreation(n, xp2, s);
            
            // We expect that one of the first entry has been overwritten by the second.
            // And since there is two there by default (starting character + starting language), we expect 3.
            Assert.AreEqual(3, cm.getJournalSize());

            // Render the character in a way we can easily run our assertions.
            CharacterData cd = cm.renderCharacterAsCharacterData();
            Assert.IsNotNull(cd);
            Assert.AreEqual(2, cd.Abilities.Count);
            Assert.AreEqual(n, cd.Abilities[0].Name);
            Assert.AreEqual(xp2, cd.Abilities[0].XP);
            Assert.AreEqual(s, cd.Abilities[0].Specialty);
            Assert.AreEqual(expectedScore, cd.Abilities[0].Score);
            
            // We should only have one Bow ability listed.
            Assert.AreEqual(1, cd.Abilities.Count(a => a.Name == n));

            // Test correct amount of XP was spent.
            Assert.AreEqual(initialXP - xp2, cm.totalRemainingXPWithoutOverdrawn());
        }
    }


}