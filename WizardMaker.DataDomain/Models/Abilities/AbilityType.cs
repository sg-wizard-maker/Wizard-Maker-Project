using System;
using System.Collections.Generic;
using System.ComponentModel;  // various Attributes

namespace WizardMaker.DataDomain.Models
{
    /// <summary>
    /// These are types of Abilities as defined in the ArM5 rules.
    /// Characters have access to learn Abilities based upon their type,
    /// and whether the character has an associated Virtue or Flaw granting access.
    /// The types are:
    /// - [Gc]   General (child) - The initial 120 XP for "Early Life" can be spent on these
    /// - [G]    General         - Any character can spend XP on these
    /// - [M]    Martial         - Requires a Virtue/Flaw to spend XP on these during chargen
    /// - [Acad] Academic        - Requires literacy, granted by certain Virtues
    /// - [Arc]  Arcane          - Requires certain Virtues/Flaws
    /// - [Sup]  Supernatural    - Each individual Ability in this grouping requires a specific Virtue (or Gifted tradition)
    /// 
    /// - [Sec]  Secret          - Not used in ArM5 core book, but useful to tag Abilities distinguished by secret origin, such as certain Org Lores
    /// </summary>
    public class AbilityType
    {
        #region Properties and Fields
        public static List<AbilityType> Types = new List<AbilityType>();

        public string Abbreviation { get; private set; }
        public string Name         { get; private set; }
        // TODO:
        // Need some additional properties defining the relations to
        // requirements/associations with certain Virtues/Flaws, age categories, etc...
        //
        // For example, these V/F grant access to
        // - Martial:      (Berserk, Knight, Warrior, Mercenary Captain, Branded Criminal, Outlaw, Outlaw Leader)
        // - Academic:     (Clerk, Educated, Magister in Artibus, Mendicant Friar, Priest)
        // - Arcane:       (Arcane Lore)
        // - Supernatural: (each corresponding same-name Virtue, but also cases such as Strong Faerie Blood granting Second Sight)
        // 
        // And then there are interesting cases, such as:
        // - Custos                (grants access to ONE category: Academic, Arcane, Martial)
        // - Faerie Blood          (Faerie Lore)
        // - Failed Apprentice     (Academic + Arcane)
        // - Privileged Upbringing (Academic + Martial) - but only for the +50 XP (excluding the Later Life pool)
        // - Redcap                (Academic + Arcane + Martial)
        // - Strong Faerie Blood   (Faerie Lore, plus Second Sight)
        // - Student of <Realm>    (<Realm> Lore, for the chosen Realm)
        // - Wise One              (grants access to ONE category: Academic, Arcane)
        // 
        // - Ability Block         (REMOVES access to a class of Abilities; scope of class is not precisely defined in rules)
        // - Covenant Upbringing   (grants access 2 Abilities: Dead Lang: Latin, Org Lore: Order of Hermes)
        // - Diabolic Past         (Infernal Lore)
        // - Enfeebled             (unable to learn Martial, or (other skills involving physical exertion) )
        // - Faerie Friend         (Faerie Lore)
        // - Faerie Upbringing     (Faerie Lore)
        // - Feral Upbringing      (alters how Early Life XP can be spent, prevents XP on Langauges)
        // - Sheltered Upbringing  (may not take Bargain, Charm, Etiquette, Folk Ken, Guile, Intrigue, Leadership)
        // 
        #endregion

        #region Constructors
        // This is only public so that it can be available for serialization
        public AbilityType(string abbrev, string name)
        {
            this.Abbreviation = abbrev;
            this.Name         = name;
        }
        #endregion

        public static AbilityType GenChild     = new AbilityType("[Gc]",   "General (child)");
        public static AbilityType General      = new AbilityType("[G]",    "General");
        public static AbilityType Martial      = new AbilityType("[M]",    "Martial");
        public static AbilityType Academic     = new AbilityType("[Acad]", "Academic");
        public static AbilityType Arcane       = new AbilityType("[Arc]",  "Arcane");
        public static AbilityType Supernatural = new AbilityType("[Sup]",  "Supernatural");
        public static AbilityType Secret       = new AbilityType("[Sec]",  "Secret");

        #region Static Constructor
        static AbilityType()
        {
            Types.Add(GenChild);
            Types.Add(General);
            Types.Add(Martial);
            Types.Add(Academic);
            Types.Add(Arcane);
            Types.Add(Supernatural);
        }
        #endregion
    }
}
