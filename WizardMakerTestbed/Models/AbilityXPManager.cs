using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
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
        public ValidationResult validateSpendXPOnAbility(ArchAbility archAbility, int xp)
        {
            // TODO: Insert validation check here
            // TODO: Throw validation exception instead?  YES
            return new ValidationResult();
        }


        public AbilityInstance createNewAbilityInstance(string ability, int xp, string specialty)
        {
            // Create the ability instance
            ArchAbility archAbility = ArchAbility.lookupCommonAbilities(ability);
            validateSpendXPOnAbility(archAbility, xp);

            return new AbilityInstance(archAbility, xp, specialty);
        }
    }
}
