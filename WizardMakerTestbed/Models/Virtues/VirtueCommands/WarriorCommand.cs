using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class WarriorCommand : ICharacterCommand
    {
        public static string WARRIOR_POOL_NAME = "Warrior XP Pool";
        public static string WARRIOR_POOL_DESCRIPTION = "XP Pool for Virtue Warrior";
        public static int WARRIOR_POOL_INITIAL_XP = 50;
        private GenericAllowAbilityTypeAndXPPoolCommand command;

        public WarriorCommand()
        {
            command = new GenericAllowAbilityTypeAndXPPoolCommand(AbilityType.Martial, WARRIOR_POOL_NAME, WARRIOR_POOL_DESCRIPTION, WARRIOR_POOL_INITIAL_XP);
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
