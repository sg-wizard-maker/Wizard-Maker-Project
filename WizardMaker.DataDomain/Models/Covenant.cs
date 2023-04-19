using System;
using System.Collections.Generic;

namespace WizardMaker.DataDomain.Models
{
    /// <summary>
    /// An object representing a community of wizards, 
    /// holding data related to this community
    /// including (Characters, Laboratories, Libraries, ...).
    /// 
    /// For the current scope of effort, this class will not be developed beyond the minimum to indicate the intended structure.
    /// For later releases, this will be a class of importance similar to Character.
    /// </summary>
    public class Covenant : IObjectForRegistrar
    {
        #region Members related to ObjRegistrar
        public static ObjRegistrar<Covenant> Registrar = new();

        public Guid   Id        { get; private set; }
        public string CanonName { get; private set; }
        #endregion

        public string Name { get; set; }

        public Covenant(string name, string canonName, Guid? existingId = null)
        {
            this.Name      = name;
            this.CanonName = canonName;
            this.Id        = (existingId != null) ? existingId.Value : Guid.NewGuid();

            Covenant.Registrar.Register(this);
        }
    }
}
