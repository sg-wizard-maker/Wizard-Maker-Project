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
            Character c = new("Foo", "Looks like a wealthy foo", startingAge);
            NewCharacterInitJournalEntry initEntry = new NewCharacterInitJournalEntry(25, ArchAbility.LangEnglish);
            c.addJournalable(initEntry);
            return c;
        }
    }
}
