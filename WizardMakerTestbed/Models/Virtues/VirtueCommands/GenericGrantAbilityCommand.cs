using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues.VirtueCommands
{
    /** Grants an ability and a score of 1.  For implementation, we simply grant enough XP to get from 0 to 1.
     */
    public class GenericGrantAbilityCommand : ICharacterCommand
    {
        ArchAbility grantedAbility;
        public static string XPPOOL_NAME_SUFFIX = " virtue";

        public GenericGrantAbilityCommand(ArchAbility grantedAbility)
        {
            this.grantedAbility = grantedAbility;
        }

        // Note:  If you delete Animal Ken, you will delete journal entries that change its experience AND add the virtue.
        public void Execute(Character c)
        {
            c.AllowedAbilities.Add(grantedAbility);

            int initialXP = (c.affinityAbilities.Contains(grantedAbility.Name) ? 4 : 5);

            c.XPPoolList.Add(new SpecificAbilitiesXpPool(grantedAbility.Name + XPPOOL_NAME_SUFFIX, "", initialXP,
                new List<ArchAbility>() { grantedAbility }));

            // Create a dummy ID.  If you delete a virtue, there is a 1:1 correspondence b/w virtues and journal entries (unlike abilities and arts).
            CharacterRenderer.AddAbility(c, grantedAbility.Name, initialXP, "", c.IsInitialCharacterFinished(), AbilityInstance.createID());
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
