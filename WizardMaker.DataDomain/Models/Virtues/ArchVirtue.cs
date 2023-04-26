using System;
using System.Collections.Generic;
using WizardMaker.DataDomain.Models.Virtues.VirtueCommands;

namespace WizardMaker.DataDomain.Models.Virtues;

public class ArchVirtue : IObjectForRegistrar
{
    #region Constants
    public const string PUISSANT_PREFIX = "Puissant ";
    public const string AFFINITY_PREFIX = "Affinity with ";
    #endregion

    #region Members related to ObjRegistrar
    public Guid   Id        { get; private set; }
    public string CanonName { get; private set; }
    #endregion

    #region Properties
    public string             Name             { get; private set; }
    public string             Description      { get; private set; }
    public VirtueType         Type             { get; private set; }
    public VirtueCost         MajorMinor       { get; private set; }
    public ICharacterCommand? CharacterCommand { get; private set; }
    #endregion

    #region Static Properties
    // Access ArchVirtue instances with this dictionary.
    //   Dev note:  Do not use this dictionary in the instantiation of the VirtueCommands
    public static Dictionary<string, ArchVirtue> NameToArchVirtue = new Dictionary<string, ArchVirtue>();

    // Note:
    // It may be useful to have a few other lookup structures, such as for (Social Status Virtues/Flaws) or (Hermetic Virtues), etc.
    // Or possibly using LINQ queries upon one central V/F lookup structure may be handy, for uses such as
    //     - to get (all Social Statuses)
    //     - to get (all Minor Virtues that grant a new Ability)
    //     - to get (all Virtues that are "parameterized" such as (Affinity with X, Puissant X, Cautious with X, ...)
    // and so forth...
    #endregion

    #region Constructors
    public ArchVirtue(string name, string description, VirtueType type, VirtueCost cost, ICharacterCommand? characterCommand, Guid? existingGuid = null)
    {
        this.Id               = (existingGuid != null) ? existingGuid.Value : Guid.NewGuid();
        this.CanonName        = ObjRegistrar<ArchVirtue>.MakeCanonName(name);
        this.Name             = name;
        this.Type             = type;
        this.Description      = description;
        this.MajorMinor       = cost;
        this.CharacterCommand = characterCommand;

        if (Saga.CurrentSaga == null)
        {
            string msg = string.Format("Attempt to register ArchVirtue with no CurrentSaga!");
            throw new Exception(msg);
        }
        Saga.CurrentSaga.RegistrarArchVirtues.Register(this);
    }

    public ArchVirtue(string name, string description, VirtueType type, VirtueCost cost, Guid? existingGuid = null)
        : this(name, description, type, cost, null, existingGuid)
    {
        // Empty
    }
    #endregion

    #region Methods (various)
    // Helps the front end decide if the Virtue has been implemented in the backend.
    // This will help while functionality is still incomplete.
    public bool IsImplemented()
    {
        bool result = (CharacterCommand != null);
        return result;
    }
    #endregion

    #region Static Constructor
    // TODO:
    // In future, rather than a hard-coded list, the program will offer the means to declare a new Virtue at runtime.
    // 
    // This will be especially needful, when considering "wildcard" Virtues which describe a list of many virtues,
    // such as:
    //     Way of the (Land)
    static ArchVirtue ()
    {
        // Implement puissant abilities as a dictionary to an arch virtue
        foreach (ArchAbility a in ArchAbility.AllCommonAbilities)
        {
            string puissantVirtueName = PUISSANT_PREFIX + a.Name;
            string affinityVirtueName = AFFINITY_PREFIX + a.Name;
            ArchVirtue.NameToArchVirtue[puissantVirtueName] = 
                new ArchVirtue(
                    puissantVirtueName, 
                    "Puissant in the ability " + a.Name, 
                    VirtueType.General, 
                    VirtueCost.Minor, 
                    new PuissantAbilityCommand(a)
                );
            ArchVirtue.NameToArchVirtue[affinityVirtueName] = 
                new ArchVirtue(
                    affinityVirtueName, 
                    "Affinity with the ability " + a.Name, 
                    VirtueType.General,
                    VirtueCost.Minor, 
                    new AffinityAbilityCommand(a)
                );
        }
        ArchVirtue.PopulateVirtueDictionary();
    }
    #endregion

    // TODO: Define the keys as constants

    #region Individual ArchVirtue Instances
    private static ArchVirtue TheGift = new ArchVirtue("TheGift", "TheGift", VirtueType.Special, VirtueCost.Free);

    // Hermetic Major
    private static ArchVirtue DiedneMagic                = new ArchVirtue("DiedneMagic",                 "DiedneMagic",                 VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue ElementalMagic             = new ArchVirtue("ElementalMagic",              "ElementalMagic",              VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue FlawlessMagic              = new ArchVirtue("FlawlessMagic",               "FlawlessMagic",               VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue FlexibleFormulaicMagic     = new ArchVirtue("FlexibleFormulaicMagic",      "FlexibleFormulaicMagic",      VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue GentleGift                 = new ArchVirtue("GentleGift",                  "GentleGift",                  VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue LifeLinkedSpontaneousMagic = new ArchVirtue("Life-LinkedSpontaneousMagic", "Life-LinkedSpontaneousMagic", VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue MajorMagicalFocus          = new ArchVirtue("MajorMagicalFocus",           "MajorMagicalFocus",           VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue MercurianMagic             = new ArchVirtue("MercurianMagic",              "MercurianMagic",              VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue MythicBlood                = new ArchVirtue("MythicBlood",                 "MythicBlood",                 VirtueType.Hermetic, VirtueCost.Major);
    private static ArchVirtue SecondaryInsight           = new ArchVirtue("SecondaryInsight",            "SecondaryInsight",            VirtueType.Hermetic, VirtueCost.Major);
    
    // Supernatural Major
    private static ArchVirtue Entrancement          = new ArchVirtue("Entrancement",          "Entrancement",          VirtueType.Supernatural, VirtueCost.Major);
    private static ArchVirtue GreaterImmunity       = new ArchVirtue("GreaterImmunity",       "GreaterImmunity",       VirtueType.Supernatural, VirtueCost.Major);
    private static ArchVirtue GreaterPurifyingTouch = new ArchVirtue("GreaterPurifyingTouch", "GreaterPurifyingTouch", VirtueType.Supernatural, VirtueCost.Major);
    private static ArchVirtue Shapeshifter          = new ArchVirtue("Shapeshifter",          "Shapeshifter",          VirtueType.Supernatural, VirtueCost.Major);
    private static ArchVirtue StrongFaerieBlood     = new ArchVirtue("StrongFaerieBlood",     "StrongFaerieBlood",     VirtueType.Supernatural, VirtueCost.Major);
    
    // Social Status Major
    private static ArchVirtue LandedNoble       = new ArchVirtue("LandedNoble",       "LandedNoble",       VirtueType.SocialStatus, VirtueCost.Major);
    private static ArchVirtue MagisterinArtibus = new ArchVirtue("MagisterinArtibus", "MagisterinArtibus", VirtueType.SocialStatus, VirtueCost.Major);
    private static ArchVirtue Redcap            = new ArchVirtue("Redcap",            "Redcap",            VirtueType.SocialStatus, VirtueCost.Major);
    
    // General Major
    private static ArchVirtue DeathProphecy = new ArchVirtue("DeathProphecy",   "DeathProphecy",   VirtueType.General, VirtueCost.Major);
    private static ArchVirtue GhostlyWarder = new ArchVirtue("GhostlyWarder",   "GhostlyWarder",   VirtueType.General, VirtueCost.Major);
    private static ArchVirtue GiantBlood    = new ArchVirtue("GiantBlood",      "GiantBlood",      VirtueType.General, VirtueCost.Major);
    private static ArchVirtue GuardianAngel = new ArchVirtue("GuardianAngel",   "GuardianAngel",   VirtueType.General, VirtueCost.Major);
    private static ArchVirtue TrueFaith     = new ArchVirtue("TrueFaith",       "TrueFaith",       VirtueType.General, VirtueCost.Major);
    private static ArchVirtue WaysoftheLand = new ArchVirtue("Waysofthe(Land)", "Waysofthe(Land)", VirtueType.General, VirtueCost.Major);
    private static ArchVirtue Wealthy       = new ArchVirtue("Wealthy",         "Wealthy",         VirtueType.General, VirtueCost.Major, new WealthyCommand());
    
    // Hermetic Minor
    private static ArchVirtue AdeptLaboratoryStudent = new ArchVirtue("AdeptLaboratoryStudent", "AdeptLaboratoryStudent", VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue AffinitywithArt        = new ArchVirtue("AffinitywithArt",        "AffinitywithArt",        VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue CautiousSorcerer       = new ArchVirtue("CautiousSorcerer",       "CautiousSorcerer",       VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue CyclicMagicpositive    = new ArchVirtue("CyclicMagic(positive)",  "CyclicMagic(positive)",  VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue DeftForm               = new ArchVirtue("DeftForm",               "DeftForm",               VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue EnduringMagic          = new ArchVirtue("EnduringMagic",          "EnduringMagic",          VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue TheEnigma              = new ArchVirtue("TheEnigma",              "TheEnigma",              VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue FaerieMagic            = new ArchVirtue("FaerieMagic",            "FaerieMagic",            VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue FastCaster             = new ArchVirtue("FastCaster",             "FastCaster",             VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue FreeStudy              = new ArchVirtue("FreeStudy",              "FreeStudy",              VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue HarnessedMagic         = new ArchVirtue("HarnessedMagic",         "HarnessedMagic",         VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue Heartbeast             = new ArchVirtue("Heartbeast",             "Heartbeast",             VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue HermeticPrestige       = new ArchVirtue("HermeticPrestige",       "HermeticPrestige",       VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue InoffensivetoAnimals   = new ArchVirtue("InoffensivetoAnimals",   "InoffensivetoAnimals",   VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue InventiveGenius        = new ArchVirtue("InventiveGenius",        "InventiveGenius",        VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue LifeBoost              = new ArchVirtue("LifeBoost",              "LifeBoost",              VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue MinorMagicalFocus      = new ArchVirtue("MinorMagicalFocus",      "MinorMagicalFocus",      VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue MagicalMemory          = new ArchVirtue("MagicalMemory",          "MagicalMemory",          VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue MasteredSpells         = new ArchVirtue("MasteredSpells",         "MasteredSpells",         VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue MethodCaster           = new ArchVirtue("MethodCaster",           "MethodCaster",           VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue PersonalVisSource      = new ArchVirtue("PersonalVisSource",      "PersonalVisSource",      VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue PuissantArt            = new ArchVirtue("PuissantArt",            "PuissantArt",            VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue QuietMagic             = new ArchVirtue("QuietMagic",             "QuietMagic",             VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue SideEffect             = new ArchVirtue("SideEffect",             "SideEffect",             VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue SkilledParens          = new ArchVirtue("SkilledParens",          "SkilledParens",          VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue SpecialCircumstances   = new ArchVirtue("SpecialCircumstances",   "SpecialCircumstances",   VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue StudyBonus             = new ArchVirtue("StudyBonus",             "StudyBonus",             VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue SubtleMagic            = new ArchVirtue("SubtleMagic",            "SubtleMagic",            VirtueType.Hermetic, VirtueCost.Minor);
    private static ArchVirtue VerditiusMagic         = new ArchVirtue("VerditiusMagic",         "VerditiusMagic",         VirtueType.Hermetic, VirtueCost.Minor);
    
    // Supernatural Minor
    private static ArchVirtue AnimalKen            = new ArchVirtue("AnimalKen",                  "AnimalKen",                  VirtueType.Supernatural, VirtueCost.Minor, new AnimalKenCommand());
    private static ArchVirtue Dowsing              = new ArchVirtue("Dowsing",                    "Dowsing",                    VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue EnchantingMusic      = new ArchVirtue("EnchantingMusic",            "EnchantingMusic",            VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue LesserImmunity       = new ArchVirtue("LesserImmunity",             "LesserImmunity",             VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue LesserPurifyingTouch = new ArchVirtue("LesserPurifyingTouch",       "LesserPurifyingTouch",       VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue MagicSensitivity     = new ArchVirtue("MagicSensitivity",           "MagicSensitivity",           VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue Premonitions         = new ArchVirtue("Premonitions",               "Premonitions",               VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue SecondSight          = new ArchVirtue("SecondSight",                "SecondSight",                VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue SenseHolyUnholy      = new ArchVirtue("SenseHolinessandUnholiness", "SenseHolinessandUnholiness", VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue Skinchanger          = new ArchVirtue("Skinchanger",                "Skinchanger",                VirtueType.Supernatural, VirtueCost.Minor);
    private static ArchVirtue WildernessSense      = new ArchVirtue("WildernessSense",            "WildernessSense",            VirtueType.Supernatural, VirtueCost.Minor);
    
    // Social Minor
    private static ArchVirtue Clerk            = new ArchVirtue("Clerk",            "Clerk",            VirtueType.SocialStatus, VirtueCost.Minor);
    private static ArchVirtue Custos           = new ArchVirtue("Custos",           "Custos",           VirtueType.SocialStatus, VirtueCost.Minor);
    private static ArchVirtue FailedApprentice = new ArchVirtue("FailedApprentice", "FailedApprentice", VirtueType.SocialStatus, VirtueCost.Minor);
    private static ArchVirtue Gentlemanwoman   = new ArchVirtue("Gentleman/woman",  "Gentleman/woman",  VirtueType.SocialStatus, VirtueCost.Minor);
    private static ArchVirtue Knight           = new ArchVirtue("Knight",           "Knight",           VirtueType.SocialStatus, VirtueCost.Minor);
    private static ArchVirtue MendicantFriar   = new ArchVirtue("MendicantFriar",   "MendicantFriar",   VirtueType.SocialStatus, VirtueCost.Minor);
    private static ArchVirtue MercenaryCaptain = new ArchVirtue("MercenaryCaptain", "MercenaryCaptain", VirtueType.SocialStatus, VirtueCost.Minor);
    private static ArchVirtue Priest           = new ArchVirtue("Priest",           "Priest",           VirtueType.SocialStatus, VirtueCost.Minor);
    private static ArchVirtue WiseOne          = new ArchVirtue("WiseOne",          "WiseOne",          VirtueType.SocialStatus, VirtueCost.Minor);
    
    // General Minor
    private static ArchVirtue AffinityWithAbility      = new ArchVirtue("AffinityWithAbility",        "AffinityWithAbility",        VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue AptStudent               = new ArchVirtue("AptStudent",                 "AptStudent",                 VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue ArcaneLore               = new ArchVirtue("ArcaneLore",                 "ArcaneLore",                 VirtueType.General, VirtueCost.Minor, new ArcaneLoreCommand());
    private static ArchVirtue Berserk                  = new ArchVirtue("Berserk",                    "Berserk",                    VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue BookLearner              = new ArchVirtue("BookLearner",                "BookLearner",                VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue CautiouswithAbility      = new ArchVirtue("CautiouswithAbility",        "CautiouswithAbility",        VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue ClearThinker             = new ArchVirtue("ClearThinker",               "ClearThinker",               VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue CommonSense              = new ArchVirtue("CommonSense",                "CommonSense",                VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Educated                 = new ArchVirtue("Educated",                   "Educated",                   VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue EnduringConstitution     = new ArchVirtue("EnduringConstitution",       "EnduringConstitution",       VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue FaerieBlood              = new ArchVirtue("FaerieBlood",                "FaerieBlood",                VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Famous                   = new ArchVirtue("Famous",                     "Famous",                     VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue FreeExpression           = new ArchVirtue("FreeExpression",             "FreeExpression",             VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue GoodTeacher              = new ArchVirtue("GoodTeacher",                "GoodTeacher",                VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Gossip                   = new ArchVirtue("Gossip",                     "Gossip",                     VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue GreatCharacteristic      = new ArchVirtue("Great Characteristic",       "Great Characteristic",       VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue ImprovedCharacteristics  = new ArchVirtue("ImprovedCharacteristics",    "ImprovedCharacteristics",    VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Inspirational            = new ArchVirtue("Inspirational",              "Inspirational",              VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Intuition                = new ArchVirtue("Intuition",                  "Intuition",                  VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue KeenVision               = new ArchVirtue("KeenVision",                 "KeenVision",                 VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Large                    = new ArchVirtue("Large",                      "Large",                      VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue LatentMagicalAbility     = new ArchVirtue("LatentMagicalAbility",       "LatentMagicalAbility",       VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue LearnAbilityfromMistakes = new ArchVirtue("Learn(Ability)fromMistakes", "Learn(Ability)fromMistakes", VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue LightTouch               = new ArchVirtue("LightTouch",                 "LightTouch",                 VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue LightningReflexes        = new ArchVirtue("LightningReflexes",          "LightningReflexes",          VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue LongWinded               = new ArchVirtue("Long-Winded",                "Long-Winded",                VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Luck                     = new ArchVirtue("Luck",                       "Luck",                       VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue RapidConvalescence       = new ArchVirtue("RapidConvalescence",         "RapidConvalescence",         VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue PerfectBalance           = new ArchVirtue("PerfectBalance",             "PerfectBalance",             VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue PiercingGaze             = new ArchVirtue("PiercingGaze",               "PiercingGaze",               VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue PrivilegedUpbringing     = new ArchVirtue("PrivilegedUpbringing",       "PrivilegedUpbringing",       VirtueType.General, VirtueCost.Minor, new PrivilegedUpbringingCommand());
    private static ArchVirtue Protection               = new ArchVirtue("Protection",                 "Protection",                 VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue PuissantAbility          = new ArchVirtue("PuissantAbility",            "PuissantAbility",            VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Relic                    = new ArchVirtue("Relic",                      "Relic",                      VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue ReservesofStrength       = new ArchVirtue("ReservesofStrength",         "ReservesofStrength",         VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue SelfConfident            = new ArchVirtue("Self-Confident",             "Self-Confident",             VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue SharpEars                = new ArchVirtue("SharpEars",                  "SharpEars",                  VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue SocialContacts           = new ArchVirtue("SocialContacts",             "SocialContacts",             VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue StrongWilled             = new ArchVirtue("Strong-Willed",              "Strong-Willed",              VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue StudentofRealm           = new ArchVirtue("Studentof(Realm)",           "Studentof(Realm)",           VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue TemporalInfluence        = new ArchVirtue("TemporalInfluence",          "TemporalInfluence",          VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Tough                    = new ArchVirtue("Tough",                      "Tough",                      VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue TroupeUpbringing         = new ArchVirtue("TroupeUpbringing",           "TroupeUpbringing",           VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue TrueLovePC               = new ArchVirtue("TrueLove(PC)",               "TrueLove(PC)",               VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Unaging                  = new ArchVirtue("Unaging",                    "Unaging",                    VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue VenusBlessing            = new ArchVirtue("Venus’Blessing",             "Venus’Blessing",             VirtueType.General, VirtueCost.Minor);
    private static ArchVirtue Warrior                  = new ArchVirtue("Warrior",                    "Warrior",                    VirtueType.General, VirtueCost.Minor, new WarriorCommand());
    private static ArchVirtue WellTraveled             = new ArchVirtue("Well-Traveled",              "Well-Traveled",              VirtueType.General, VirtueCost.Minor);
    
    // Social Free
    private static ArchVirtue Covenfolk     = new ArchVirtue("Covenfolk",     "Covenfolk",     VirtueType.SocialStatus, VirtueCost.Free);
    private static ArchVirtue Craftsman     = new ArchVirtue("Craftsman",     "Craftsman",     VirtueType.SocialStatus, VirtueCost.Free);
    private static ArchVirtue HermeticMagus = new ArchVirtue("HermeticMagus", "HermeticMagus", VirtueType.SocialStatus, VirtueCost.Free);
    private static ArchVirtue Merchant      = new ArchVirtue("Merchant",      "Merchant",      VirtueType.SocialStatus, VirtueCost.Free);
    private static ArchVirtue Peasant       = new ArchVirtue("Peasant",       "Peasant",       VirtueType.SocialStatus, VirtueCost.Free);
    private static ArchVirtue Wanderer      = new ArchVirtue("Wanderer",      "Wanderer",      VirtueType.SocialStatus, VirtueCost.Free);
    #endregion

    #region Static Methods
    private static void PopulateVirtueDictionary()
    {
        #region Populating the NameToArchVirtue Dictionary
        NameToArchVirtue.Add("TheGift", TheGift);

        // Hermetic Major
        NameToArchVirtue.Add("DiedneMagic",                 DiedneMagic);
        NameToArchVirtue.Add("ElementalMagic",              ElementalMagic);
        NameToArchVirtue.Add("FlawlessMagic",               FlawlessMagic);
        NameToArchVirtue.Add("FlexibleFormulaicMagic",      FlexibleFormulaicMagic);
        NameToArchVirtue.Add("GentleGift",                  GentleGift);
        NameToArchVirtue.Add("Life-LinkedSpontaneousMagic", LifeLinkedSpontaneousMagic);
        NameToArchVirtue.Add("MajorMagicalFocus",           MajorMagicalFocus);
        NameToArchVirtue.Add("MercurianMagic",              MercurianMagic);
        NameToArchVirtue.Add("MythicBlood",                 MythicBlood);  // Note: Package deal, select a MinorMagicalFocus AND design a power. Complexities to implement well...
        NameToArchVirtue.Add("SecondaryInsight",            SecondaryInsight);

        // Supernatural Major
        NameToArchVirtue.Add("Entrancement",          Entrancement);
        NameToArchVirtue.Add("GreaterImmunity",       GreaterImmunity);        // Note: Greater Immunity to <Something>,      so we may need more infrastructure...
        NameToArchVirtue.Add("GreaterPurifyingTouch", GreaterPurifyingTouch);  // Note: Greater Purifying Touch: <Something>, so we may  need more infrastructure...
        NameToArchVirtue.Add("Shapeshifter",          Shapeshifter);
        NameToArchVirtue.Add("StrongFaerieBlood",     StrongFaerieBlood);

        // Social Status Major
        NameToArchVirtue.Add("LandedNoble",       LandedNoble);
        NameToArchVirtue.Add("MagisterinArtibus", MagisterinArtibus);
        NameToArchVirtue.Add("Redcap",            Redcap);

        // General Major
        NameToArchVirtue.Add("DeathProphecy",   DeathProphecy);
        NameToArchVirtue.Add("GhostlyWarder",   GhostlyWarder);
        NameToArchVirtue.Add("GiantBlood",      GiantBlood);
        NameToArchVirtue.Add("GuardianAngel",   GuardianAngel);
        NameToArchVirtue.Add("TrueFaith",       TrueFaith);
        NameToArchVirtue.Add("Waysofthe(Land)", WaysoftheLand);  // Note: Ways of the <SomeLand>, so we may need more infrastructure...
        NameToArchVirtue.Add("Wealthy",         Wealthy);

        // Hermetic Minor
        NameToArchVirtue.Add("AdeptLaboratoryStudent", AdeptLaboratoryStudent);
        NameToArchVirtue.Add("AffinitywithArt",        AffinitywithArt);
        NameToArchVirtue.Add("CautiousSorcerer",       CautiousSorcerer);
        NameToArchVirtue.Add("CyclicMagic(positive)",  CyclicMagicpositive);
        NameToArchVirtue.Add("DeftForm",               DeftForm);  // Note: Def Form: <SomeForm>, so we may need more infrastructure...
        NameToArchVirtue.Add("EnduringMagic",          EnduringMagic);
        NameToArchVirtue.Add("TheEnigma",              TheEnigma);
        NameToArchVirtue.Add("FaerieMagic",            FaerieMagic);
        NameToArchVirtue.Add("FastCaster",             FastCaster);
        NameToArchVirtue.Add("FreeStudy",              FreeStudy);
        NameToArchVirtue.Add("HarnessedMagic",         HarnessedMagic);
        NameToArchVirtue.Add("Heartbeast",             Heartbeast);
        NameToArchVirtue.Add("HermeticPrestige",       HermeticPrestige);
        NameToArchVirtue.Add("InoffensivetoAnimals",   InoffensivetoAnimals);
        NameToArchVirtue.Add("InventiveGenius",        InventiveGenius);
        NameToArchVirtue.Add("LifeBoost",              LifeBoost);
        NameToArchVirtue.Add("MinorMagicalFocus",      MinorMagicalFocus);  // Note: Minor Magical Focus: <SomeFocus>, so we may need more infrastructure...
        NameToArchVirtue.Add("MagicalMemory",          MagicalMemory);
        NameToArchVirtue.Add("MasteredSpells",         MasteredSpells);
        NameToArchVirtue.Add("MethodCaster",           MethodCaster);
        NameToArchVirtue.Add("PersonalVisSource",      PersonalVisSource);
        NameToArchVirtue.Add("PuissantArt",            PuissantArt);
        NameToArchVirtue.Add("QuietMagic",             QuietMagic);
        NameToArchVirtue.Add("SideEffect",             SideEffect);
        NameToArchVirtue.Add("SkilledParens",          SkilledParens);
        NameToArchVirtue.Add("SpecialCircumstances",   SpecialCircumstances);
        NameToArchVirtue.Add("StudyBonus",             StudyBonus);
        NameToArchVirtue.Add("SubtleMagic",            SubtleMagic);
        NameToArchVirtue.Add("VerditiusMagic",         VerditiusMagic);

        // Supernatural Minor
        NameToArchVirtue.Add("AnimalKen",                  AnimalKen);
        NameToArchVirtue.Add("Dowsing",                    Dowsing);
        NameToArchVirtue.Add("EnchantingMusic",            EnchantingMusic);
        NameToArchVirtue.Add("LesserImmunity",             LesserImmunity);
        NameToArchVirtue.Add("LesserPurifyingTouch",       LesserPurifyingTouch);
        NameToArchVirtue.Add("MagicSensitivity",           MagicSensitivity);
        NameToArchVirtue.Add("Premonitions",               Premonitions);
        NameToArchVirtue.Add("SecondSight",                SecondSight);
        NameToArchVirtue.Add("SenseHolinessandUnholiness", SenseHolyUnholy);
        NameToArchVirtue.Add("Skinchanger",                Skinchanger);
        NameToArchVirtue.Add("WildernessSense",            WildernessSense);

        // Social Minor
        NameToArchVirtue.Add("Clerk",            Clerk);
        NameToArchVirtue.Add("Custos",           Custos);
        NameToArchVirtue.Add("FailedApprentice", FailedApprentice);
        NameToArchVirtue.Add("Gentleman/woman",  Gentlemanwoman);
        NameToArchVirtue.Add("Knight",           Knight);
        NameToArchVirtue.Add("MendicantFriar",   MendicantFriar);
        NameToArchVirtue.Add("MercenaryCaptain", MercenaryCaptain);
        NameToArchVirtue.Add("Priest",           Priest);
        NameToArchVirtue.Add("WiseOne",          WiseOne);

        // General Minor
        NameToArchVirtue.Add("AffinitywithAbility",        AffinityWithAbility);  // Note: Infrastructure exists to generate variants for this, do we need more?
        NameToArchVirtue.Add("AptStudent",                 AptStudent);
        NameToArchVirtue.Add("ArcaneLore",                 ArcaneLore);
        NameToArchVirtue.Add("Berserk",                    Berserk);
        NameToArchVirtue.Add("BookLearner",                BookLearner);
        NameToArchVirtue.Add("CautiouswithAbility",        CautiouswithAbility);  // Note: Cautious with <SomeAbility>, so we may need more infrastructure...
        NameToArchVirtue.Add("ClearThinker",               ClearThinker);
        NameToArchVirtue.Add("CommonSense",                CommonSense);
        NameToArchVirtue.Add("Educated",                   Educated);
        NameToArchVirtue.Add("EnduringConstitution",       EnduringConstitution);
        NameToArchVirtue.Add("FaerieBlood",                FaerieBlood);
        NameToArchVirtue.Add("Famous",                     Famous);
        NameToArchVirtue.Add("FreeExpression",             FreeExpression);
        NameToArchVirtue.Add("GoodTeacher",                GoodTeacher);
        NameToArchVirtue.Add("Gossip",                     Gossip);
        NameToArchVirtue.Add("GreatCharacteristic",        GreatCharacteristic);
        NameToArchVirtue.Add("ImprovedCharacteristics",    ImprovedCharacteristics);
        NameToArchVirtue.Add("Inspirational",              Inspirational);
        NameToArchVirtue.Add("Intuition",                  Intuition);
        NameToArchVirtue.Add("KeenVision",                 KeenVision);
        NameToArchVirtue.Add("Large",                      Large);
        NameToArchVirtue.Add("LatentMagicalAbility",       LatentMagicalAbility);
        NameToArchVirtue.Add("Learn(Ability)fromMistakes", LearnAbilityfromMistakes);  // Note: Learn <SomeAbility> from Mistakes, so we may need more infrastructure...
        NameToArchVirtue.Add("LightTouch",                 LightTouch);
        NameToArchVirtue.Add("LightningReflexes",          LightningReflexes);
        NameToArchVirtue.Add("Long-Winded",                LongWinded);
        NameToArchVirtue.Add("Luck",                       Luck);
        NameToArchVirtue.Add("RapidConvalescence",         RapidConvalescence);
        NameToArchVirtue.Add("PerfectBalance",             PerfectBalance);
        NameToArchVirtue.Add("PiercingGaze",               PiercingGaze);
        NameToArchVirtue.Add("PrivilegedUpbringing",       PrivilegedUpbringing);
        NameToArchVirtue.Add("Protection",                 Protection);
        NameToArchVirtue.Add("PuissantAbility",            PuissantAbility);  // Note: Infrastructure exists to generate variants for this, do we need more?
        NameToArchVirtue.Add("Relic",                      Relic);  // Note: Will need fields to attach a description of the relic, and ultimately, to add a suitable inventory item...
        NameToArchVirtue.Add("ReservesofStrength",         ReservesofStrength);
        NameToArchVirtue.Add("Self-Confident",             SelfConfident);
        NameToArchVirtue.Add("SharpEars",                  SharpEars);
        NameToArchVirtue.Add("SocialContacts",             SocialContacts);
        NameToArchVirtue.Add("Strong-Willed",              StrongWilled);
        NameToArchVirtue.Add("Studentof(Realm)",           StudentofRealm);
        NameToArchVirtue.Add("TemporalInfluence",          TemporalInfluence);
        NameToArchVirtue.Add("Tough",                      Tough);
        NameToArchVirtue.Add("TroupeUpbringing",           TroupeUpbringing);
        NameToArchVirtue.Add("TrueLove(PC)",               TrueLovePC);  // Note: True Love <SomePC_or_powerfulNPC>, so we may need more infrastructure...
        NameToArchVirtue.Add("Unaging",                    Unaging);
        NameToArchVirtue.Add("VenusBlessing",              VenusBlessing);
        NameToArchVirtue.Add("Warrior",                    Warrior);
        NameToArchVirtue.Add("Well-Traveled",              WellTraveled);

        // Social Free
        NameToArchVirtue.Add("Covenfolk",     Covenfolk);
        NameToArchVirtue.Add("Craftsman",     Craftsman);
        NameToArchVirtue.Add("HermeticMagus", HermeticMagus);
        NameToArchVirtue.Add("Merchant",      Merchant);
        NameToArchVirtue.Add("Peasant",       Peasant);
        NameToArchVirtue.Add("Wanderer",      Wanderer);
        #endregion
    }
    #endregion
}
