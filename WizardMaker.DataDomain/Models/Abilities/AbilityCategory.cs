using System;
using System.Collections.Generic;
using System.ComponentModel;  // various Attributes

namespace WizardMaker.DataDomain.Models
{
    /// <summary>
    /// These are groupings of related Abilities for display, such as:
    ///     Martial, Physical, Social, Languages, Crafts, Professions, 
    ///     Academic, Supernatural, Arcane, Area Lores, Organization Lores
    /// Note that the names of some of these groupings are identical to 
    /// the names of AbilityTypes, such as:
    ///     (Martial, Academic, Arcane, Supernatural)
    ///     
    /// AbilityCategories are not defined as such in the ArM5 rules,
    /// but rather are used for convenience of display.
    /// - Some people prefer a single alphabetized list, which has advantages for a very short list of Abilities.
    /// - Others prefer a number of AbilityCategories (possibly alphabetized within each).
    /// </summary>
    public static class AbilityCategory
    {
        public static List<string> Categories = new List<string>();

        public static string Martial      = "Martial Abilities";
        public static string Physical     = "Physical Abilities";
        public static string Social       = "Social Abilities";
        public static string Languages    = "Languages";
        public static string Crafts       = "Crafts";
        public static string Professions  = "Professions";
        public static string Academic     = "Academic Abilities";
        public static string Supernatural = "Supernatural Abilities";
        public static string Arcane       = "Arcane Abilities";
        public static string RealmLores   = "Supernatural Realms";
        public static string AreaLores    = "Area Lores";
        public static string OrgLores     = "Organization Lores";

        #region Static Constructor
        static AbilityCategory()
        {
            Categories.Add(Martial);
            Categories.Add(Physical);
            Categories.Add(Social);
            Categories.Add(Languages);
            Categories.Add(Crafts);
            Categories.Add(Professions);
            Categories.Add(Academic);
            Categories.Add(Supernatural);
            Categories.Add(Arcane);
            Categories.Add(RealmLores);
            Categories.Add(AreaLores);
            Categories.Add(OrgLores);
        }
        #endregion
    }
}
