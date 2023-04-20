using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands
{
    // This class is a utility class for virtues that grant access to
    // an Ability Type AND an XP pool to spend on abilities of that type.
    // If the virtue is not doing both of those things, then developers should not use this class.
    internal class GenericAllowAbilityTypeAndXPPoolCommand : ICharacterCommand
    {
        AbilityType AllowedType;
        string      Name;
        string      Description;
        int         InitialXP;

        public GenericAllowAbilityTypeAndXPPoolCommand(AbilityType allowedType, string name, string description, int initialXP)
        {
            this.AllowedType = allowedType;
            this.Name        = name;
            this.Description = description;
            this.InitialXP   = initialXP;
        }

        #region Implementing ICharacterCommand
        public void Execute(Character c)
        {
            // Allow access to Abilities of the indicated type
            c.AllowedAbilityTypes.Add(AllowedType);

            // Create an XP pool for the indicated type, and specified number of XP
            c.XPPoolList.Add(
                new CategoryAbilityXpPool(
                    Name, 
                    Description, 
                    InitialXP, 
                    new List<AbilityType>() { AllowedType } 
                )
            );
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
