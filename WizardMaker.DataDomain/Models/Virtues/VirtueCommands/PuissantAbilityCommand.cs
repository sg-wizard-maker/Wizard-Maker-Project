using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands
{
    public class PuissantAbilityCommand : ICharacterCommand
    {
        ArchAbility Ability;
        
        public PuissantAbilityCommand(ArchAbility ability)
        {
            this.Ability = ability;
        }

        public void Execute(Character c)
        {
            c.PuissantAbilities.Add(Ability.Name);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
