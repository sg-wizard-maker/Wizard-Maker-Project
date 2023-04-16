using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands
{
    public class ArcaneLoreCommand : ICharacterCommand
    {
        public static string ARCANE_LORE_POOL_NAME        = "Arcane Lore XP Pool";
        public static string ARCANE_LORE_POOL_DESCRIPTION = "XP Pool for Virtue Arcane Lore";
        public static int    ARCANE_LORE_INITIAL_XP       = 50;

        private GenericAllowAbilityTypeAndXPPoolCommand Command;

        public ArcaneLoreCommand()
        {
            Command = new GenericAllowAbilityTypeAndXPPoolCommand(AbilityType.Arcane, ARCANE_LORE_POOL_NAME, ARCANE_LORE_POOL_DESCRIPTION, ARCANE_LORE_INITIAL_XP);
        }

        public void Execute(Character c)
        {
            Command.Execute(c);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
