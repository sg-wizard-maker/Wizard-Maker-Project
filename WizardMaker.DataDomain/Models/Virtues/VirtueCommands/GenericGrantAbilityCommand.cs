﻿using System;

namespace WizardMaker.DataDomain.Models.Virtues.VirtueCommands;

// Grants an ability and a score of 1.
// For implementation, we simply grant enough XP to grant a Score of 1.
public class GenericGrantAbilityCommand : ICharacterCommand
{
    ArchAbility GrantedAbility;

    public static string XPPOOL_NAME_SUFFIX = " virtue";

    public GenericGrantAbilityCommand(ArchAbility grantedAbility)
    {
        this.GrantedAbility = grantedAbility;
    }

    #region Implementing ICharacterCommand
    // Note: If you delete Animal Ken, you will delete journal entries that change its experience AND add the virtue.
    public void Execute(Character c)
    {
        c.AllowedAbilities.Add(GrantedAbility);

        const int XP_FOR_SCORE_1               = 5;
        const int XP_FOR_SCORE_1_WITH_AFFINITY = 4;  // (5 * 2/3) --> 3.333, rounded up to 4
        int initialXP = c.AffinityAbilities.Contains(GrantedAbility.Name) ? XP_FOR_SCORE_1_WITH_AFFINITY : XP_FOR_SCORE_1;

        c.XPPoolList.Add(
            new SpecificAbilitiesXpPool(
                GrantedAbility.Name + XPPOOL_NAME_SUFFIX, 
                "", 
                initialXP,
                new List<ArchAbility>() { GrantedAbility }
            )
        );

        // Create a dummy ID.
        // If you delete a virtue, there is a 1:1 correspondence b/w virtues and journal entries (unlike abilities and arts).
        CharacterRenderer.AddAbility(c, GrantedAbility.Name, initialXP, "", c.IsInitialCharacterFinished(), AbilityInstance.CreateID());
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }
    #endregion
}
