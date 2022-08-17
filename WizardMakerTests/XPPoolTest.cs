using WizardMakerPrototype.Models;
using WizardMakerTestbed.Models;

namespace WizardMakerTests
{
    [TestClass]
    public class XPPoolTest
    {
        private const string CHILDHOOD_LANGUAGE_POOL_NAME = "Childhood language XP Pool";
        private const string CHILDHOOD_LANGUAGE_DESCRIPTION = "XP granted to starting characters that can be spent on one language";
        private const int CHILDHOOD_LANGUAGE_XP = 75;

        private const string CHILDHOOD_POOL_NAME = "Childhood XP Pool";
        private const string CHILDHOOD_DESCRIPTION = "XP granted to starting characters that can be spent on childhood skills";
        private const int CHILDHOOD_XP = 45;

        private const string LATER_LIFE_POOL_NAME = "Later life XP Pool";
        private const string LATER_LIFE_DESCRIPTION = "XP granted to starting characters that can be spent on anything the character can learn.  After age 5.";


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
        [TestMethod]
        public void TestMultipleXPPoolsAbilities()
        {
            SortedSet <XPPool>  pools = new SortedSet<XPPool>(new XPPoolComparer()) {
                new SpecificAbilitiesXpPool(CHILDHOOD_LANGUAGE_POOL_NAME, CHILDHOOD_LANGUAGE_DESCRIPTION, CHILDHOOD_LANGUAGE_XP, 
                    new List<ArchAbility>() {ArchAbility.LangEnglish}),
                new SpecificAbilitiesXpPool(CHILDHOOD_POOL_NAME, CHILDHOOD_DESCRIPTION, CHILDHOOD_XP, NewCharacterInitJournalEntry.determineChildhoodAbilities()),
                new BasicXPPool(LATER_LIFE_POOL_NAME, LATER_LIFE_DESCRIPTION, 75),
                new AllowOverdrawnXpPool()
            };

            AbilityInstance english = new AbilityInstance(ArchAbility.LangEnglish, 75);
            
            AbilityInstance charm = new AbilityInstance(ArchAbility.Charm, 75);

            AbilityXPManager.debitXPPoolsForAbility(english, pools);
            AbilityXPManager.debitXPPoolsForAbility(charm, pools);

            // Childhood language should be 0
            Assert.IsTrue(pools.ElementAt(0).remainingXP == 0);

            // Childhood skills should be 0 (spent on charm)
            Assert.IsTrue(pools.ElementAt(1).remainingXP == 0);

            // Later life should be reduced by 30
            Assert.IsTrue(pools.ElementAt(2).remainingXP == (75-30));
        }
    }
}