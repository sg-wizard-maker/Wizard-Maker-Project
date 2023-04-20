using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WizardMakerTests")]

namespace WizardMaker.DataDomain.Models;

// Basic XP Pool that allows any ability spend.
// This should be used last in sort order, since we want to expend the most versatile sources last.
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

    public override int SortOrder()
    {
        return 100;
    }
}
