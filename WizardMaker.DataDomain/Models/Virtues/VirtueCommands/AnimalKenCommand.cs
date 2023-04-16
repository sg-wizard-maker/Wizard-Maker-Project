﻿using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands
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
