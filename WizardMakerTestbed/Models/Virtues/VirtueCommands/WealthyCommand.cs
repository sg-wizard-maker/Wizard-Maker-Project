using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class WealthyCommand : ICharacterCommand
    {
        public static int WEALTHY_XP = 20;
        public void Execute(Character cb)
        {
            // Since this is executed before the XP Pool initialization, the wealthy will carry over for initial characters (so long as the developers do a good job with
            //  making sure that both journal entries have the same season-year.
            //  And this will also work for characters that acquire wealthy later on, due to the SeasonYear attached to the virtue.
            cb.XpPerYear = WEALTHY_XP;
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
