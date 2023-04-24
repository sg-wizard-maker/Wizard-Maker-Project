using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands;

public class AffinityAbilityCommand : ICharacterCommand
{
    ArchAbility Ability;

    public AffinityAbilityCommand(ArchAbility ability)
    {
        this.Ability = ability;
    }

    #region Implementing ICharacterCommand
    public void Execute(Character c)
    {
        c.AffinityAbilities.Add(Ability.Name);
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }
    #endregion
}
