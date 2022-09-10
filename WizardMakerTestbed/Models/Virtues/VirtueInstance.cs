using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues
{
    public class VirtueInstance
    {
        public ArchVirtue Virtue { get; private set;}
        public VirtueInstance(ArchVirtue virtue)
        {
            Virtue = virtue;
        }
    }
}
