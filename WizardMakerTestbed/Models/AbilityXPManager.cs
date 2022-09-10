using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    //TODO: Delete this class unless it shows usefulness.
    public class ValidationResult
    {
        readonly Boolean isValidated;
        readonly string failureReason;

        public ValidationResult(string failureReason)
        {
            this.isValidated = false;
            this.failureReason = failureReason;
        }

        public ValidationResult()
        {
            this.isValidated = true;
            this.failureReason = "";
        }
    }
    public class AbilityXPManager
    {
        private static ValidationResult validateSpendXPOnAbility(ArchAbility archAbility, int xp)
        {
            // TODO: Insert validation check here
            // TODO: Throw validation exception instead?  YES
            return new ValidationResult();
        }

        public static AbilityInstance createNewAbilityInstance(string ability, int xp, string specialty, string journalID, bool isPuissant)
        {
            // Create the ability instance
            ArchAbility archAbility = ArchAbility.lookupCommonAbilities(ability);
            validateSpendXPOnAbility(archAbility, xp);

            return new AbilityInstance(archAbility, journalID, xp, specialty, hasPuissance:isPuissant);
        }

        public static void debitXPPoolsForAbility(AbilityInstance a, int xp, SortedSet<XPPool> XPPoolList)
        {
            int remainingXPToAllocate = xp;

            // Allocate the XP cost to the remaining pools.
            foreach (XPPool p in XPPoolList)
            {
                if (p.CanSpendOnAbility(a.Ability))
                {
                    int allocatedXP = Math.Min(p.remainingXP, remainingXPToAllocate);

                    // Adjust the pool
                    p.remainingXP -= allocatedXP;

                    // Track whether we have allocated all of the necessary XP.
                    remainingXPToAllocate -= allocatedXP;

                    if (remainingXPToAllocate == 0)
                    {
                        break;
                    }
                }
            }

            if (remainingXPToAllocate > 0)
            {
                throw new ShouldNotBeAbleToGetHereException("Could not allocate XP for the ability " + a.Name + ".  Please send this error to a developer.");
            }
        }
        public static void debitXPPoolsForAbility(AbilityInstance a, SortedSet<XPPool> XPPoolList)
        {
            debitXPPoolsForAbility(a, a.XP, XPPoolList);
        }
    }
}
