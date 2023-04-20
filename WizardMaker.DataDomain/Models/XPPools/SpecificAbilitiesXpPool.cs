using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WizardMakerTests")]

namespace WizardMaker.DataDomain.Models;

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
        if (!base.IsSameSpecs(other))
        {
            return false;
        }
        SpecificAbilitiesXpPool otherPool = (SpecificAbilitiesXpPool)other;
        if (this.AllowedAbilities.Count != otherPool.AllowedAbilities.Count)
        {
            return false;
        }
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
