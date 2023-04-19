using System;
using System.Collections.Generic;

namespace WizardMaker.DataDomain.Models
{
    /// <summary>
    /// An object containing all of the materials for an ArM5 Saga, such as (Covenants, Characters, etc).
    /// This includes Archetypal data, such as (ArchAbility, ArchVirtue, ...), 
    /// as one Saga might differ from another in using additional/different materials or house-ruled versions.
    /// </summary>
    public class Saga : IObjectForRegistrar
    {
        #region Members related to ObjRegistrar
        public static ObjRegistrar<Saga> Registrar = new();

        public Guid   Id        { get; private set; }
        public string CanonName { get; private set; }
        #endregion

        public string Name      { get; set; }

        public Saga(string name, string canonName, Guid? existingId = null)
        {
            this.Name      = name;
            this.CanonName = canonName;
            this.Id        = (existingId != null) ? existingId.Value : Guid.NewGuid();

            Saga.Registrar.Register(this);
        }
    }
}
