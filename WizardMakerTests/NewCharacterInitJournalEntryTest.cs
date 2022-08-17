using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models;
using WizardMakerTestbed.Models;
using Newtonsoft.Json;
namespace WizardMakerTests
{
    [TestClass]
    public class NewCharacterInitJournalEntryTest
    {
        [TestMethod]
        public void TestSimpleInit()
        {
            CharacterManager cm = new CharacterManager(25);
            NewCharacterInitJournalEntry NewCharacterInitJournalEntry = new NewCharacterInitJournalEntry(cm, 25, ArchAbility.LangEnglish);
            
            // Normally, call cm.renderAllJournalEntries(), rather than executing the journal entry directly.
            NewCharacterInitJournalEntry.Execute();
            Assert.IsTrue(cm.getXPPoolCount() == 4);
        }
    }
}
