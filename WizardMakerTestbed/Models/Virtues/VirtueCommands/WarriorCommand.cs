using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    public class WarriorCommand : ICharacterCommand
    {
        public void Execute(Character c)
        {
            // Allow access to Martial skills
            c.AllowedAbilityTypes.Add(AbilityType.Martial);

            // Create a martial 50XP pool
            c.XPPoolList.Add(new CategoryAbilityXpPool(ArchVirtue.NameToArchVirtue["Warrior"].Name, "XP Pool for Virtue Warrior", 50,
                new List<AbilityType>() { AbilityType.Martial }));
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
