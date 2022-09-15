using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models;
using WizardMakerPrototype.Models.Journal;
using WizardMakerPrototype.Models.Virtues;

namespace WizardMakerTests.Models.Virtues.VirtueCommands
{
    internal class CommandTestUtilities
    {
        public static Character GenerateBasicTestCharacter(int startingAge, int sagaStart=1220)
        {
            // Create a basic character
            Character c = new("Foo", "Looks like a foo", startingAge);
            NewCharacterInitJournalEntry initEntry = new NewCharacterInitJournalEntry(25, ArchAbility.LangEnglish);
            c.addJournalable(initEntry);
            return c;
        }

        /**
         * The starting virtue string must be a key in the ArchVirtue.NameToArchVirtue dictionary
         */
        public static Character GenerateBasicTestCharacterWithStartingVirtue(int startingAge, string startingVirtue, int sagaStart = 1220)
        {
            Character c = GenerateBasicTestCharacter(startingAge, sagaStart);
            ArchVirtue archVirtue = ArchVirtue.NameToArchVirtue[startingVirtue];

            // Note that this is only generating a virtue that is in the same season as the NewCharacterInit journal entry.
            AddVirtueJournalEntry virtueJournalEntry = new AddVirtueJournalEntry(new SeasonYear(sagaStart - startingAge, Season.SPRING),
                archVirtue);
            c.addJournalable(virtueJournalEntry);

            Assert.IsTrue(archVirtue.IsImplemented());

            // Assert that the virtue appears before the new character init, since these were put in the same season.
            Assert.IsInstanceOfType(c.GetJournal().ElementAt(0), typeof(AddVirtueJournalEntry));
            Assert.IsInstanceOfType(c.GetJournal().ElementAt(1), typeof(NewCharacterInitJournalEntry));

            return c;
        }

        public static void AssertXPPoolInitialization(Character c, string xpPoolName, string xpPoolDescription, int initialXP)
        {
            // Assert that the XP Pool was created and has the correct amount of XP (initial and remaining)
            Assert.IsTrue(c.XPPoolList.Where(x => x.name.Equals(xpPoolName)).Any());
            Assert.AreEqual(initialXP, c.XPPoolList.Where(x => x.name.Equals(xpPoolName)).First().remainingXP);
            Assert.AreEqual(initialXP, c.XPPoolList.Where(x => x.name.Equals(xpPoolName)).First().initialXP);   
        }
    }
}
