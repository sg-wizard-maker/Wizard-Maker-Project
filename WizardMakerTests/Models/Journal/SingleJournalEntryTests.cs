﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using WizardMaker.DataDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Tests
{
    [TestClass()]
    public class SingleJournalEntryTests
    {
        [TestMethod()]
        public void SingleJournalEntryTest()
        {
            Journalable entry = new SimpleJournalEntry("Test Entry", new SeasonYear(1222, Season.SPRING));
            Assert.IsNotNull(entry);
        }

        [TestMethod()]
        public void getTextTest()
        {
            const string TITLE = "New Entry";
            Journalable entry = new SimpleJournalEntry(TITLE, new SeasonYear(1222, Season.SPRING));
            Assert.IsNotNull(entry);
            Assert.AreEqual(TITLE, entry.getText());
        }

        [TestMethod()]
        public void getDateTest()
        {
            const string TITLE = "New Entry";
            SeasonYear sy = new SeasonYear(1222, Season.SPRING);
            Journalable entry = new SimpleJournalEntry(TITLE, sy);
            Assert.IsNotNull(entry);
            Assert.AreEqual(sy, entry.getDate());
        }

        [TestMethod()]
        public void ExecuteTest()
        {
            // Execute is currently a no-op, so just pass if you can run the method with no exception.
            const string TITLE = "New Entry";
            SeasonYear sy = new SeasonYear(1222, Season.SPRING);
            Character dummy = new Character("My name", "My desription", 30);
            Journalable entry = new SimpleJournalEntry(TITLE, sy);
            entry.Execute(dummy);
        }

        [TestMethod()]
        public void getIdTest()
        {
            // Test that the IDs actually change when you deserialize a SingleJournalEntry
            Journalable entry = new SimpleJournalEntry("Test Entry", new SeasonYear(1222, Season.SPRING));

            string tmp = entry.SerializeJson();
            Journalable deserialized = Journalable.DeserializeJson(tmp);

            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(tmp);

            // Note that if this is not calling the derived class IsSameSpecs, you will get erroneous passing of this test.  But
            //  this should not happen due to usage of GetType
            Assert.IsTrue(entry.IsSameSpecs(deserialized));

            Assert.AreNotSame(deserialized.getId(), entry.getId());
        }

        [TestMethod()]
        public void SerializableTest()
        {

            Journalable entry = new SimpleJournalEntry("Test Entry", new SeasonYear(1222, Season.SPRING));

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