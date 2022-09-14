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
        public static Character GenerateBasicTestCharacterWithStartingVirtue(int startingAge, string staringVirtue, int sagaStart = 1220)
        {
            Character c = GenerateBasicTestCharacter(startingAge, sagaStart);
            ArchVirtue archVirtue = ArchVirtue.NameToArchVirtue[staringVirtue];

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
    }
}
