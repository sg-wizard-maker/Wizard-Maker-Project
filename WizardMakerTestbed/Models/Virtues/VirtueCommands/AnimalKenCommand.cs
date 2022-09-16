using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class AnimalKenCommand : ICharacterCommand
    {
        private GenericGrantAbilityCommand command;
        public AnimalKenCommand()
        {
            command = new GenericGrantAbilityCommand(ArchAbility.AnimalKen);
        }

        public void Execute(Character c)
        {
            command.Execute(c);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
