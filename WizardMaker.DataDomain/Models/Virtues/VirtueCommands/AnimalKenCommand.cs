﻿using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands;

public class AnimalKenCommand : ICharacterCommand
{
    private GenericGrantAbilityCommand Command;

    public AnimalKenCommand()
    {
        Command = new GenericGrantAbilityCommand(ArchAbility.AnimalKen);
    }

    #region Implementing ICharacterCommand
    public void Execute(Character c)
    {
        Command.Execute(c);
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }
    #endregion
}
