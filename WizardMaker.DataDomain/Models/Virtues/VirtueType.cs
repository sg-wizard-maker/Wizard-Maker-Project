using System;
using System.Collections.Generic;
using WizardMaker.DataDomain.Models.Virtues.VirtueCommands;

namespace WizardMaker.DataDomain.Models.Virtues
{
    public class VirtueType
    {
        public static List<VirtueType> Types = new List<VirtueType>();

        #region Properties
        public string Abbreviation { get; private set; }
        public string Name         { get; private set; }
        #endregion

        #region Constructors
        // This is only public so that it can be available for serialization
        // ...That may well change, as in future there seems to be need to be able to define new Virtues, etc at runtime...
        public VirtueType(string abbrev, string name)
        {
            this.Abbreviation = abbrev;
            this.Name         = name;
        }
        #endregion

        public static VirtueType Special      = new VirtueType("Special",      "Special");  // The Gift, also some from other books have this type
        public static VirtueType Hermetic     = new VirtueType("Hermetic",     "Hermetic");
        public static VirtueType Supernatural = new VirtueType("Supernatural", "Supernatural");
        public static VirtueType SocialStatus = new VirtueType("Social",       "Social");
        public static VirtueType General      = new VirtueType("General",      "General");
        // In HoH:MC another type is defined "Heroic", which we will eventually support...
        // There are some twists which complicate that,
        // like Mythic Blood being BOTH Hermetic and Heroic,
        // which may require we re-arrange infrastructure to allow for such things.

        #region Static Constructor
        static VirtueType()
        {
            Types.Add(Special);
            Types.Add(Hermetic);
            Types.Add(General);
            Types.Add(SocialStatus);
            Types.Add(Supernatural);
        }
        #endregion
    }
}
