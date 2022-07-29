using WizardMakerPrototype.Models;
using WizardMakerTestbed.Models;

namespace WizardMakerTests
{
    [TestClass]
    public class XPPoolTest
    {
        [TestMethod]
        public void TestBasicXPPool()
        {
            XPPool xpPool = new BasicXPPool("Name", "description", 100);

            Assert.IsNotNull(xpPool);
        }

        [TestMethod]
        public void TestBasicXPPoolSimpleSpend()
        {
            XPPool xpPool = new BasicXPPool("Name", "description", 100);
            xpPool.spendXP(50);
            Assert.IsTrue(xpPool.remainingXP == 50);
        }

        [TestMethod]
        public void TestBasicXPPoolSimpleFailure()
        {
            XPPool xpPool = new BasicXPPool("Name", "description", 100);
            Assert.ThrowsException<XPPoolOverdrawnException>(() => xpPool.spendXP(150));
        }

        [TestMethod]
        public void TestBasicXpPoolAllowsAllAbilities()
        {
            XPPool xpPool = new BasicXPPool("Name", "description", 100);

            List<string> errorMessages = new List<string>();
            foreach (ArchAbility archAbility in ArchAbility.AllCommonAbilities)
            {
                if (!xpPool.CanSpendOnAbility(archAbility))
                {
                    errorMessages.Add("BasicXP Pool should allow spending on any ability.  Could not spend on " + archAbility.Name);
                }
            }
            string errorString = String.Join(", ", errorMessages);
            Assert.IsTrue(errorMessages.Count == 0, errorString);
        }

        [TestMethod]
        public void TestCategoryAbilityXpPoolAcceptsAbility()
        {
            XPPool xPPool = new CategoryAbilityXpPool("Warrior Virtue", "XP Pool from having the warrior virtue.", 50, new List<AbilityType> { AbilityType.Martial });
            Assert.IsTrue(xPPool.CanSpendOnAbility(ArchAbility.Bows));
        }

        [TestMethod]
        public void TestCategoryAbilityXpPoolDoesNotAcceptAbility()
        {
            XPPool xPPool = new CategoryAbilityXpPool("Warrior Virtue", "XP Pool from having the warrior virtue.", 50, new List<AbilityType> { AbilityType.Martial });
            Assert.IsFalse(xPPool.CanSpendOnAbility(ArchAbility.ArtesLiberales));
        }

        [TestMethod]
        public void TestXPPoolSort()
        {
            XPPool xpPool1 = new BasicXPPool("Name", "description", 100);
            XPPool xpPool2 = new CategoryAbilityXpPool("Warrior Virtue", "XP Pool from having the warrior virtue.", 50, new List<AbilityType> { AbilityType.Martial });
            XPPool xpPool3 = new CategoryAbilityXpPool("Educated Virtue", "XP Pool from having the educated virtue.", 50, new List<AbilityType> { AbilityType.Academic });

            XPPool xpPool4 = new AllowOverdrawnXpPool();

            SortedSet<XPPool> xPPools = new(new XPPoolComparer())
            {
                xpPool2,
                xpPool3,
                xpPool4,
                xpPool1
            };

            Assert.IsTrue(xPPools.ElementAt(0).name == "Educated Virtue");
            Assert.IsTrue(xPPools.ElementAt(1).name == "Warrior Virtue");
            Assert.IsTrue(xPPools.ElementAt(2).name == "Name");
            Assert.IsTrue(xPPools.ElementAt(3).remainingXP > 1000000);
        }
    }
}