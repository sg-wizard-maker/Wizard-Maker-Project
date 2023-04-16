using Newtonsoft.Json;

using System.Collections;
using System.Runtime.CompilerServices;

using WizardMaker.DataDomain.Validation;


// TODO: Move XP Pools and Journals to their own package.
[assembly: InternalsVisibleTo("WizardMakerTests")]
namespace WizardMaker.DataDomain.Models
{
    public class XPPoolComparer : IComparer<XPPool>
    {
        public int Compare(XPPool xx, XPPool yy)
        {
            int result;
            if (xx.SortOrder() == yy.SortOrder())
            {
                result = new CaseInsensitiveComparer().Compare(xx.Name, yy.Name);
                return result;
            }
            result = xx.SortOrder().CompareTo(yy.SortOrder());
            return result;
        }
    }
    /*
     * This class expects the caller to manage the XP expenditures in a pool and make sure that it is not 
     *  overdrawn.
     */
    public abstract class XPPool
    {
        public string Name        { get; }
        public string Description { get; }
        public int    InitialXP   { get; set; }
        public int    RemainingXP { get; set; }

        public XPPool(string name, string description, int initialXP)
        {
            this.Name        = name;
            this.Description = description;
            this.InitialXP   = initialXP;
            this.RemainingXP = initialXP;
        }

        public virtual void Reset()
        {
            RemainingXP = InitialXP; 
        }

        public virtual void SpendXP(int xp)
        {
            if (this.RemainingXP < xp)
            {
                throw new XPPoolOverdrawnException("XP Pool " + Name + " was asked for too many XP: " + xp + " > " + this.RemainingXP);
            }
            this.RemainingXP -= xp;
        }

        // TODO: Do we need to be able to serialize and deserialize XP Pools?
        public static XPPool? DeserializeJson(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                MaxDepth = 128
            };

            var result = JsonConvert.DeserializeObject(json, settings) as XPPool;
            return result;
        }

        public virtual string SerializeJson()
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };
            var result = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
            return result;
        }

        abstract public bool CanSpendOnAbility(ArchAbility archAbility);

        abstract public int SortOrder();

        // TODO: This seems more difficult than it should be.
        // TODO: If we do not need to serialize and deserialize XPPools, do we need this method?  It was meant for testing.
        // This method is only used for testing.
        // We do not want to override the Equals behavior,
        // because we typically do not want two XPPools with the same stats to be considered equal.  
        public virtual bool IsSameSpecs(XPPool other)
        {
            if (other == null) return false;
            if (this.GetType()   != other.GetType())         return false;
            if (this.Name        != other.Name)              return false;
            if (this.Description != other.Description)       return false;
            if (!this.RemainingXP.Equals(other.RemainingXP)) return false;
            if (this.InitialXP   != other.InitialXP)         return false;
            
            return true;
        }
    }

    /**
     * Basic XP Pool that allows any ability spend.
     * This should be used last in sort order, since we want to expend the most versatile sources last.
     */
    public class BasicXPPool : XPPool
    {
        public BasicXPPool(string name, string description, int initialXP) 
            : base(name, description, initialXP)
        {
        }

        public override bool CanSpendOnAbility(ArchAbility archAbility)
        {
            return true;
        }

        public override int SortOrder() {
            return 100;
        }
    }

    // XP Pool that only allows spending on one category of abilities
    public class CategoryAbilityXpPool : XPPool
    {
        public List<AbilityType> AllowedAbilityTypes { get; set; } 

        public CategoryAbilityXpPool(string name, string description, int initialXP, List<AbilityType> allowedAbilityTypes) 
            : base(name, description, initialXP) 
        { 
            this.AllowedAbilityTypes = allowedAbilityTypes;
        }

        public override bool CanSpendOnAbility(ArchAbility archAbility)
        {
            if (this.AllowedAbilityTypes.Contains(archAbility.Type)) 
            {
                return true; 
            }
            return false;
        }

        public override int SortOrder()
        {
            return 10;
        }

        public override bool IsSameSpecs(XPPool other)
        {
            // TODO: What is the intent here?
            // Param 'other' is XPPool (not specifically CategoryAbilityXpPool),
            // yet is cast to CategoryAbilityXpPool, which might fail.
            // Confusion!
            if (!base.IsSameSpecs(other)) return false;
            CategoryAbilityXpPool otherPool = (CategoryAbilityXpPool)other;
            if (this.AllowedAbilityTypes.Count != otherPool.AllowedAbilityTypes.Count) return false;
            foreach (AbilityType type in AllowedAbilityTypes)
            {
                if (otherPool.AllowedAbilityTypes
                        .Where<AbilityType>(archAbilityType => archAbilityType.Name == type.Name)
                        .Count() == 0
                    )
                {
                    return false;
                }
            }
            return true;
        }

    }

    // This will be spent before ability categories
    public class SpecificAbilitiesXpPool : XPPool
    {
        public List<ArchAbility> AllowedAbilities { get; }

        public SpecificAbilitiesXpPool(string name, string description, int initialXP, List<ArchAbility> allowedAbilities) 
            : base(name, description, initialXP)
        {
            this.AllowedAbilities = allowedAbilities;
        }

        public override bool CanSpendOnAbility(ArchAbility archAbility)
        {
            if (this.AllowedAbilities.Contains(archAbility)) 
            {
                return true; 
            }
            return false;
        }

        public override int SortOrder()
        {
            return 9;
        }

        public override bool IsSameSpecs(XPPool other)
        {
            // TODO: What is the intent here?
            // Param 'other' is XPPool (not specifically SpecificAbilitiesXpPool),
            // yet is cast to SpecificAbilitiesXpPool, which might fail.
            // Confusion!
            if (!base.IsSameSpecs(other)) return false;
            SpecificAbilitiesXpPool otherPool = (SpecificAbilitiesXpPool)other;
            if (this.AllowedAbilities.Count != otherPool.AllowedAbilities.Count) return false;
            foreach (ArchAbility a in AllowedAbilities)
            {
                
                if (otherPool.AllowedAbilities
                        .Where<ArchAbility>(archAbility => archAbility.Name == a.Name)
                        .Count() == 0 
                    )
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class AllowOverdrawnXpPool : XPPool
    {
        const string OVERDRAWN_NAME        = "Overdraw";
        const string OVERDRAWN_DESCRIPTION = "Overdraw experience pool that allows negative remaining XP.  Each character can only have one.";

        // TODO:
        // This should just be a method that shows that it always has enough XP.
        // Rather than making this a ridiculously high number.
        const int OVERDRAWN_AMOUNT = int.MaxValue-1;

        public AllowOverdrawnXpPool() 
            : base(OVERDRAWN_NAME, OVERDRAWN_DESCRIPTION, OVERDRAWN_AMOUNT)
        {
        }

        public override bool CanSpendOnAbility(ArchAbility archAbility)
        {
            return true;
        }

        public override int SortOrder()
        {
            return 1000;
        }

        public override void SpendXP(int xp)
        {
            // XP can go negative.
            this.RemainingXP -= xp;

            ValidationLog.AddValidationMessage("Overdrawn by " + xp + " XP.");
        }
    }
}
