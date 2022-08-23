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
            CharacterRenderer.addAbility(c, n, xp, s);
            CharacterData cd = CharacterRenderer.renderCharacterAsCharacterData(c);

            // Language abillity is added automatically.
            Assert.IsNotNull(cd);
            Assert.AreEqual(1, cd.Abilities.Count);
            Assert.AreEqual(n, cd.Abilities[0].Name);
            Assert.AreEqual(xp, cd.Abilities[0].XP);
            Assert.AreEqual(s, cd.Abilities[0].Specialty);
            Assert.AreEqual(expectedScore, cd.Abilities[0].Score);
        }
    }
}
