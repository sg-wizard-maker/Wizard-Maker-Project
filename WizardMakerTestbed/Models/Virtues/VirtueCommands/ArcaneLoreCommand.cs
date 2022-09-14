using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class ArcaneLoreCommand : ICharacterCommand
    {
        public static string ARCANE_LORE_POOL_NAME = "Arcane Lore XP Pool";
        public static string ARCANE_LORE_POOL_DESCRIPTION = "XP Pool for Virtue Arcane Lore";
        public static int ARCANE_LORE_INITIAL_XP = 50;
        private GenericAllowAbilityTypeCommand command;

        public ArcaneLoreCommand()
        {
            command = new GenericAllowAbilityTypeCommand(AbilityType.Arcane, ARCANE_LORE_POOL_NAME, ARCANE_LORE_POOL_DESCRIPTION, ARCANE_LORE_INITIAL_XP);
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
