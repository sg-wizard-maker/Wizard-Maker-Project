using System;
using System.Collections.Generic;

namespace WizardMaker.DataDomain.Models
{
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

        public Guid   Id        { get; private set; }
        public string CanonName { get; private set; }
        #endregion

        public string Name      { get; set; }

        #region Constructors
        public Saga(string name, string canonName, Guid? existingId = null)
        {
            this.Name      = name;
            this.CanonName = canonName;
            this.Id        = (existingId != null) ? existingId.Value : Guid.NewGuid();

            Saga.RegistrarSagas.Register(this);
            if (Saga.CurrentSaga == null)
            {
                Saga.CurrentSaga = this;
            }
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
}
