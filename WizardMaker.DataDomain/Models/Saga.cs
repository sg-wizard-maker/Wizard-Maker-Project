﻿using System;
using System.Collections.Generic;
using WizardMaker.DataDomain.Models.Virtues;

namespace WizardMaker.DataDomain.Models;

/// <summary>
/// An object containing all of the materials for an ArM5 Saga, such as (Covenants, Characters, etc).
/// This includes Archetypal data, such as (ArchAbility, ArchVirtue, ...), 
/// as one Saga might differ from another in using additional/different materials or house-ruled versions.
/// 
/// For the current scope, this class will only be minimally developed.
/// For future releases, Saga will surely keep track of more entities, and will likely have more information overall.
/// </summary>
public class Saga : IObjectForRegistrar
{
    #region Members related to ObjRegistrar
    public static Saga? CurrentSaga { get; set; }
    public static ObjRegistrar<Saga>      RegistrarSagas      = new();
    public        ObjRegistrar<Covenant>  RegistrarCovenants  = new();
    public        ObjRegistrar<Character> RegistrarCharacters = new();

    public        ObjRegistrar<ArchVirtue>  RegistrarArchVirtues   = new();
    public        ObjRegistrar<ArchAbility> RegistrarArchAbilities = new();


    // Notes on classes that will / will not (be registered & tracked within Saga, get a Guid for ID):
    // 
    // Classes to be (objects registered + objects have ID)
    // - Saga
    // - Covenant
    // - Character
    // - (in future, classes related to Laboratory, Library, etc)
    // - classes with Archetypal data (ArchVirtue, ArchAbility, ..., and some related such as VirtueType, ...)
    // - possibly class for a Journal entire
    // 
    // Classes to be (objects NOT registered + objects do NOT have ID)
    // - (classes which are part of a registered object, such as CharacterData, ...
    // - Instance data owned/contained by a registered object (VirtueInstance, AbilityInstance, ...)
    // - (Journal entry class(es))
    // - ...
    // - 
    // - 

    public Guid   Id        { get; private set; }
    public string CanonName { get; private set; }
    #endregion

    public string Name      { get; set; }

    #region Constructors
    public Saga(string name, Guid? existingId = null)
    {
        this.Name      = name;
        this.CanonName = ObjRegistrar<Saga>.MakeCanonName(name);
        this.Id        = (existingId != null) ? existingId.Value : Guid.NewGuid();

        Saga.RegistrarSagas.Register(this);
        if (Saga.CurrentSaga == null)
        {
            Saga.SetCurrentSaga(this);
        }
    }
    #endregion

    #region Static Constructor
    static Saga()
    {
        // A placeholder, until whatever infrastructure (for main program, and for tests) is in place...
        Saga.CurrentSaga = new Saga("Placeholder Saga Name");
    }
    #endregion

    #region Static Methods (various)
    public static void SetCurrentSaga(Saga currentSaga) 
    {
        Saga.CurrentSaga = currentSaga;
    }
    #endregion

    #region Methods (various)
    // ...nothing yet
    #endregion
}
