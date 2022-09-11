using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class WarriorCommand : ICharacterCommand
    {
        public static string WARRIOR_POOL_NAME = ArchVirtue.NameToArchVirtue["Warrior"].Name;
        public static string WARRIOR_POOL_DESCRIPTION = "XP Pool for Virtue Warrior";
        public static int WARRIOR_POOL_INITIAL_XP = 50;
        public void Execute(Character c)
        {
            // Allow access to Martial skills
            c.AllowedAbilityTypes.Add(AbilityType.Martial);

            // Create a martial 50XP pool
            c.XPPoolList.Add(new CategoryAbilityXpPool(WARRIOR_POOL_NAME, WARRIOR_POOL_DESCRIPTION, WARRIOR_POOL_INITIAL_XP,
                new List<AbilityType>() { AbilityType.Martial }));
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
