﻿using System;

using WizardMaker.DataDomain.Models.CharacterPersist;
using WizardMaker.DataDomain.Models.Virtues;

namespace WizardMaker.DataDomain.Models;

// TODO:
// We could separate out a character as a set of journal entries (and name, startingAge, and description)
// from a rendered character (with ability instances and XP Pools).
// This might simplify the design -- we could probably get rid of CharacterData and only need to serialize the Character.

/// <summary>
/// An object representing a single ArM5 character.
/// The Character object is the central piece, and the Journal is the primary source of data,
/// whereas information about a character (at a certain point in time) is __derived__ from the Journal contents.
///     This is a fundamental notion within the architecture which inspired the project;
///     but be aware that details about the particular classes which implement this 
///     will surely change over time as this vision is more fully realized.
/// 
/// For the current scope of development, this will cover only materials from the ArM5 core rulebook,
/// and will not develop special handling for certain features which are difficult to implement thoroughly.
///     (such as shape-shifting, which has considerable demands 
///     for things such as display of multiple variants of character data, etc)
///     
/// In the scope of future releases, which include non-placeholder support for (Covenants, Laboratories, ...),
/// Character objects will be referenced by various other object types (and vice-versa) such as
///     - Covenant has a set of Characters
///     - Character may have (one or more) Laboratories
///     - Character may reference many other Characters in biographical details (parens, apprentices, sodales, allies/enemies/rivals, ...)
/// and managing such relationships over time via Journal entries will be more complex.
///
/// </summary>
public class Character :IObjectForRegistrar
{
    #region Members related to ObjRegistrar
    public Guid   Id        { get; private set; }
    public string CanonName { get; private set; }
    #endregion

    #region Other Properties and Fields
    public string Name        { get; set; }
    public string Description { get; set; }

    public List<AbilityInstance> Abilities         { get; set; }
    public List<string>          PuissantAbilities { get; set; } = new List<string>();

    // TODO: We may want to make this a dictionary of ability names to a list of XP cost modifiers.
    public List<string>      AffinityAbilities { get; set; } = new List<string>();
    public SortedSet<XPPool> XPPoolList        { get; }

    // TODO: Refactor and do the whole allowed ability logic in a separate stateful instance.
    // The default list here are what all characters can have w/o additional virtues.
    public HashSet<AbilityType> AllowedAbilityTypes { get; private set; } =
        new HashSet<AbilityType>()
        {
            AbilityType.General, 
            AbilityType.GenChild
        };

    // This list is in addition (OR'ed) with the ability type list.
    // Anything on this list is allowed to be selected by the character without a validation error.
    public HashSet<ArchAbility> AllowedAbilities { get; private set; } = new HashSet<ArchAbility>();

    public int XpPerYear { get; set; } = 15;

    public List<VirtueInstance> Virtues { get; private set; }

    public int StartingAge { get; set; }

    private IJournalableManager JournalableManager { get; set; }

    private SortedSet<Journalable> JournalEntries { get { return JournalableManager.GetJournalables(); } }
    #endregion

    #region Constructors
    // TODO:
    // Consider variant constructors and how they are used.
    // We need to make sure that if there is an existing Guid (from deserializing a saved asset), it is preserved.
    // So, parameters to match...
    // 
    // It might make more sense to have a very short list of basic parameters,
    // and for most of the other materials to be filled out via method calls after the ctor (abilities, journal entries, etc)

    public Character(RawCharacter rc) 
        : this(rc.Name, rc.Description, new List<AbilityInstance>(), rc.JournalEntries, rc.StartingAge) { }
        
    public Character(string name, string description, int startingAge) 
        : this(name, description, new List<AbilityInstance>(), new List<Journalable>(), startingAge) { }

    public Character(string name, string description, List<AbilityInstance> abilities, List<Journalable> journalEntries, int startingAge, Guid? existingId = null)
    {
        this.CanonName   = "TODO: PLACEHOLDER (need to change args to include a CanonName): " + name;
        this.Id          = (existingId != null) ? existingId.Value : Guid.NewGuid();  // TODO: Guid(s) for Character assigned elsewhere need to be changed to use this
        this.Name        = name;
        this.Description = description;
        this.Abilities   = abilities;
        
        this.JournalableManager = new BasicJournalableManager();
        foreach(Journalable journalable in journalEntries)
        {
            this.JournalableManager.AddJournalable(journalable);
        }
        this.StartingAge = startingAge;
        this.XPPoolList  = new SortedSet<XPPool>(new XPPoolComparer());
        this.Virtues     = new List<VirtueInstance>();

        // TODO: 
        // Currently, many tests fail, apparently because (with the current in-progress implementation)
        // calling .Register fails, on the second of two intended-as-identical objects which are to be compared.
        // 
        // Therefore, with the line below uncommented, many test failures abound:

        //if (Saga.CurrentSaga == null)
        //{
        //    string msg = string.Format("Attempt to register Character with no CurrentSaga!");
        //    throw new Exception(msg);
        //}
        //Saga.CurrentSaga.RegistrarCharacters.Register(this);

    }
    #endregion

    #region Methods (various)
    public void EndCharacterCreation()
    {
        JournalableManager.IsChargenMode = false;
    }

    public bool IsInitialCharacterFinished()
    {
        return !JournalableManager.IsChargenMode;
    }

    public void AddJournalable(Journalable journalable)
    {
        JournalableManager.AddJournalable(journalable); 
    }

    public void RemoveJournalable(string text) 
    {
        JournalableManager.RemoveJournalEntry(text); 
    }

    public SortedSet<Journalable> GetJournal() 
    {
        return JournalableManager.GetJournalables(); 
    }

    // Assumes that the last element in the XP Pool list is the overdrawn pool.
    public int TotalRemainingXPWithoutOverdrawn()
    {
        int result = 0;
        for (int i = 0; i < XPPoolList.Count - 1; i++)
        {
            result += XPPoolList.ElementAt(i).RemainingXP;
        }
        return result;
    }

    public void ResetAbilities()
    {
        Abilities = new List<AbilityInstance>(); 
    }

    public bool IsAbilityAllowedToBePurchased(ArchAbility a)
    {
        if (this.AllowedAbilityTypes.Contains(a.Type)) return true;
        if (this.AllowedAbilities.Contains(a)        ) return true;
        return false;
    }
    #endregion
}
