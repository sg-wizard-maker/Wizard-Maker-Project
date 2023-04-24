using System.Runtime.CompilerServices;
using WizardMaker.DataDomain.Validation;

[assembly: InternalsVisibleTo("WizardMakerTests")]

namespace WizardMaker.DataDomain.Models;

public class AllowOverdrawnXpPool : XPPool
{
    const string OVERDRAWN_NAME        = "Overdraw";
    const string OVERDRAWN_DESCRIPTION = "Overdraw experience pool that allows negative remaining XP.  Each character can only have one.";

    // TODO:
    // This should just be a method that shows that it always has enough XP.
    // Rather than making this a ridiculously high number.
    const int OVERDRAWN_AMOUNT = int.MaxValue - 1;

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
