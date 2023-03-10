using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands
{
    /**
     * this class is a utility class for virtues that grant access to an Ability Type AND an XP pool to spend on abilities
     *  of that type.  If the virtue is not doing both of those things, then developers should not use this class.
     */
    internal class GenericAllowAbilityTypeAndXPPoolCommand : ICharacterCommand
    {
        AbilityType allowedType;
        string name;
        string description;
        int initialXP;

        public GenericAllowAbilityTypeAndXPPoolCommand(AbilityType allowedType, string name, string description, int initialXP)
        {
            this.allowedType = allowedType;
            this.name = name;
            this.description = description;
            this.initialXP = initialXP;
        }

        public void Execute(Character c)
        {
            // Allow access to Martial skills
            c.AllowedAbilityTypes.Add(allowedType);

            // Create a martial 50XP pool
            c.XPPoolList.Add(new CategoryAbilityXpPool(name, description, initialXP,
                new List<AbilityType>() { allowedType }));
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
