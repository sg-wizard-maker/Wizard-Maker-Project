using System;

namespace WizardMaker.DataDomain.Models;

// TODO: Delete this class unless it shows usefulness.
public class ValidationResult
{
    #region Properties and Fields
    readonly bool   IsValidated;
    readonly string FailureReason;
    #endregion

    #region Constructors
    public ValidationResult(string failureReason)
    {
        this.IsValidated   = false;
        this.FailureReason = failureReason;
    }

    public ValidationResult()
    {
        this.IsValidated   = true;
        this.FailureReason = "";
    }
    #endregion
}

// Note: Should this be a static class?
public class AbilityXPManager
{
    private static void ValidateSpendXPOnAbility(ArchAbility archAbility, int xp)
    {
        // Empty?
    }

    public static AbilityInstance CreateNewAbilityInstance(string ability, int xp, string specialty, string journalID, bool isPuissant, bool isAffinity)
    {
        // Create the ability instance
        ArchAbility archAbility = ArchAbility.LookupCommonAbilities(ability);
        ValidateSpendXPOnAbility(archAbility, xp);

        var result = new AbilityInstance(archAbility, journalID, xp, specialty, hasPuissance: isPuissant, hasAffinity: isAffinity);
        return result;
    }

    public static void DebitXPPoolsForAbility(AbilityInstance a, int xp, SortedSet<XPPool> XPPoolList)
    {
        int remainingXPToAllocate = xp;

        // Allocate the XP cost to the remaining pools.
        foreach (XPPool p in XPPoolList)
        {
            if (!p.CanSpendOnAbility(a.Ability))
            {
                continue;
            }
            int allocatedXP = Math.Min(p.RemainingXP, remainingXPToAllocate);

            // Adjust the pool
            p.RemainingXP -= allocatedXP;

            // Track whether we have allocated all of the necessary XP.
            remainingXPToAllocate -= allocatedXP;

            if (remainingXPToAllocate == 0)
            {
                break;
            }
        }

        if (remainingXPToAllocate > 0)
        {
            throw new ShouldNotBeAbleToGetHereException("Could not allocate XP for the ability " + a.Name + ".  Please send this error to a developer.");
        }
    }

    public static void DebitXPPoolsForAbility(AbilityInstance a, SortedSet<XPPool> XPPoolList)
    {
        DebitXPPoolsForAbility(a, a.XP, XPPoolList);
    }
}
