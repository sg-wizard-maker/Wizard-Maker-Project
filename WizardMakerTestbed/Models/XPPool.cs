using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("WizardMakerTests")]
namespace WizardMakerPrototype.Models
{
    /*
     * This class expects the caller to manage the XP expenditures in a pool and make sure that it is not 
     *  overdrawn.
     */
    public class XPPool
    {
        public string name { get; }
        public string description { get; }
        private int initialXP;
        public int remainingXP { get; set; }

        public XPPool(string name, string description, int initialXP)
        {
            this.name = name;
            this.description = description;
            this.initialXP = initialXP;
            this.remainingXP = initialXP;
        }

        public void spendXP(int xp)
        {
            if (this.remainingXP < xp)
            {
                throw new XPPoolOverdrawnException("XP Pool " + name + " asked for too many XP: " + xp + " > " + this.remainingXP);
            }
            this.remainingXP -= xp;
        } 
    }

}
