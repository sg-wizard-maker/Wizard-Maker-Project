using Newtonsoft.Json;
using WizardMaker.DataDomain.Models;
using WizardMaker.DataDomain.Models;

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
        private const string XP_POOL_NAME = "Test XP Pool";

        [TestMethod]
        public void TestBasicXPPool()
        {
            XPPool xpPool = new BasicXPPool(XP_POOL_NAME, "description", 100);

            Assert.IsNotNull(xpPool);
        }

        [TestMethod]
        public void TestBasicXPPoolSimpleSpend()
        {
            XPPool xpPool = new BasicXPPool(XP_POOL_NAME, "description", 100);
            xpPool.SpendXP(50);
            Assert.IsTrue(xpPool.RemainingXP == 50);
        }

        [TestMethod]
        public void TestBasicXPPoolSimpleFailure()
        {
            XPPool xpPool = new BasicXPPool(XP_POOL_NAME, "description", 100);
            Assert.ThrowsException<XPPoolOverdrawnException>(() => xpPool.SpendXP(150));
        }

        [TestMethod]
        public void TestBasicXpPoolAllowsAllAbilities()
        {
            XPPool xpPool = new BasicXPPool(XP_POOL_NAME, "description", 100);

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
            XPPool xpPool1 = new BasicXPPool(XP_POOL_NAME, "description", 100);
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

            Assert.IsTrue(xPPools.ElementAt(0).Name == "Educated Virtue");
            Assert.IsTrue(xPPools.ElementAt(1).Name == "Warrior Virtue");
            Assert.IsTrue(xPPools.ElementAt(2).Name == XP_POOL_NAME);
            Assert.IsTrue(xPPools.ElementAt(3).RemainingXP > 1000000);
        }
        [TestMethod]
        public void TestMultipleXPPoolsAbilities()
        {
            SortedSet <XPPool>  pools = new SortedSet<XPPool>(new XPPoolComparer()) {
                new SpecificAbilitiesXpPool(CHILDHOOD_LANGUAGE_POOL_NAME, CHILDHOOD_LANGUAGE_DESCRIPTION, CHILDHOOD_LANGUAGE_XP, 
                    new List<ArchAbility>() {ArchAbility.LangEnglish}),
                new SpecificAbilitiesXpPool(CHILDHOOD_POOL_NAME, CHILDHOOD_DESCRIPTION, CHILDHOOD_XP, NewCharacterInitJournalEntry.DetermineChildhoodAbilities()),
                new BasicXPPool(LATER_LIFE_POOL_NAME, LATER_LIFE_DESCRIPTION, 75),
                new AllowOverdrawnXpPool()
            };

            AbilityInstance english = new AbilityInstance(ArchAbility.LangEnglish, "testID1", 75);
            
            AbilityInstance charm = new AbilityInstance(ArchAbility.Charm, "testID2", 75);

            AbilityXPManager.DebitXPPoolsForAbility(english, pools);
            AbilityXPManager.DebitXPPoolsForAbility(charm, pools);

            // Childhood language should be 0
            Assert.IsTrue(pools.ElementAt(0).RemainingXP == 0);

            // Childhood skills should be 0 (spent on charm)
            Assert.IsTrue(pools.ElementAt(1).RemainingXP == 0);

            // Later life should be reduced by 30
            Assert.IsTrue(pools.ElementAt(2).RemainingXP == (75-30));
        }


        private static IEnumerable<object[]> ProduceTestXPPools()
        {
            yield return new object[]
            {
                new SpecificAbilitiesXpPool(CHILDHOOD_LANGUAGE_POOL_NAME, CHILDHOOD_LANGUAGE_DESCRIPTION, CHILDHOOD_LANGUAGE_XP,
                    new List<ArchAbility>() {ArchAbility.LangEnglish}),
            };
            yield return new object[]
            {
                new SpecificAbilitiesXpPool(CHILDHOOD_POOL_NAME, CHILDHOOD_DESCRIPTION, CHILDHOOD_XP, 
                    new List<ArchAbility>() {ArchAbility.AnimalHandling, ArchAbility.Charm}),
            };
            yield return new object[]
            {
                new CategoryAbilityXpPool("foo", "test", 100, new List<AbilityType>(){AbilityType.Arcane }),
            };
            yield return new object[]
            {
                new BasicXPPool(LATER_LIFE_POOL_NAME, LATER_LIFE_DESCRIPTION, 75),
            }; 
            yield return new object[]
            {
                new AllowOverdrawnXpPool()
            };
        } 
        // TODO: Add broken serialization attempts.  Particularly when trying to deserialize into the wrong derived class
        [TestMethod]
        [DynamicData(nameof(ProduceTestXPPools), DynamicDataSourceType.Method)]
        
        // This test has been disabled, since XPPools being serializable is not necessarily a requirement.
        public void SerializableTest(XPPool pool)
        {
            string tmp = pool.SerializeJson();

            XPPool deserialized = XPPool.DeserializeJson(tmp);

            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(pool);

            // Note that if this is not calling the derived class IsSameSpecs, you will get erroneous passing of this test.  But
            //  this should not happen due to usage of GetType
            Assert.IsTrue(pool.IsSameSpecs(deserialized));
        }
    }
}