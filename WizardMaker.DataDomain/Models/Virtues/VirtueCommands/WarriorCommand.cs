using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands;

public class WarriorCommand : ICharacterCommand
{
    public static string WARRIOR_POOL_NAME        = "Warrior XP Pool";
    public static string WARRIOR_POOL_DESCRIPTION = "XP Pool for Virtue Warrior";
    public static int    WARRIOR_POOL_INITIAL_XP  = 50;

    private GenericAllowAbilityTypeAndXPPoolCommand Command;

    public WarriorCommand()
    {
        Command = new GenericAllowAbilityTypeAndXPPoolCommand(
            AbilityType.Martial, 
            WARRIOR_POOL_NAME, 
            WARRIOR_POOL_DESCRIPTION, 
            WARRIOR_POOL_INITIAL_XP
        );
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
