﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMaker.DataDomain.Models;

namespace WizardMaker.DataDomain.Models
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
        private static void validateSpendXPOnAbility(ArchAbility archAbility, int xp)
        {
            
        }

        public static AbilityInstance createNewAbilityInstance(string ability, int xp, string specialty, string journalID, bool isPuissant, bool isAffinity)
        {
            // Create the ability instance
            ArchAbility archAbility = ArchAbility.lookupCommonAbilities(ability);
            validateSpendXPOnAbility(archAbility, xp);

            return new AbilityInstance(archAbility, journalID, xp, specialty, hasPuissance:isPuissant, hasAffinity:isAffinity);
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