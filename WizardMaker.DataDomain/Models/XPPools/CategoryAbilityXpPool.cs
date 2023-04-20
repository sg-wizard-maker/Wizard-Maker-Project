using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WizardMakerTests")]

namespace WizardMaker.DataDomain.Models;

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
        bool result = this.AllowedAbilityTypes.Contains(archAbility.Type);
        return result;
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
        if (!base.IsSameSpecs(other))
        {
            return false;
        }
        CategoryAbilityXpPool otherPool = (CategoryAbilityXpPool)other;
        if (this.AllowedAbilityTypes.Count != otherPool.AllowedAbilityTypes.Count)
        {
            return false;
        }
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
