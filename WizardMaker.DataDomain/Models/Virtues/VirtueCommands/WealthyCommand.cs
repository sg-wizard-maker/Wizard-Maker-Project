using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands
{
    public class WealthyCommand : ICharacterCommand
    {
        public static int WEALTHY_ANNUAL_XP = 20;

        public void Execute(Character c)
        {
            // Since this is executed before the XP Pool initialization,
            // the wealthy will carry over for initial characters
            // (so long as the developers do a good job with making sure that both journal entries have the same season-year).
            // And this will also work for characters that acquire wealthy later on, due to the SeasonYear attached to the virtue.
            c.XpPerYear = WEALTHY_ANNUAL_XP;
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
