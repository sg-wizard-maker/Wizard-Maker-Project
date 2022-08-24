using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


//TODO: Move XP Pools and Journals to their own package.
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
        public int initialXP;
        public int remainingXP { get; set; }

        public XPPool(string name, string description, int initialXP)
        {
            this.name = name;
            this.description = description;
            this.initialXP = initialXP;
            this.remainingXP = initialXP;
        }

        public virtual void reset() { remainingXP = initialXP; }

        public virtual void spendXP(int xp)
        {
            if (this.remainingXP < xp)
            {
                throw new XPPoolOverdrawnException("XP Pool " + name + " was asked for too many XP: " + xp + " > " + this.remainingXP);
            }
            this.remainingXP -= xp;
        }

        // TODO: Do we need to be able to serialize and deserialize XP Pools?
        public static XPPool deserializeJson(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                MaxDepth = 128
            };

            return (XPPool) JsonConvert.DeserializeObject(json, settings);
        }

        public virtual string serializeJson()
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.All;

            return JsonConvert.SerializeObject(this, Formatting.Indented, settings);
        }

        abstract public bool CanSpendOnAbility(ArchAbility archAbility);

        abstract public int sortOrder();

        //TODO: This seems more difficult than it should be.
        // TODO: If we do not need to serialize and deserialize XPPools, do we need this method?  It was meant for testing.
        // This method is only used for testing.  We do not want to override the Equals behavior, becuase we typically do not want two XPPools with the 
        //  same stats to be considered equal.  
        public virtual Boolean IsSameSpecs(XPPool other)
        {
            if (other == null) return false;
            if (this.GetType() != other.GetType()) return false;
            if (this.name != other.name) return false;
            if (this.description != other.description) return false;
            if (!this.remainingXP.Equals(other.remainingXP)) return false;
            if (this.initialXP != other.initialXP) return false;
            
            return true;
        }
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
        public List<AbilityType> allowedAbilityTypes { get; set; } 

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

        public override Boolean IsSameSpecs(XPPool other)
        {

            if (!base.IsSameSpecs(other)) return false;
            CategoryAbilityXpPool o2 = (CategoryAbilityXpPool)other;
            if (this.allowedAbilityTypes.Count != o2.allowedAbilityTypes.Count) return false;
            foreach (AbilityType type in allowedAbilityTypes)
            {
                if (!o2.allowedAbilityTypes.Contains(type)) { return false; }
            }
            return true;
        }

    }
    /** This will be spent before ability categories
     */
    public class SpecificAbilitiesXpPool : XPPool
    {
        public List<ArchAbility> allowedAbilities { get; }

        public SpecificAbilitiesXpPool(string name, string description, int initialXP, List<ArchAbility> allowedAbilities) : base(name, description, initialXP)
        {
            this.allowedAbilities = allowedAbilities;
        }

        public override bool CanSpendOnAbility(ArchAbility archAbility)
        {
            if (this.allowedAbilities.Contains(archAbility)) { return true; }
            else { return false; }
        }

        public override int sortOrder()
        {
            return 9;
        }

        public override Boolean IsSameSpecs(XPPool other)
        {
            if (!base.IsSameSpecs(other)) return false;
            SpecificAbilitiesXpPool o2 = (SpecificAbilitiesXpPool)other;
            if (this.allowedAbilities.Count != o2.allowedAbilities.Count) return false;
            foreach (ArchAbility a in allowedAbilities)
            {
                if (!o2.allowedAbilities.Contains(a)) { return false; }
            }
            return true;
        }
    }

    public class AllowOverdrawnXpPool : XPPool
    {
        const string OVERDRAWN_NAME = "Overdraw";
        const string OVERDRAWN_DESCRIPTION = "Overdraw experience pool that allows negative remaining XP.  Each character can only have one.";

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
