using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class PrivilegedUpbringingCommand : ICharacterCommand
    {
        public static string PRIVILEGED_UPBRINGING_XP_POOL_NAME = "Privileged upbringing XP Pool";
        public static string PRIVILEGED_UPBRINGING_XP_POOL_DESCRIPTION = "XP Pool from taking the virtue, Privileged Upbringing";
        public static int PRIVILEGED_UPBRINGING_XP_POOL_XP = 50;
        public void Execute(Character c)
        {
            // Create a 50XP pool
            // TODO: This will generate a ValidationPool error when someone buys skills that the character may not normally be able to get.  See issue (https://github.com/sg-wizard-maker/Wizard-Maker-Project/issues/10)
            c.XPPoolList.Add(new CategoryAbilityXpPool(PRIVILEGED_UPBRINGING_XP_POOL_NAME, PRIVILEGED_UPBRINGING_XP_POOL_DESCRIPTION, PRIVILEGED_UPBRINGING_XP_POOL_XP,
                new List<AbilityType>() { AbilityType.General, AbilityType.GenChild, AbilityType.Academic, AbilityType.Martial }));
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
