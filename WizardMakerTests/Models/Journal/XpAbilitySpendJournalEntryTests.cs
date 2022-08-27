﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMakerPrototype.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Tests
{
    [TestClass()]
    public class XpAbilitySpendJournalEntryTests
    {
        private const string Expected = "Test Entry";

        [TestMethod()]
        public void XpAbilitySpendJournalEntryTest()
        {
            // Just a simple test that we can create an instance and have it not be junk
            SeasonYear sy = new SeasonYear(1222, Season.SPRING);
            Journalable entry = new XpAbilitySpendJournalEntry(Expected, sy, "Brawl", 5, "Fists");
            Assert.IsNotNull(entry);
        }

        [TestMethod()]
        public void getTextTest()
        {
            Journalable entry = new XpAbilitySpendJournalEntry(Expected, new SeasonYear(1222, Season.SPRING),
    "Brawl", 5, "Fists");

            Assert.AreEqual(Expected, entry.getText());  
        }

        [TestMethod()]
        public void getDateTest()
        {
            SeasonYear sy = new SeasonYear(1222, Season.SPRING);
            Journalable entry = new XpAbilitySpendJournalEntry(Expected, sy, "Brawl", 5, "Fists");
            Assert.IsNotNull(entry);
            Assert.AreEqual(sy, entry.getDate());
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            SeasonYear sy = new SeasonYear(1222, Season.SPRING);
            Character dummy = new Character("My name", "My desription", 30);

            Journalable entry = new XpAbilitySpendJournalEntry(Expected, sy, "Brawl", 5, "Fists");

            // Initialize the character before attempting to buy an ability
            Journalable initEntry = new NewCharacterInitJournalEntry(25, ArchAbility.LangEnglish, 20);
            initEntry.Execute(dummy);

            // Now actually test the XP spend
            entry.Execute(dummy);
            Assert.AreEqual(1, dummy.abilities.Count, "Dummy character should only have one abililty.  Found " + dummy.abilities.Count);
            Assert.AreEqual(ArchAbility.Brawl.Name, dummy.abilities[0].Name);

        }

        [TestMethod]
        public void ExecuteNoExperienceExceptionTest()
        {
            SeasonYear sy = new SeasonYear(1222, Season.SPRING);
            Character dummy = new Character("My name", "My desription", 30);

            Journalable entry = new XpAbilitySpendJournalEntry(Expected, sy, "Brawl", 5, "Fists");

            // No XP pools given to the character
            try
            {
                entry.Execute(dummy);
                Assert.Fail();
            } catch (ShouldNotBeAbleToGetHereException snae)
            {
                // do nothing, we want to be here in a passing test
            }
        }

        [TestMethod()]
        public void getIdTest()
        {
            Journalable entry = new XpAbilitySpendJournalEntry(Expected, new SeasonYear(1222, Season.SPRING),
    "Brawl", 5, "Fists");

            string tmp = entry.SerializeJson();
            Journalable deserialized = Journalable.DeserializeJson(tmp);

            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(tmp);

            // Note that if this is not calling the derived class IsSameSpecs, you will get erroneous passing of this test.  But
            //  this should not happen due to usage of GetType
            Assert.IsTrue(entry.IsSameSpecs(deserialized));

            // Make sure that the IDs do not match
            Assert.AreNotSame(entry, deserialized); 
        }

        [TestMethod()]
        public void SerializableTest()
        {

            Journalable entry = new XpAbilitySpendJournalEntry(Expected, new SeasonYear(1222, Season.SPRING), 
                "Brawl", 5, "Fists");

            string tmp = entry.SerializeJson();
            Journalable deserialized = Journalable.DeserializeJson(tmp);

            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(tmp);

            // Note that if this is not calling the derived class IsSameSpecs, you will get erroneous passing of this test.  But
            //  this should not happen due to usage of GetType
            Assert.IsTrue(entry.IsSameSpecs(deserialized));
        }
    }
}