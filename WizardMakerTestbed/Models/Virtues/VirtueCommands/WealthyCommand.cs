using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class WealthyCommand : ICharacterCommand
    {
        public void Execute(Character cb)
        {
            cb.XpPerSeasonForInitialCreation = 20;

            // TODO: Increase the BasicXPPool
            
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
