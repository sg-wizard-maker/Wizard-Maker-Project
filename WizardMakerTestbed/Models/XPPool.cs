using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

[assembly: InternalsVisibleTo("WizardMakerTests")]
namespace WizardMakerPrototype.Models
{
    public class XPPoolComparer : IComparer<XPPool>
    {
        public int Compare(XPPool x, XPPool y)
        {
            if (x.sortOrder() == y.sortOrder())
            {
                return new CaseInsensitiveComparer().Compare(x.name,y.name);
            }
            return x.sortOrder().CompareTo(y.sortOrder());  
        }
    }
    /*
     * This class expects the caller to manage the XP expenditures in a pool and make sure that it is not 
     *  overdrawn.
     */
    public abstract class XPPool
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

        public virtual void spendXP(int xp)
        {
            if (this.remainingXP < xp)
            {
                throw new XPPoolOverdrawnException("XP Pool " + name + " was asked for too many XP: " + xp + " > " + this.remainingXP);
            }
            this.remainingXP -= xp;
        }

        abstract public bool CanSpendOnAbility(ArchAbility archAbility);

        abstract public int sortOrder();
    }

    /**
     * Basic XP Pool that allows any ability spend.
     * 
     * This should be used last in sort order, since we want to expend the most versatile sources last.
     */
    public class BasicXPPool : XPPool
    {
        public BasicXPPool(string name, string description, int initialXP) : base(name, description, initialXP)
        {
        }

        public override bool CanSpendOnAbility(ArchAbility archAbility)
        {
            return true;
        }

        public override int sortOrder() {
            return 100;
        }
    }

    /** XP Pool that only allows spending on one category of abilities
     */
    public class CategoryAbilityXpPool : XPPool
    {
        private List<AbilityType> allowedAbilityTypes;

        public CategoryAbilityXpPool(string name, string description, int initialXP, List<AbilityType> allowedAbilityTypes) : base(name, description, initialXP) { 
            this.allowedAbilityTypes = allowedAbilityTypes;
        }

        public override bool CanSpendOnAbility(ArchAbility archAbility)
        {
            if (this.allowedAbilityTypes.Contains(archAbility.Type)) { return true; }
            else { return false; }
        }

        public override int sortOrder()
        {
            return 10;
        }
    }

    public class AllowOverdrawnXpPool : XPPool
    {
        const string OVERDRAWN_NAME = "Overdraw";
        const string OVERDRAWN_DESCRIPTION = "Overdraw experience pool that allows negative remaining XP.";

        //TODO: This should just be a method that shows that it always has enough XP.  Rather than making this a ridiculously high number.
        const int OVERDRAWN_AMOUNT = int.MaxValue-1;

        public AllowOverdrawnXpPool() : base(OVERDRAWN_NAME, OVERDRAWN_DESCRIPTION, OVERDRAWN_AMOUNT)
        {
        }

        public override bool CanSpendOnAbility(ArchAbility archAbility)
        {
            return true;
        }

        public override int sortOrder()
        {
            return 1000;
        }

        public void spendXP(int xp)
        {
            // XP can go negative.
            this.remainingXP -= xp;
        }
    }
}
