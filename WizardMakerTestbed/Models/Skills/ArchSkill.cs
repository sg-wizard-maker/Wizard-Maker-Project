using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Skills
{
    public class ArchSkill
    {
        public int baseXPCost { get; private set; }
        public string name { get; private set; }

        public static T lookupCommonAbilities<T>(string skill) where T:ArchSkill
        {
            return null;
        }
        

    }
}
