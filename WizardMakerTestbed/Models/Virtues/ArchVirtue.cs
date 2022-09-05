using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Virtues
{

    public class VirtueType
    {
        public static List<VirtueType> Types = new List<VirtueType>();

        public string Abbreviation { get; private set; }
        public string Name { get; private set; }


        // This is only public so that it can be available for serialization
        public VirtueType(string abbrev, string name)
        {
            this.Abbreviation = abbrev;
            this.Name = name;
        }

        public static VirtueType Hermetic = new VirtueType("Hermetic", "Hermetic");
        public static VirtueType Supernatural = new VirtueType("Supernatural", "Supernatural");
        public static VirtueType Social = new VirtueType("Social", "Social");
        public static VirtueType General = new VirtueType("General", "General");

        static VirtueType()
        {
            Types.Add(Hermetic);
            Types.Add(General);
            Types.Add(Social);
            Types.Add(Supernatural);
        }
    }

    public enum VirtueMajorMinor { MAJOR = 3, MINOR = 1, FREE = 0}
    public class ArchVirtue
    {
        public string Name;
        public string Description;
        public VirtueType Type;
        public VirtueMajorMinor MajorMinor;

        public ArchVirtue(string name, string description, VirtueType type, VirtueMajorMinor majorMinor)
        {
            Name = name;
            Type = type;
            Description = description;
            MajorMinor = majorMinor;
        }

        // TODO: In future, rather than a hard-coded list,
        // the program will offer the means to declare a new Virtue at runtime.
        // 
        // This will be especially needful, when considering "wildcard" Virtues which describe a list of many virtues, such as:
        //  Way of the (Land)

        public static ArchVirtue TheGift = new ArchVirtue("TheGift", "TheGift", VirtueType.Hermetic, VirtueMajorMinor.FREE);
        
        // Hermetic Major
        public static ArchVirtue DiedneMagic = new ArchVirtue("DiedneMagic", "DiedneMagic", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue ElementalMagic = new ArchVirtue("ElementalMagic", "ElementalMagic", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue FlawlessMagic = new ArchVirtue("FlawlessMagic", "FlawlessMagic", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue FlexibleFormulaicMagic = new ArchVirtue("FlexibleFormulaicMagic", "FlexibleFormulaicMagic", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue GentleGift = new ArchVirtue("GentleGift", "GentleGift", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue LifeLinkedSpontaneousMagic = new ArchVirtue("Life-LinkedSpontaneousMagic", "Life-LinkedSpontaneousMagic", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue MajorMagicalFocus = new ArchVirtue("MajorMagicalFocus", "MajorMagicalFocus", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue MercurianMagic = new ArchVirtue("MercurianMagic", "MercurianMagic", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue MythicBlood = new ArchVirtue("MythicBlood", "MythicBlood", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        public static ArchVirtue SecondaryInsight = new ArchVirtue("SecondaryInsight", "SecondaryInsight", VirtueType.Hermetic, VirtueMajorMinor.MAJOR);
        
        // Supernatural Major
        public static ArchVirtue Entrancement = new ArchVirtue("Entrancement", "Entrancement", VirtueType.Supernatural, VirtueMajorMinor.MAJOR);
        public static ArchVirtue GreaterImmunity = new ArchVirtue("GreaterImmunity", "GreaterImmunity", VirtueType.Supernatural, VirtueMajorMinor.MAJOR);
        public static ArchVirtue GreaterPurifyingTouch = new ArchVirtue("GreaterPurifyingTouch", "GreaterPurifyingTouch", VirtueType.Supernatural, VirtueMajorMinor.MAJOR);
        public static ArchVirtue Shapeshifter = new ArchVirtue("Shapeshifter", "Shapeshifter", VirtueType.Supernatural, VirtueMajorMinor.MAJOR);
        public static ArchVirtue StrongFaerieBlood = new ArchVirtue("StrongFaerieBlood", "StrongFaerieBlood", VirtueType.Supernatural, VirtueMajorMinor.MAJOR);
        
        // Social Status Major
        public static ArchVirtue LandedNoble = new ArchVirtue("LandedNoble", "LandedNoble", VirtueType.Social, VirtueMajorMinor.MAJOR);
        public static ArchVirtue MagisterinArtibus = new ArchVirtue("MagisterinArtibus", "MagisterinArtibus", VirtueType.Social, VirtueMajorMinor.MAJOR);
        public static ArchVirtue Redcap = new ArchVirtue("Redcap", "Redcap", VirtueType.Social, VirtueMajorMinor.MAJOR);
        
        // General Major
        public static ArchVirtue DeathProphecy = new ArchVirtue("DeathProphecy", "DeathProphecy", VirtueType.General, VirtueMajorMinor.MAJOR);
        public static ArchVirtue GhostlyWarder = new ArchVirtue("GhostlyWarder", "GhostlyWarder", VirtueType.General, VirtueMajorMinor.MAJOR);
        public static ArchVirtue GiantBlood = new ArchVirtue("GiantBlood", "GiantBlood", VirtueType.General, VirtueMajorMinor.MAJOR);
        public static ArchVirtue GuardianAngel = new ArchVirtue("GuardianAngel", "GuardianAngel", VirtueType.General, VirtueMajorMinor.MAJOR);
        public static ArchVirtue TrueFaith = new ArchVirtue("TrueFaith", "TrueFaith", VirtueType.General, VirtueMajorMinor.MAJOR);
        public static ArchVirtue WaysoftheLand = new ArchVirtue("Waysofthe(Land)", "Waysofthe(Land)", VirtueType.General, VirtueMajorMinor.MAJOR);
        public static ArchVirtue Wealthy = new ArchVirtue("Wealthy", "Wealthy", VirtueType.General, VirtueMajorMinor.MAJOR);
        
        // Hermetic Minor
        public static ArchVirtue AdeptLaboratoryStudent = new ArchVirtue("AdeptLaboratoryStudent", "AdeptLaboratoryStudent", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue AffinitywithArt = new ArchVirtue("AffinitywithArt", "AffinitywithArt", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue CautiousSorcerer = new ArchVirtue("CautiousSorcerer", "CautiousSorcerer", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue CyclicMagicpositive = new ArchVirtue("CyclicMagic(positive)", "CyclicMagic(positive)", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue DeftForm = new ArchVirtue("DeftForm", "DeftForm", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue EnduringMagic = new ArchVirtue("EnduringMagic", "EnduringMagic", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue TheEnigma = new ArchVirtue("TheEnigma", "TheEnigma", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue FaerieMagic = new ArchVirtue("FaerieMagic", "FaerieMagic", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue FastCaster = new ArchVirtue("FastCaster", "FastCaster", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue FreeStudy = new ArchVirtue("FreeStudy", "FreeStudy", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue HarnessedMagic = new ArchVirtue("HarnessedMagic", "HarnessedMagic", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue Heartbeast = new ArchVirtue("Heartbeast", "Heartbeast", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue HermeticPrestige = new ArchVirtue("HermeticPrestige", "HermeticPrestige", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue InoffensivetoAnimals = new ArchVirtue("InoffensivetoAnimals", "InoffensivetoAnimals", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue InventiveGenius = new ArchVirtue("InventiveGenius", "InventiveGenius", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue LifeBoost = new ArchVirtue("LifeBoost", "LifeBoost", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue MinorMagicalFocus = new ArchVirtue("MinorMagicalFocus", "MinorMagicalFocus", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue MagicalMemory = new ArchVirtue("MagicalMemory", "MagicalMemory", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue MasteredSpells = new ArchVirtue("MasteredSpells", "MasteredSpells", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue MethodCaster = new ArchVirtue("MethodCaster", "MethodCaster", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue PersonalVisSource = new ArchVirtue("PersonalVisSource", "PersonalVisSource", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue PuissantArt = new ArchVirtue("PuissantArt", "PuissantArt", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue QuietMagic = new ArchVirtue("QuietMagic", "QuietMagic", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue SideEffect = new ArchVirtue("SideEffect", "SideEffect", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue SkilledParens = new ArchVirtue("SkilledParens", "SkilledParens", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue SpecialCircumstances = new ArchVirtue("SpecialCircumstances", "SpecialCircumstances", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue StudyBonus = new ArchVirtue("StudyBonus", "StudyBonus", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue SubtleMagic = new ArchVirtue("SubtleMagic", "SubtleMagic", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        public static ArchVirtue VerditiusMagic = new ArchVirtue("VerditiusMagic", "VerditiusMagic", VirtueType.Hermetic, VirtueMajorMinor.MINOR);
        
        // Supernatural Minor
        public static ArchVirtue AnimalKen = new ArchVirtue("AnimalKen", "AnimalKen", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue Dowsing = new ArchVirtue("Dowsing", "Dowsing", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue EnchantingMusic = new ArchVirtue("EnchantingMusic", "EnchantingMusic", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue LesserImmunity = new ArchVirtue("LesserImmunity", "LesserImmunity", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue LesserPurifyingTouch = new ArchVirtue("LesserPurifyingTouch", "LesserPurifyingTouch", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue MagicSensitivity = new ArchVirtue("MagicSensitivity", "MagicSensitivity", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue Premonitions = new ArchVirtue("Premonitions", "Premonitions", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue SecondSight = new ArchVirtue("SecondSight", "SecondSight", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue SenseHolinessandUnholiness = new ArchVirtue("SenseHolinessandUnholiness", "SenseHolinessandUnholiness", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue Skinchanger = new ArchVirtue("Skinchanger", "Skinchanger", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        public static ArchVirtue WildernessSense = new ArchVirtue("WildernessSense", "WildernessSense", VirtueType.Supernatural, VirtueMajorMinor.MINOR);
        
        // Social Minor
        public static ArchVirtue Clerk = new ArchVirtue("Clerk", "Clerk", VirtueType.Social, VirtueMajorMinor.MINOR);
        public static ArchVirtue Custos = new ArchVirtue("Custos", "Custos", VirtueType.Social, VirtueMajorMinor.MINOR);
        public static ArchVirtue FailedApprentice = new ArchVirtue("FailedApprentice", "FailedApprentice", VirtueType.Social, VirtueMajorMinor.MINOR);
        public static ArchVirtue Gentlemanwoman = new ArchVirtue("Gentleman/woman", "Gentleman/woman", VirtueType.Social, VirtueMajorMinor.MINOR);
        public static ArchVirtue Knight = new ArchVirtue("Knight", "Knight", VirtueType.Social, VirtueMajorMinor.MINOR);
        public static ArchVirtue MendicantFriar = new ArchVirtue("MendicantFriar", "MendicantFriar", VirtueType.Social, VirtueMajorMinor.MINOR);
        public static ArchVirtue MercenaryCaptain = new ArchVirtue("MercenaryCaptain", "MercenaryCaptain", VirtueType.Social, VirtueMajorMinor.MINOR);
        public static ArchVirtue Priest = new ArchVirtue("Priest", "Priest", VirtueType.Social, VirtueMajorMinor.MINOR);
        public static ArchVirtue WiseOne = new ArchVirtue("WiseOne", "WiseOne", VirtueType.Social, VirtueMajorMinor.MINOR);
        
        // General Minor
        public static ArchVirtue AffinitywithAbility = new ArchVirtue("AffinitywithAbility", "AffinitywithAbility", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue AptStudent = new ArchVirtue("AptStudent", "AptStudent", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue ArcaneLore = new ArchVirtue("ArcaneLore", "ArcaneLore", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Berserk = new ArchVirtue("Berserk", "Berserk", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue BookLearner = new ArchVirtue("BookLearner", "BookLearner", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue CautiouswithAbility = new ArchVirtue("CautiouswithAbility", "CautiouswithAbility", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue ClearThinker = new ArchVirtue("ClearThinker", "ClearThinker", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue CommonSense = new ArchVirtue("CommonSense", "CommonSense", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Educated = new ArchVirtue("Educated", "Educated", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue EnduringConstitution = new ArchVirtue("EnduringConstitution", "EnduringConstitution", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue FaerieBlood = new ArchVirtue("FaerieBlood", "FaerieBlood", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Famous = new ArchVirtue("Famous", "Famous", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue FreeExpression = new ArchVirtue("FreeExpression", "FreeExpression", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue GoodTeacher = new ArchVirtue("GoodTeacher", "GoodTeacher", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Gossip = new ArchVirtue("Gossip", "Gossip", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue ImprovedCharacteristics = new ArchVirtue("ImprovedCharacteristics", "ImprovedCharacteristics", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Inspirational = new ArchVirtue("Inspirational", "Inspirational", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Intuition = new ArchVirtue("Intuition", "Intuition", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue KeenVision = new ArchVirtue("KeenVision", "KeenVision", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Large = new ArchVirtue("Large", "Large", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue LatentMagicalAbility = new ArchVirtue("LatentMagicalAbility", "LatentMagicalAbility", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue LearnAbilityfromMistakes = new ArchVirtue("Learn(Ability)fromMistakes", "Learn(Ability)fromMistakes", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue LightTouch = new ArchVirtue("LightTouch", "LightTouch", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue LightningReflexes = new ArchVirtue("LightningReflexes", "LightningReflexes", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue LongWinded = new ArchVirtue("Long-Winded", "Long-Winded", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Luck = new ArchVirtue("Luck", "Luck", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue RapidConvalescence = new ArchVirtue("RapidConvalescence", "RapidConvalescence", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue PerfectBalance = new ArchVirtue("PerfectBalance", "PerfectBalance", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue PiercingGaze = new ArchVirtue("PiercingGaze", "PiercingGaze", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue PrivilegedUpbringing = new ArchVirtue("PrivilegedUpbringing", "PrivilegedUpbringing", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Protection = new ArchVirtue("Protection", "Protection", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue PuissantAbility = new ArchVirtue("PuissantAbility", "PuissantAbility", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Relic = new ArchVirtue("Relic", "Relic", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue ReservesofStrength = new ArchVirtue("ReservesofStrength", "ReservesofStrength", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue SelfConfident = new ArchVirtue("Self-Confident", "Self-Confident", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue SharpEars = new ArchVirtue("SharpEars", "SharpEars", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue SocialContacts = new ArchVirtue("SocialContacts", "SocialContacts", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue StrongWilled = new ArchVirtue("Strong-Willed", "Strong-Willed", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue StudentofRealm = new ArchVirtue("Studentof(Realm)", "Studentof(Realm)", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue TemporalInfluence = new ArchVirtue("TemporalInfluence", "TemporalInfluence", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Tough = new ArchVirtue("Tough", "Tough", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue TroupeUpbringing = new ArchVirtue("TroupeUpbringing", "TroupeUpbringing", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue TrueLovePC = new ArchVirtue("TrueLove(PC)", "TrueLove(PC)", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Unaging = new ArchVirtue("Unaging", "Unaging", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue VenusBlessing = new ArchVirtue("Venus’Blessing", "Venus’Blessing", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue Warrior = new ArchVirtue("Warrior", "Warrior", VirtueType.General, VirtueMajorMinor.MINOR);
        public static ArchVirtue WellTraveled = new ArchVirtue("Well-Traveled", "Well-Traveled", VirtueType.General, VirtueMajorMinor.MINOR);
        
        // Social Free
        public static ArchVirtue Covenfolk = new ArchVirtue("Covenfolk", "Covenfolk", VirtueType.Social, VirtueMajorMinor.FREE);
        public static ArchVirtue Craftsman = new ArchVirtue("Craftsman", "Craftsman", VirtueType.Social, VirtueMajorMinor.FREE);
        public static ArchVirtue HermeticMagus = new ArchVirtue("HermeticMagus", "HermeticMagus", VirtueType.Social, VirtueMajorMinor.FREE);
        public static ArchVirtue Merchant = new ArchVirtue("Merchant", "Merchant", VirtueType.Social, VirtueMajorMinor.FREE);
        public static ArchVirtue Peasant = new ArchVirtue("Peasant", "Peasant", VirtueType.Social, VirtueMajorMinor.FREE);
        public static ArchVirtue Wanderer = new ArchVirtue("Wanderer", "Wanderer", VirtueType.Social, VirtueMajorMinor.FREE);
    }
}
