using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands
{
    public class AffinityAbilityCommand : ICharacterCommand
    {
        ArchAbility ability;

        public AffinityAbilityCommand(ArchAbility ability)
        {
            this.ability = ability;
        }

        public void Execute(Character c)
        {
            c.affinityAbilities.Add(ability.Name);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
