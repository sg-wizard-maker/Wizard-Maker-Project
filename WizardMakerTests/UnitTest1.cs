using WizardMakerPrototype.Models;
namespace WizardMakerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestXPPool()
        {
            XPPool xpPool = new ("Name", "description", 100);

            Assert.IsNotNull(xpPool);
        }

        [TestMethod]
        public void TestXPPoolSimpleSpend()
        {
            XPPool xpPool = new("Name", "description", 100);
            xpPool.spendXP(50);
            Assert.IsTrue(xpPool.remainingXP == 50);
        }

        [TestMethod]
        public void TestXPPoolSimpleFailure()
        {
            XPPool xpPool = new("Name", "description", 100);
            Assert.ThrowsException<XPPoolOverdrawnException>(() => xpPool.spendXP(150));
        }
    }
}