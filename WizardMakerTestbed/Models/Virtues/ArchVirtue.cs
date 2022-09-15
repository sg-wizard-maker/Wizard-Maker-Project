using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.Virtues.VirtueCommands;

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

    public enum VirtueCost { MAJOR = 3, MINOR = 1, FREE = 0}
    public class ArchVirtue
    {
        public string Name;
        public string Description;
        public VirtueType Type;
        public VirtueCost MajorMinor;
        public ICharacterCommand CharacterCommand { get; private set; }

        // Access ArchVirtue instances with this dictionary.
        //   Dev note:  Do not use this dictionary in the instantiation of the VirtueCommands
        public static Dictionary<string, ArchVirtue> NameToArchVirtue = new Dictionary<string, ArchVirtue>();

        public const string PUISSANT_PREFIX = "Puissant ";
        public const string AFFINITY_PREFIX = "Affinity with ";

        public ArchVirtue(string name, string description, VirtueType type, VirtueCost majorMinor) : this(name, description, type, majorMinor, null) { }

        public ArchVirtue(string name, string description, VirtueType type, VirtueCost majorMinor, ICharacterCommand characterCommand)
        {
            Name = name;
            Type = type;
            Description = description;
            MajorMinor = majorMinor;
            this.CharacterCommand = characterCommand;
        }

        // Helps the front end decide if the Virtue has been implemented in the backend.  This will help while functionality is still incomplete.
        public bool IsImplemented()
        {
            return (CharacterCommand != null);
        }

        // TODO: In future, rather than a hard-coded list,
        // the program will offer the means to declare a new Virtue at runtime.
        // 
        // This will be especially needful, when considering "wildcard" Virtues which describe a list of many virtues, such as:
        //  Way of the (Land)
        static ArchVirtue ()
        {
            // Implement puissant abilities as a dictionary to an arch virtue
            foreach (ArchAbility a in ArchAbility.AllCommonAbilities)
            {
                NameToArchVirtue[PUISSANT_PREFIX + a.Name] = new ArchVirtue(PUISSANT_PREFIX + a.Name, "Puissant in the ability " + a.Name, VirtueType.General, 
                    VirtueCost.MINOR, new PuissantAbilityCommand(a));
                NameToArchVirtue[AFFINITY_PREFIX + a.Name] = new ArchVirtue(AFFINITY_PREFIX + a.Name, "Affinity with the ability " + a.Name, VirtueType.Hermetic, 
                    VirtueCost.MINOR, new AffinityAbilityCommand(a));
            }
            PopulateVirtueDictionary();
        }

        // TODO: Define the keys as constants

        #region Individual ArchVirtue Instances
        private static ArchVirtue TheGift = new ArchVirtue("TheGift", "TheGift", VirtueType.Hermetic, VirtueCost.FREE);
        
        // Hermetic Major
        private static ArchVirtue DiedneMagic = new ArchVirtue("DiedneMagic", "DiedneMagic", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue ElementalMagic = new ArchVirtue("ElementalMagic", "ElementalMagic", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue FlawlessMagic = new ArchVirtue("FlawlessMagic", "FlawlessMagic", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue FlexibleFormulaicMagic = new ArchVirtue("FlexibleFormulaicMagic", "FlexibleFormulaicMagic", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue GentleGift = new ArchVirtue("GentleGift", "GentleGift", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue LifeLinkedSpontaneousMagic = new ArchVirtue("Life-LinkedSpontaneousMagic", "Life-LinkedSpontaneousMagic", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue MajorMagicalFocus = new ArchVirtue("MajorMagicalFocus", "MajorMagicalFocus", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue MercurianMagic = new ArchVirtue("MercurianMagic", "MercurianMagic", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue MythicBlood = new ArchVirtue("MythicBlood", "MythicBlood", VirtueType.Hermetic, VirtueCost.MAJOR);
        private static ArchVirtue SecondaryInsight = new ArchVirtue("SecondaryInsight", "SecondaryInsight", VirtueType.Hermetic, VirtueCost.MAJOR);
        
        // Supernatural Major
        private static ArchVirtue Entrancement = new ArchVirtue("Entrancement", "Entrancement", VirtueType.Supernatural, VirtueCost.MAJOR);
        private static ArchVirtue GreaterImmunity = new ArchVirtue("GreaterImmunity", "GreaterImmunity", VirtueType.Supernatural, VirtueCost.MAJOR);
        private static ArchVirtue GreaterPurifyingTouch = new ArchVirtue("GreaterPurifyingTouch", "GreaterPurifyingTouch", VirtueType.Supernatural, VirtueCost.MAJOR);
        private static ArchVirtue Shapeshifter = new ArchVirtue("Shapeshifter", "Shapeshifter", VirtueType.Supernatural, VirtueCost.MAJOR);
        private static ArchVirtue StrongFaerieBlood = new ArchVirtue("StrongFaerieBlood", "StrongFaerieBlood", VirtueType.Supernatural, VirtueCost.MAJOR);
        
        // Social Status Major
        private static ArchVirtue LandedNoble = new ArchVirtue("LandedNoble", "LandedNoble", VirtueType.Social, VirtueCost.MAJOR);
        private static ArchVirtue MagisterinArtibus = new ArchVirtue("MagisterinArtibus", "MagisterinArtibus", VirtueType.Social, VirtueCost.MAJOR);
        private static ArchVirtue Redcap = new ArchVirtue("Redcap", "Redcap", VirtueType.Social, VirtueCost.MAJOR);
        
        // General Major
        private static ArchVirtue DeathProphecy = new ArchVirtue("DeathProphecy", "DeathProphecy", VirtueType.General, VirtueCost.MAJOR);
        private static ArchVirtue GhostlyWarder = new ArchVirtue("GhostlyWarder", "GhostlyWarder", VirtueType.General, VirtueCost.MAJOR);
        private static ArchVirtue GiantBlood = new ArchVirtue("GiantBlood", "GiantBlood", VirtueType.General, VirtueCost.MAJOR);
        private static ArchVirtue GuardianAngel = new ArchVirtue("GuardianAngel", "GuardianAngel", VirtueType.General, VirtueCost.MAJOR);
        private static ArchVirtue TrueFaith = new ArchVirtue("TrueFaith", "TrueFaith", VirtueType.General, VirtueCost.MAJOR);
        private static ArchVirtue WaysoftheLand = new ArchVirtue("Waysofthe(Land)", "Waysofthe(Land)", VirtueType.General, VirtueCost.MAJOR);
        private static ArchVirtue Wealthy = new ArchVirtue("Wealthy", "Wealthy", VirtueType.General, VirtueCost.MAJOR, new WealthyCommand());
        
        // Hermetic Minor
        private static ArchVirtue AdeptLaboratoryStudent = new ArchVirtue("AdeptLaboratoryStudent", "AdeptLaboratoryStudent", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue AffinitywithArt = new ArchVirtue("AffinitywithArt", "AffinitywithArt", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue CautiousSorcerer = new ArchVirtue("CautiousSorcerer", "CautiousSorcerer", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue CyclicMagicpositive = new ArchVirtue("CyclicMagic(positive)", "CyclicMagic(positive)", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue DeftForm = new ArchVirtue("DeftForm", "DeftForm", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue EnduringMagic = new ArchVirtue("EnduringMagic", "EnduringMagic", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue TheEnigma = new ArchVirtue("TheEnigma", "TheEnigma", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue FaerieMagic = new ArchVirtue("FaerieMagic", "FaerieMagic", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue FastCaster = new ArchVirtue("FastCaster", "FastCaster", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue FreeStudy = new ArchVirtue("FreeStudy", "FreeStudy", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue HarnessedMagic = new ArchVirtue("HarnessedMagic", "HarnessedMagic", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue Heartbeast = new ArchVirtue("Heartbeast", "Heartbeast", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue HermeticPrestige = new ArchVirtue("HermeticPrestige", "HermeticPrestige", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue InoffensivetoAnimals = new ArchVirtue("InoffensivetoAnimals", "InoffensivetoAnimals", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue InventiveGenius = new ArchVirtue("InventiveGenius", "InventiveGenius", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue LifeBoost = new ArchVirtue("LifeBoost", "LifeBoost", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue MinorMagicalFocus = new ArchVirtue("MinorMagicalFocus", "MinorMagicalFocus", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue MagicalMemory = new ArchVirtue("MagicalMemory", "MagicalMemory", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue MasteredSpells = new ArchVirtue("MasteredSpells", "MasteredSpells", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue MethodCaster = new ArchVirtue("MethodCaster", "MethodCaster", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue PersonalVisSource = new ArchVirtue("PersonalVisSource", "PersonalVisSource", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue PuissantArt = new ArchVirtue("PuissantArt", "PuissantArt", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue QuietMagic = new ArchVirtue("QuietMagic", "QuietMagic", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue SideEffect = new ArchVirtue("SideEffect", "SideEffect", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue SkilledParens = new ArchVirtue("SkilledParens", "SkilledParens", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue SpecialCircumstances = new ArchVirtue("SpecialCircumstances", "SpecialCircumstances", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue StudyBonus = new ArchVirtue("StudyBonus", "StudyBonus", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue SubtleMagic = new ArchVirtue("SubtleMagic", "SubtleMagic", VirtueType.Hermetic, VirtueCost.MINOR);
        private static ArchVirtue VerditiusMagic = new ArchVirtue("VerditiusMagic", "VerditiusMagic", VirtueType.Hermetic, VirtueCost.MINOR);
        
        // Supernatural Minor
        private static ArchVirtue AnimalKen = new ArchVirtue("AnimalKen", "AnimalKen", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue Dowsing = new ArchVirtue("Dowsing", "Dowsing", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue EnchantingMusic = new ArchVirtue("EnchantingMusic", "EnchantingMusic", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue LesserImmunity = new ArchVirtue("LesserImmunity", "LesserImmunity", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue LesserPurifyingTouch = new ArchVirtue("LesserPurifyingTouch", "LesserPurifyingTouch", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue MagicSensitivity = new ArchVirtue("MagicSensitivity", "MagicSensitivity", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue Premonitions = new ArchVirtue("Premonitions", "Premonitions", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue SecondSight = new ArchVirtue("SecondSight", "SecondSight", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue SenseHolinessandUnholiness = new ArchVirtue("SenseHolinessandUnholiness", "SenseHolinessandUnholiness", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue Skinchanger = new ArchVirtue("Skinchanger", "Skinchanger", VirtueType.Supernatural, VirtueCost.MINOR);
        private static ArchVirtue WildernessSense = new ArchVirtue("WildernessSense", "WildernessSense", VirtueType.Supernatural, VirtueCost.MINOR);
        
        // Social Minor
        private static ArchVirtue Clerk = new ArchVirtue("Clerk", "Clerk", VirtueType.Social, VirtueCost.MINOR);
        private static ArchVirtue Custos = new ArchVirtue("Custos", "Custos", VirtueType.Social, VirtueCost.MINOR);
        private static ArchVirtue FailedApprentice = new ArchVirtue("FailedApprentice", "FailedApprentice", VirtueType.Social, VirtueCost.MINOR);
        private static ArchVirtue Gentlemanwoman = new ArchVirtue("Gentleman/woman", "Gentleman/woman", VirtueType.Social, VirtueCost.MINOR);
        private static ArchVirtue Knight = new ArchVirtue("Knight", "Knight", VirtueType.Social, VirtueCost.MINOR);
        private static ArchVirtue MendicantFriar = new ArchVirtue("MendicantFriar", "MendicantFriar", VirtueType.Social, VirtueCost.MINOR);
        private static ArchVirtue MercenaryCaptain = new ArchVirtue("MercenaryCaptain", "MercenaryCaptain", VirtueType.Social, VirtueCost.MINOR);
        private static ArchVirtue Priest = new ArchVirtue("Priest", "Priest", VirtueType.Social, VirtueCost.MINOR);
        private static ArchVirtue WiseOne = new ArchVirtue("WiseOne", "WiseOne", VirtueType.Social, VirtueCost.MINOR);
        
        // General Minor
        private static ArchVirtue AffinitywithAbility = new ArchVirtue("AffinitywithAbility", "AffinitywithAbility", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue AptStudent = new ArchVirtue("AptStudent", "AptStudent", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue ArcaneLore = new ArchVirtue("ArcaneLore", "ArcaneLore", VirtueType.General, VirtueCost.MINOR, new ArcaneLoreCommand());
        private static ArchVirtue Berserk = new ArchVirtue("Berserk", "Berserk", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue BookLearner = new ArchVirtue("BookLearner", "BookLearner", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue CautiouswithAbility = new ArchVirtue("CautiouswithAbility", "CautiouswithAbility", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue ClearThinker = new ArchVirtue("ClearThinker", "ClearThinker", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue CommonSense = new ArchVirtue("CommonSense", "CommonSense", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Educated = new ArchVirtue("Educated", "Educated", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue EnduringConstitution = new ArchVirtue("EnduringConstitution", "EnduringConstitution", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue FaerieBlood = new ArchVirtue("FaerieBlood", "FaerieBlood", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Famous = new ArchVirtue("Famous", "Famous", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue FreeExpression = new ArchVirtue("FreeExpression", "FreeExpression", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue GoodTeacher = new ArchVirtue("GoodTeacher", "GoodTeacher", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Gossip = new ArchVirtue("Gossip", "Gossip", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue ImprovedCharacteristics = new ArchVirtue("ImprovedCharacteristics", "ImprovedCharacteristics", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Inspirational = new ArchVirtue("Inspirational", "Inspirational", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Intuition = new ArchVirtue("Intuition", "Intuition", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue KeenVision = new ArchVirtue("KeenVision", "KeenVision", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Large = new ArchVirtue("Large", "Large", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue LatentMagicalAbility = new ArchVirtue("LatentMagicalAbility", "LatentMagicalAbility", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue LearnAbilityfromMistakes = new ArchVirtue("Learn(Ability)fromMistakes", "Learn(Ability)fromMistakes", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue LightTouch = new ArchVirtue("LightTouch", "LightTouch", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue LightningReflexes = new ArchVirtue("LightningReflexes", "LightningReflexes", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue LongWinded = new ArchVirtue("Long-Winded", "Long-Winded", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Luck = new ArchVirtue("Luck", "Luck", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue RapidConvalescence = new ArchVirtue("RapidConvalescence", "RapidConvalescence", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue PerfectBalance = new ArchVirtue("PerfectBalance", "PerfectBalance", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue PiercingGaze = new ArchVirtue("PiercingGaze", "PiercingGaze", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue PrivilegedUpbringing = new ArchVirtue("PrivilegedUpbringing", "PrivilegedUpbringing", VirtueType.General, VirtueCost.MINOR, new PrivilegedUpbringingCommand());
        private static ArchVirtue Protection = new ArchVirtue("Protection", "Protection", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue PuissantAbility = new ArchVirtue("PuissantAbility", "PuissantAbility", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Relic = new ArchVirtue("Relic", "Relic", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue ReservesofStrength = new ArchVirtue("ReservesofStrength", "ReservesofStrength", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue SelfConfident = new ArchVirtue("Self-Confident", "Self-Confident", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue SharpEars = new ArchVirtue("SharpEars", "SharpEars", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue SocialContacts = new ArchVirtue("SocialContacts", "SocialContacts", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue StrongWilled = new ArchVirtue("Strong-Willed", "Strong-Willed", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue StudentofRealm = new ArchVirtue("Studentof(Realm)", "Studentof(Realm)", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue TemporalInfluence = new ArchVirtue("TemporalInfluence", "TemporalInfluence", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Tough = new ArchVirtue("Tough", "Tough", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue TroupeUpbringing = new ArchVirtue("TroupeUpbringing", "TroupeUpbringing", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue TrueLovePC = new ArchVirtue("TrueLove(PC)", "TrueLove(PC)", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Unaging = new ArchVirtue("Unaging", "Unaging", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue VenusBlessing = new ArchVirtue("Venus’Blessing", "Venus’Blessing", VirtueType.General, VirtueCost.MINOR);
        private static ArchVirtue Warrior = new ArchVirtue("Warrior", "Warrior", VirtueType.General, VirtueCost.MINOR, new WarriorCommand());
        private static ArchVirtue WellTraveled = new ArchVirtue("Well-Traveled", "Well-Traveled", VirtueType.General, VirtueCost.MINOR);
        
        // Social Free
        private static ArchVirtue Covenfolk = new ArchVirtue("Covenfolk", "Covenfolk", VirtueType.Social, VirtueCost.FREE);
        private static ArchVirtue Craftsman = new ArchVirtue("Craftsman", "Craftsman", VirtueType.Social, VirtueCost.FREE);
        private static ArchVirtue HermeticMagus = new ArchVirtue("HermeticMagus", "HermeticMagus", VirtueType.Social, VirtueCost.FREE);
        private static ArchVirtue Merchant = new ArchVirtue("Merchant", "Merchant", VirtueType.Social, VirtueCost.FREE);
        private static ArchVirtue Peasant = new ArchVirtue("Peasant", "Peasant", VirtueType.Social, VirtueCost.FREE);
        private static ArchVirtue Wanderer = new ArchVirtue("Wanderer", "Wanderer", VirtueType.Social, VirtueCost.FREE);
        #endregion

        #region Populating the NameToArchVirtue Dictionary
        private static void PopulateVirtueDictionary()
        {
            NameToArchVirtue.Add("TheGift", TheGift);

            // Hermetic Major
            NameToArchVirtue.Add("DiedneMagic", DiedneMagic);
            NameToArchVirtue.Add("ElementalMagic", ElementalMagic);
            NameToArchVirtue.Add("FlawlessMagic", FlawlessMagic);
            NameToArchVirtue.Add("FlexibleFormulaicMagic", FlexibleFormulaicMagic);
            NameToArchVirtue.Add("GentleGift", GentleGift);
            NameToArchVirtue.Add("Life-LinkedSpontaneousMagic", LifeLinkedSpontaneousMagic);
            NameToArchVirtue.Add("MajorMagicalFocus", MajorMagicalFocus);
            NameToArchVirtue.Add("MercurianMagic", MercurianMagic);
            NameToArchVirtue.Add("MythicBlood", MythicBlood);
            NameToArchVirtue.Add("SecondaryInsight", SecondaryInsight);

            // Supernatural Major
            NameToArchVirtue.Add("Entrancement", Entrancement);
            NameToArchVirtue.Add("GreaterImmunity", GreaterImmunity);
            NameToArchVirtue.Add("GreaterPurifyingTouch", GreaterPurifyingTouch);
            NameToArchVirtue.Add("Shapeshifter", Shapeshifter);
            NameToArchVirtue.Add("StrongFaerieBlood", StrongFaerieBlood);

            // Social Status Major
            NameToArchVirtue.Add("LandedNoble", LandedNoble);
            NameToArchVirtue.Add("MagisterinArtibus", MagisterinArtibus);
            NameToArchVirtue.Add("Redcap", Redcap);

            // General Major
            NameToArchVirtue.Add("DeathProphecy", DeathProphecy);
            NameToArchVirtue.Add("GhostlyWarder", GhostlyWarder);
            NameToArchVirtue.Add("GiantBlood", GiantBlood);
            NameToArchVirtue.Add("GuardianAngel", GuardianAngel);
            NameToArchVirtue.Add("TrueFaith", TrueFaith);
            NameToArchVirtue.Add("Waysofthe(Land)", WaysoftheLand);
            NameToArchVirtue.Add("Wealthy", Wealthy);

            // Hermetic Minor
            NameToArchVirtue.Add("AdeptLaboratoryStudent", AdeptLaboratoryStudent);
            NameToArchVirtue.Add("AffinitywithArt", AffinitywithArt);
            NameToArchVirtue.Add("CautiousSorcerer", CautiousSorcerer);
            NameToArchVirtue.Add("CyclicMagic(positive)", CyclicMagicpositive);
            NameToArchVirtue.Add("DeftForm", DeftForm);
            NameToArchVirtue.Add("EnduringMagic", EnduringMagic);
            NameToArchVirtue.Add("TheEnigma", TheEnigma);
            NameToArchVirtue.Add("FaerieMagic", FaerieMagic);
            NameToArchVirtue.Add("FastCaster", FastCaster);
            NameToArchVirtue.Add("FreeStudy", FreeStudy);
            NameToArchVirtue.Add("HarnessedMagic", HarnessedMagic);
            NameToArchVirtue.Add("Heartbeast", Heartbeast);
            NameToArchVirtue.Add("HermeticPrestige", HermeticPrestige);
            NameToArchVirtue.Add("InoffensivetoAnimals", InoffensivetoAnimals);
            NameToArchVirtue.Add("InventiveGenius", InventiveGenius);
            NameToArchVirtue.Add("LifeBoost", LifeBoost);
            NameToArchVirtue.Add("MinorMagicalFocus", MinorMagicalFocus);
            NameToArchVirtue.Add("MagicalMemory", MagicalMemory);
            NameToArchVirtue.Add("MasteredSpells", MasteredSpells);
            NameToArchVirtue.Add("MethodCaster", MethodCaster);
            NameToArchVirtue.Add("PersonalVisSource", PersonalVisSource);
            NameToArchVirtue.Add("PuissantArt", PuissantArt);
            NameToArchVirtue.Add("QuietMagic", QuietMagic);
            NameToArchVirtue.Add("SideEffect", SideEffect);
            NameToArchVirtue.Add("SkilledParens", SkilledParens);
            NameToArchVirtue.Add("SpecialCircumstances", SpecialCircumstances);
            NameToArchVirtue.Add("StudyBonus", StudyBonus);
            NameToArchVirtue.Add("SubtleMagic", SubtleMagic);
            NameToArchVirtue.Add("VerditiusMagic", VerditiusMagic);

            // Supernatural Minor
            NameToArchVirtue.Add("AnimalKen", AnimalKen);
            NameToArchVirtue.Add("Dowsing", Dowsing);
            NameToArchVirtue.Add("EnchantingMusic", EnchantingMusic);
            NameToArchVirtue.Add("LesserImmunity", LesserImmunity);
            NameToArchVirtue.Add("LesserPurifyingTouch", LesserPurifyingTouch);
            NameToArchVirtue.Add("MagicSensitivity", MagicSensitivity);
            NameToArchVirtue.Add("Premonitions", Premonitions);
            NameToArchVirtue.Add("SecondSight", SecondSight);
            NameToArchVirtue.Add("SenseHolinessandUnholiness", SenseHolinessandUnholiness);
            NameToArchVirtue.Add("Skinchanger", Skinchanger);
            NameToArchVirtue.Add("WildernessSense", WildernessSense);

            // Social Minor
            NameToArchVirtue.Add("Clerk", Clerk);
            NameToArchVirtue.Add("Custos", Custos);
            NameToArchVirtue.Add("FailedApprentice", FailedApprentice);
            NameToArchVirtue.Add("Gentleman/woman", Gentlemanwoman);
            NameToArchVirtue.Add("Knight", Knight);
            NameToArchVirtue.Add("MendicantFriar", MendicantFriar);
            NameToArchVirtue.Add("MercenaryCaptain", MercenaryCaptain);
            NameToArchVirtue.Add("Priest", Priest);
            NameToArchVirtue.Add("WiseOne", WiseOne);

            // General Minor
            NameToArchVirtue.Add("AffinitywithAbility", AffinitywithAbility);
            NameToArchVirtue.Add("AptStudent", AptStudent);
            NameToArchVirtue.Add("ArcaneLore", ArcaneLore);
            NameToArchVirtue.Add("Berserk", Berserk);
            NameToArchVirtue.Add("BookLearner", BookLearner);
            NameToArchVirtue.Add("CautiouswithAbility", CautiouswithAbility);
            NameToArchVirtue.Add("ClearThinker", ClearThinker);
            NameToArchVirtue.Add("CommonSense", CommonSense);
            NameToArchVirtue.Add("Educated", Educated);
            NameToArchVirtue.Add("EnduringConstitution", EnduringConstitution);
            NameToArchVirtue.Add("FaerieBlood", FaerieBlood);
            NameToArchVirtue.Add("Famous", Famous);
            NameToArchVirtue.Add("FreeExpression", FreeExpression);
            NameToArchVirtue.Add("GoodTeacher", GoodTeacher);
            NameToArchVirtue.Add("Gossip", Gossip);
            NameToArchVirtue.Add("ImprovedCharacteristics", ImprovedCharacteristics);
            NameToArchVirtue.Add("Inspirational", Inspirational);
            NameToArchVirtue.Add("Intuition", Intuition);
            NameToArchVirtue.Add("KeenVision", KeenVision);
            NameToArchVirtue.Add("Large", Large);
            NameToArchVirtue.Add("LatentMagicalAbility", LatentMagicalAbility);
            NameToArchVirtue.Add("Learn(Ability)fromMistakes", LearnAbilityfromMistakes);
            NameToArchVirtue.Add("LightTouch", LightTouch);
            NameToArchVirtue.Add("LightningReflexes", LightningReflexes);
            NameToArchVirtue.Add("Long-Winded", LongWinded);
            NameToArchVirtue.Add("Luck", Luck);
            NameToArchVirtue.Add("RapidConvalescence", RapidConvalescence);
            NameToArchVirtue.Add("PerfectBalance", PerfectBalance);
            NameToArchVirtue.Add("PiercingGaze", PiercingGaze);
            NameToArchVirtue.Add("PrivilegedUpbringing", PrivilegedUpbringing);
            NameToArchVirtue.Add("Protection", Protection);
            NameToArchVirtue.Add("PuissantAbility", PuissantAbility);
            NameToArchVirtue.Add("Relic", Relic);
            NameToArchVirtue.Add("ReservesofStrength", ReservesofStrength);
            NameToArchVirtue.Add("Self-Confident", SelfConfident);
            NameToArchVirtue.Add("SharpEars", SharpEars);
            NameToArchVirtue.Add("SocialContacts", SocialContacts);
            NameToArchVirtue.Add("Strong-Willed", StrongWilled);
            NameToArchVirtue.Add("Studentof(Realm)", StudentofRealm);
            NameToArchVirtue.Add("TemporalInfluence", TemporalInfluence);
            NameToArchVirtue.Add("Tough", Tough);
            NameToArchVirtue.Add("TroupeUpbringing", TroupeUpbringing);
            NameToArchVirtue.Add("TrueLove(PC)", TrueLovePC);
            NameToArchVirtue.Add("Unaging", Unaging);
            NameToArchVirtue.Add("VenusBlessing", VenusBlessing);
            NameToArchVirtue.Add("Warrior", Warrior);
            NameToArchVirtue.Add("Well-Traveled", WellTraveled);

            // Social Free
            NameToArchVirtue.Add("Covenfolk", Covenfolk);
            NameToArchVirtue.Add("Craftsman", Craftsman);
            NameToArchVirtue.Add("HermeticMagus", HermeticMagus);
            NameToArchVirtue.Add("Merchant", Merchant);
            NameToArchVirtue.Add("Peasant", Peasant);
            NameToArchVirtue.Add("Wanderer", Wanderer);
        }

        #endregion
    }
}
