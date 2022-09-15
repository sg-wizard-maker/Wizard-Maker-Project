using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class PuissantAbilityCommand : ICharacterCommand
    {
        ArchAbility ability;
        
        public PuissantAbilityCommand(ArchAbility ability)
        {
            this.ability = ability;
        }

        public void Execute(Character c)
        {
            c.puissantAbilities.Add(ability.Name);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
