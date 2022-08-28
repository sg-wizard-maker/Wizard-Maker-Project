using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
//using System.Text.Json.Serialization;  // JsonConverterAttribute
using System.Xml.Serialization;

namespace WizardMakerPrototype.Models
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
        public static List<String> Categories = new List<string>();

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

        static AbilityCategory ()
        {
            Categories.Add( Martial      );
            Categories.Add( Physical     );
            Categories.Add( Social       );
            Categories.Add( Languages    );
            Categories.Add( Crafts       );
            Categories.Add( Professions  );
            Categories.Add( Academic     );
            Categories.Add( Supernatural );
            Categories.Add( Arcane       );
            Categories.Add( RealmLores   );
            Categories.Add( AreaLores    );
            Categories.Add( OrgLores     );
        }
    }

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
    /// </summary>
    public class AbilityType
    {
        public static List<AbilityType> Types = new List<AbilityType>();

        public string Abbreviation { get; private set; }
        public string Name         { get; private set; }
        // TODO:
        // Need some additional properties defining the relations to
        // requirements/associations with certain Virtues/Flaws, age categories, etc...

        // This is only public so that it can be available for serialization
        public AbilityType(string abbrev, string name)
        {
            this.Abbreviation = abbrev;
            this.Name = name;
        }

        public static AbilityType GenChild     = new AbilityType("[Gc]",   "General (child)" );
        public static AbilityType General      = new AbilityType("[G]",    "General"         );
        public static AbilityType Martial      = new AbilityType("[M]",    "Martial"         );
        public static AbilityType Academic     = new AbilityType("[Acad]", "Academic"        );
        public static AbilityType Arcane       = new AbilityType("[Arc]",  "Arcane"          );
        public static AbilityType Supernatural = new AbilityType("[Sup]",  "Supernatural"    );
        public static AbilityType Secret       = new AbilityType("[Sec]",  "Secret"          );

        static AbilityType()
        {
            Types.Add( GenChild     );
            Types.Add( General      );
            Types.Add( Martial      );
            Types.Add( Academic     );
            Types.Add( Arcane       );
            Types.Add( Supernatural );
        }
    }

    public class ArchAbility
    {
        public string       Category              { get; private set; }
        public AbilityType  Type                  { get; private set; }
        public string       Name                  { get; private set; }
        public List<string> CommonSpecializations { get; private set; } = new List<string>();
        public bool         CannotUseUnskilled    { get; private set; } = false;
        public bool         IsAccelerated         { get; private set; } = false;
        public decimal      BaseXpCost            { get; private set; }

        public ArchAbility ( string category, AbilityType type, string name, List<string> specializations, 
            bool cannotUseUnskilled = false, bool isAccelerated = false )
        {
            this.Category = category;
            this.Type = type;
            this.Name = name;
            this.CommonSpecializations = specializations ?? new List<string>();
            this.BaseXpCost            = isAccelerated ? 1m : 5m;
            this.CannotUseUnskilled    = cannotUseUnskilled;
            this.IsAccelerated         = isAccelerated;
        }

        public static List<ArchAbility> AllCommonAbilities = new List<ArchAbility>();

        private const bool  NO_UNSKILLED = true;
        private const bool YES_UNSKILLED = false;

        #region Static data for Common Specializations
        private static List<string> emptyListOfSpecialties = new List<string>();

        private static List<string> brawlSpecializations        = new List<string>() { "Dodging", "Punches", "Kicks", "Grapples", "Knives", "Bludgeon" };
        private static List<string> singleWeaponSpecializations = new List<string>() { "Axe/Hatchet", "Club/Mace", "Mace and Chain", "Short Spear", "Short Sword", "Long Sword", "Shields" };
        private static List<string> greatWeaponSpecializations  = new List<string>() { "Cudgel", "Farm implement", "Flail", "Pole Arm", "Pole Axe", "Long Spear", "Great Sword", "Staff", "Warhammer" };
        private static List<string> bowsSpecializations         = new List<string>() { "Short bow", "Long bow", "Crossbow" };
        private static List<string> thrownSpecializations       = new List<string>() { "Throwing axe", "Javelin", "Thrown knife", "Sling", "Thrown stone" };

        // ...
        #endregion

        #region Setup static data for Common Abilities
        // TODO:
        // In future, rather than a hard-coded list,
        // the program will offer the means to declare a new Ability at runtime.
        // 
        // This will be especially needful, when considering "wildcard" Abilities which describe a list of many Abilities, such as:
        //     (Language, Dead Language, Craft, Profession, Area Lore, Organization Lore)
        // 
        // This will require that this data
        // is loaded from JSON files
        //     (one for CommonAbilities, at least one for additional / user-defined)
        // upon startup,
        // and saved to JSON files
        //     (at least for additional / user-defined)
        // upon (when XXX is saved, possibly also at shutdown)

        // Category: Martial
        public static ArchAbility Brawl        = new ArchAbility(AbilityCategory.Martial, AbilityType.GenChild, "Brawl",         brawlSpecializations );
        public static ArchAbility SingleWeapon = new ArchAbility(AbilityCategory.Martial, AbilityType.Martial,  "Single Weapon", singleWeaponSpecializations );
        public static ArchAbility GreatWeapon  = new ArchAbility(AbilityCategory.Martial, AbilityType.Martial,  "Great Weapon",  greatWeaponSpecializations  );
        public static ArchAbility Bows         = new ArchAbility(AbilityCategory.Martial, AbilityType.Martial,  "Bows",          bowsSpecializations   );
        public static ArchAbility Thrown       = new ArchAbility(AbilityCategory.Martial, AbilityType.Martial,  "Thrown",        thrownSpecializations );

        // Category: Physical
        public static ArchAbility AnimalHandling = new ArchAbility(AbilityCategory.Physical, AbilityType.General,  "Animal Handling", emptyListOfSpecialties );
        public static ArchAbility Athletics      = new ArchAbility(AbilityCategory.Physical, AbilityType.GenChild, "Athletics",       emptyListOfSpecialties );
        public static ArchAbility Awareness      = new ArchAbility(AbilityCategory.Physical, AbilityType.GenChild, "Awareness",       emptyListOfSpecialties );
        public static ArchAbility Hunt           = new ArchAbility(AbilityCategory.Physical, AbilityType.General,  "Hunt",            emptyListOfSpecialties );
        public static ArchAbility Legerdemain    = new ArchAbility(AbilityCategory.Physical, AbilityType.General,  "Legerdemain",     emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Ride           = new ArchAbility(AbilityCategory.Physical, AbilityType.General,  "Ride",            emptyListOfSpecialties );
        public static ArchAbility Stealth        = new ArchAbility(AbilityCategory.Physical, AbilityType.GenChild, "Stealth",         emptyListOfSpecialties );
        public static ArchAbility Survival       = new ArchAbility(AbilityCategory.Physical, AbilityType.GenChild, "Survival",        emptyListOfSpecialties );
        public static ArchAbility Swim           = new ArchAbility(AbilityCategory.Physical, AbilityType.GenChild, "Swim",            emptyListOfSpecialties );

        // Category: Social
        public static ArchAbility Bargain        = new ArchAbility(AbilityCategory.Social, AbilityType.General,  "Bargain",    emptyListOfSpecialties );
        public static ArchAbility Carouse        = new ArchAbility(AbilityCategory.Social, AbilityType.General,  "Carouse",    emptyListOfSpecialties );
        public static ArchAbility Charm          = new ArchAbility(AbilityCategory.Social, AbilityType.GenChild, "Charm",      emptyListOfSpecialties );
        public static ArchAbility Etiquette      = new ArchAbility(AbilityCategory.Social, AbilityType.General,  "Etiquette",  emptyListOfSpecialties );
        public static ArchAbility FolkKen        = new ArchAbility(AbilityCategory.Social, AbilityType.GenChild, "Folk Ken",   emptyListOfSpecialties );
        public static ArchAbility Guile          = new ArchAbility(AbilityCategory.Social, AbilityType.GenChild, "Guile",      emptyListOfSpecialties );
        public static ArchAbility Intrigue       = new ArchAbility(AbilityCategory.Social, AbilityType.General,  "Intrigue",   emptyListOfSpecialties );
        public static ArchAbility Music          = new ArchAbility(AbilityCategory.Social, AbilityType.General,  "Music",      emptyListOfSpecialties );
        public static ArchAbility Leadership     = new ArchAbility(AbilityCategory.Social, AbilityType.General,  "Leadership", emptyListOfSpecialties );
        public static ArchAbility Teaching       = new ArchAbility(AbilityCategory.Social, AbilityType.General,  "Teaching",   emptyListOfSpecialties );

        // Category: Languages - Living Languages (General)
        public static ArchAbility LangEnglish    = new ArchAbility(AbilityCategory.Languages, AbilityType.General, "Lang: English",     emptyListOfSpecialties );
        public static ArchAbility LangHighGerman = new ArchAbility(AbilityCategory.Languages, AbilityType.General, "Lang: High German", emptyListOfSpecialties );
        public static ArchAbility LangItalian    = new ArchAbility(AbilityCategory.Languages, AbilityType.General, "Lang: Italian",     emptyListOfSpecialties );
        public static ArchAbility LangKoineGreek = new ArchAbility(AbilityCategory.Languages, AbilityType.General, "Lang: Koine Greek", emptyListOfSpecialties );
        public static ArchAbility LangArabic     = new ArchAbility(AbilityCategory.Languages, AbilityType.General, "Lang: Arabic",      emptyListOfSpecialties );
        // ...

        // Category: Languages - Dead Languages (Academic)
        public static ArchAbility LangLatin        = new ArchAbility(AbilityCategory.Languages, AbilityType.Academic, "Dead: Latin",         emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility LangAncientGreek = new ArchAbility(AbilityCategory.Languages, AbilityType.Academic, "Dead: Ancient Greek", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility LangHebrew       = new ArchAbility(AbilityCategory.Languages, AbilityType.Academic, "Dead: Hebrew",        emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility LangGothic       = new ArchAbility(AbilityCategory.Languages, AbilityType.Academic, "Dead: Gothic",        emptyListOfSpecialties, NO_UNSKILLED );
        // ...

        // Category: Crafts
        public static ArchAbility CraftTanner        = new ArchAbility(AbilityCategory.Crafts, AbilityType.General,  "Craft: Tanner",        emptyListOfSpecialties );
        public static ArchAbility CraftLeatherworker = new ArchAbility(AbilityCategory.Crafts, AbilityType.General,  "Craft: Leatherworker", emptyListOfSpecialties );
        public static ArchAbility CraftCarpenter     = new ArchAbility(AbilityCategory.Crafts, AbilityType.General,  "Craft: Carpenter",     emptyListOfSpecialties );
        public static ArchAbility CraftWoodcarver    = new ArchAbility(AbilityCategory.Crafts, AbilityType.General,  "Craft: Woodcarver",    emptyListOfSpecialties );
        public static ArchAbility CraftBlacksmith    = new ArchAbility(AbilityCategory.Crafts, AbilityType.General,  "Craft: Blacksmith",    emptyListOfSpecialties );
        public static ArchAbility CraftJeweler       = new ArchAbility(AbilityCategory.Crafts, AbilityType.General,  "Craft: Jeweler",       emptyListOfSpecialties );
        public static ArchAbility CraftWhitesmith    = new ArchAbility(AbilityCategory.Crafts, AbilityType.General,  "Craft: Whitesmith",    emptyListOfSpecialties );
        // ...

        // Category: Professions
        public static ArchAbility ProfScribe      = new ArchAbility(AbilityCategory.Professions, AbilityType.General,  "Prof: Scribe",      emptyListOfSpecialties );
        public static ArchAbility ProfApothecary  = new ArchAbility(AbilityCategory.Professions, AbilityType.General,  "Prof: Apothecary",  emptyListOfSpecialties );
        // ...
        public static ArchAbility ProfJongleur    = new ArchAbility(AbilityCategory.Professions, AbilityType.General,  "Prof: Jongleur",    emptyListOfSpecialties );
        public static ArchAbility ProfReeve       = new ArchAbility(AbilityCategory.Professions, AbilityType.General,  "Prof: Reeve",       emptyListOfSpecialties );
        public static ArchAbility ProfSailor      = new ArchAbility(AbilityCategory.Professions, AbilityType.General,  "Prof: Sailor",      emptyListOfSpecialties );
        public static ArchAbility ProfSteward     = new ArchAbility(AbilityCategory.Professions, AbilityType.General,  "Prof: Steward",     emptyListOfSpecialties );
        public static ArchAbility ProfTeamster    = new ArchAbility(AbilityCategory.Professions, AbilityType.General,  "Prof: Teamster",    emptyListOfSpecialties );
        public static ArchAbility ProfWasherwoman = new ArchAbility(AbilityCategory.Professions, AbilityType.General,  "Prof: Washerwoman", emptyListOfSpecialties );
        // ...

        // Category: Academic
        public static ArchAbility ArtesLiberales    = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Artes Liberales", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility ArtOfMemory       = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Art of Memory",   emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Chirurgy          = new ArchAbility(AbilityCategory.Academic, AbilityType.General,   "Chirurgy",        emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Medicine          = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Medicine",        emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Philosophiae      = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Philosophiae",    emptyListOfSpecialties, NO_UNSKILLED );
        // ...
        public static ArchAbility LawCodeOfHermes   = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Law: Code of Hermes", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility LawCivilAndCanon  = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Law: Civil & Canon",  emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility LawIslamic        = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Law: Islamic",        emptyListOfSpecialties, NO_UNSKILLED );
        // ...
        public static ArchAbility TheologyChristian = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Theology: Christian", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility TheologyJudaic    = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Theology: Judaic",    emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility TheologyIslamic   = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Theology: Islamic",   emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility TheologyPagan     = new ArchAbility(AbilityCategory.Academic, AbilityType.Academic,  "Theology: Pagan",     emptyListOfSpecialties, NO_UNSKILLED );
        // ...

        // Category: Supernatural
        public static ArchAbility AnimalKen        = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Animal Ken",        emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Dowsing          = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Dowsing",           emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility EnchantingMusic  = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Enchanting Music",  emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Entrancement     = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Entrancement",      emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility MagicSensitivity = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Magic Sensitivity", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Premonitions     = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Premonitions",      emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility SecondSight      = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Second Sight",      emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility SenseHolyUnholy  = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Sense Holy/Unholy", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Shapeshifter     = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Shapeshifter",      emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility WildernessSense  = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Wilderness Sense",  emptyListOfSpecialties, NO_UNSKILLED );
        // ...
        public static ArchAbility TheEnigma     = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "The Enigma",   emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility HeartBeast    = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "HeartBeast",   emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility FaerieMagic   = new ArchAbility(AbilityCategory.Supernatural, AbilityType.Supernatural,  "Faerie Magic", emptyListOfSpecialties, NO_UNSKILLED );

        // Category: Arcane
        public static ArchAbility HermeticMagicTheory  = new ArchAbility(AbilityCategory.Arcane, AbilityType.Arcane,  "Hermetic Magic Theory",   emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility FolkWitchMagicTheory = new ArchAbility(AbilityCategory.Arcane, AbilityType.Arcane,  "Folk Witch Magic Theory", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility ParmaMagica          = new ArchAbility(AbilityCategory.Arcane, AbilityType.Arcane,  "Parma Magica",            emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility Certamen             = new ArchAbility(AbilityCategory.Arcane, AbilityType.Arcane,  "Certamen",                emptyListOfSpecialties );
        public static ArchAbility Concentration        = new ArchAbility(AbilityCategory.Arcane, AbilityType.General, "Concentration",           emptyListOfSpecialties );
        public static ArchAbility Finesse              = new ArchAbility(AbilityCategory.Arcane, AbilityType.Arcane,  "Finesse",                 emptyListOfSpecialties );
        public static ArchAbility Penetration          = new ArchAbility(AbilityCategory.Arcane, AbilityType.Arcane,  "Penetration",             emptyListOfSpecialties );
        public static ArchAbility Recuperation         = new ArchAbility(AbilityCategory.Arcane, AbilityType.Arcane,  "Recuperation",            emptyListOfSpecialties );
        public static ArchAbility Withstanding         = new ArchAbility(AbilityCategory.Arcane, AbilityType.Arcane,  "Withstanding",            emptyListOfSpecialties );

        // Category: Supernatural Realm Lores
        public static ArchAbility MagicLore       = new ArchAbility(AbilityCategory.RealmLores, AbilityType.Arcane,  "Magic Lore",    emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility FaerieLore      = new ArchAbility(AbilityCategory.RealmLores, AbilityType.Arcane,  "Faerie Lore",   emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility DominionLore    = new ArchAbility(AbilityCategory.RealmLores, AbilityType.Arcane,  "Dominion Lore", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility InfernalLore    = new ArchAbility(AbilityCategory.RealmLores, AbilityType.Arcane,  "Infernal Lore", emptyListOfSpecialties, NO_UNSKILLED );

        // Category: Area Lores
        public static ArchAbility AreaLoreVeryBasic     = new ArchAbility(AbilityCategory.AreaLores, AbilityType.General, "Area Lore: VeryBasic",     emptyListOfSpecialties );
        public static ArchAbility AreaLoreSomeCountry   = new ArchAbility(AbilityCategory.AreaLores, AbilityType.General, "Area Lore: SomeCountry",   emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility AreaLoreSomeTribunal  = new ArchAbility(AbilityCategory.AreaLores, AbilityType.General, "Area Lore: SomeTribunal",  emptyListOfSpecialties, NO_UNSKILLED );
        // ...
        public static ArchAbility AreaLoreSecretMagical = new ArchAbility(AbilityCategory.AreaLores, AbilityType.Secret,  "Area Lore: SecretMagical", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility AreaLoreSomeCovenant  = new ArchAbility(AbilityCategory.AreaLores, AbilityType.Arcane,  "Area Lore: SomeCovenant",  emptyListOfSpecialties, NO_UNSKILLED );
        // ...

        // Category: Organization Lores
        public static ArchAbility OrgLoreChurch          = new ArchAbility(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: The Church",      emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility OrgLoreOrderOfHermes   = new ArchAbility(AbilityCategory.OrgLores, AbilityType.Arcane,  "Org Lore: Order of Hermes", emptyListOfSpecialties, NO_UNSKILLED );
        // ...
        public static ArchAbility OrgLoreVeryBasic       = new ArchAbility(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: VeryBasic",       emptyListOfSpecialties );
        public static ArchAbility OrgLoreSomeKnightOrder = new ArchAbility(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: SomeKnightOrder", emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility OrgLoreSomeNobleCourt  = new ArchAbility(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: SomeNobleCourt",  emptyListOfSpecialties, NO_UNSKILLED );
        public static ArchAbility OrgLoreSomeCraftGuild  = new ArchAbility(AbilityCategory.OrgLores, AbilityType.General, "Org Lore: SomeCraftGuild",  emptyListOfSpecialties, NO_UNSKILLED );
        // ...
        #endregion

        static ArchAbility ()
        {
            AllCommonAbilities.AddRange( new List<ArchAbility>() { Brawl, SingleWeapon, GreatWeapon, Bows, Thrown } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { AnimalHandling, Athletics, Awareness, Hunt, Legerdemain, Ride, Stealth, Survival, Swim } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { Bargain, Carouse, Charm, Etiquette, FolkKen, Guile, Intrigue, Music, Leadership, Teaching } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { LangEnglish, LangHighGerman, LangItalian, LangKoineGreek, LangArabic } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { LangLatin, LangAncientGreek, LangHebrew, LangGothic } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { CraftTanner, CraftLeatherworker, CraftCarpenter, CraftWoodcarver, CraftBlacksmith, CraftJeweler, CraftWhitesmith } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { ProfScribe, ProfApothecary, ProfJongleur, ProfReeve, ProfSailor, ProfSteward, ProfTeamster, ProfWasherwoman } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { ArtesLiberales, ArtOfMemory, Chirurgy, Medicine, Philosophiae } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { LawCodeOfHermes, LawCivilAndCanon, LawIslamic } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { TheologyChristian, TheologyJudaic, TheologyIslamic, TheologyPagan } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { AnimalKen, Dowsing, EnchantingMusic, Entrancement, MagicSensitivity, Premonitions, SecondSight, SenseHolyUnholy, Shapeshifter, WildernessSense } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { TheEnigma, HeartBeast, FaerieMagic } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { HermeticMagicTheory, FolkWitchMagicTheory, ParmaMagica, Certamen, Concentration, Finesse, Penetration, Recuperation, Withstanding } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { MagicLore, FaerieLore, DominionLore, InfernalLore } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { AreaLoreVeryBasic, AreaLoreSomeCountry, AreaLoreSomeTribunal, AreaLoreSecretMagical, AreaLoreSomeCovenant } );
            AllCommonAbilities.AddRange( new List<ArchAbility>() { OrgLoreChurch, OrgLoreOrderOfHermes, OrgLoreVeryBasic, OrgLoreSomeKnightOrder, OrgLoreSomeNobleCourt, OrgLoreSomeCraftGuild } );
        }

        // TODO: cache this into a more intelligent lookup.  Ths is a brute force loop.
        public static ArchAbility lookupCommonAbilities(string ability)
        {
            foreach (var a in ArchAbility.AllCommonAbilities)
            {
                if (a.Name == ability) { 
                    return a; 
                }
            }
            throw new AbilityNotFoundException(ability + " not supported.");
        }

        public static string[] getCommonAbilities()
        {
            List<string> abilities = new List<string>();
            foreach (var a in ArchAbility.AllCommonAbilities)
            {
                abilities.Add(a.Name);
            }

            // Sort the abililties
            abilities.Sort();

            return abilities.ToArray();
        }

    }

    public class AbilityInstance
    {
        // To make a public property NOT present in the DataGridView, apply an attribute such as:
        //     [System.ComponentModel.Browsable(false)]
        // 
        //[Browsable(false)]
        //public string PropertyNotAppearingInThisDataGridView { get; set; }
        // 
        // To make a public property present in the DataGridView (and thus accessible to business logic), but NOT displayed:
        //     dataGridView1.Columns[0].Visible = false;

        [Browsable(false)]
        public ArchAbility Ability    { get; private set; }

        [Browsable(false)]
        public decimal     BaseXPCost { get { return Ability.BaseXpCost; } }

        [Browsable(false)]
        public string Category   { get { return this.Ability.Category;          } }

        [Browsable(false)]
        public string Type       { get { return this.Ability.Type.Name;         } }

        [DisplayName("Type")]
        public string TypeAbbrev { get { return this.Ability.Type.Abbreviation; } }

        public string Name       { get { return this.Ability.Name;              } }
        public int    XP         { get; set; }
        public int    Score      { get => AbilityXpCosts.ScoreForXP(XP, determineXpCost(Ability)); set => throw new NotImplementedException(); }

        [DisplayName(" ")]
        public string AddToScore { get { return this.HasPuissance ? this.PuissantBonus.ToString() : null; } }

        public string Specialty  { get; set; }

        public List<string> CommonSpecializations { get { return this.Ability.CommonSpecializations; } }

        [Browsable(false)]
        public bool HasThisAbility { get; set; } = false;  // If false, display a blank value for XP / Score, rather than 0 / 0

        [DisplayName("Aff")]
        public bool HasAffinity    { get; set; } = false;

        [DisplayName("Pui")]
        public bool HasPuissance   { get; set; } = false;

        [Browsable(false)]
        public int  PuissantBonus  { get; private set; } = 2;

        // Used to tie this abillity to the journal entry associated with it.
        // TODO: This is a bit complicated, could we simplify the design?  This is necessary to support deletion from a GUI.  
        public string id;

        public decimal determineXpCost(ArchAbility archAbility)
        {
            return HasAffinity? AbilityXpCosts.BaseXpCostWithAffinity(archAbility.BaseXpCost) : archAbility.BaseXpCost;
        }

        // TODO:
        // Something more will be needed to represent how Languages
        // get a Puissant-like bonus from a related Language with a higher Score...

        public AbilityInstance ( ArchAbility ability, int xp = 0, string specialty = "", 
            bool hasAffinity = false, bool hasPuissance = false, int puissantBonus = 2)
        {
            decimal xpCost = determineXpCost(ability);

            this.Ability = ability;
            this.XP      = xp;
            this.Specialty     = specialty ?? "";
            this.HasAffinity   = hasAffinity;
            this.HasPuissance  = hasPuissance;
            this.PuissantBonus = puissantBonus;
            this.id = createID();
        }

        public override string ToString ()
        {
            string str = string.Format("{0} '{1}' XP={2} / S={3} ({4}) A={5}, P={6}",
                this.TypeAbbrev, this.Name, this.XP, this.Score, this.Specialty, HasAffinity, HasPuissance);
            return str;
        }

        public static string createID()
        {
            Guid myuuid = Guid.NewGuid();
            return myuuid.ToString();
        }

    }



    ////////////////////////////////////////////




    public static class AbilityXpCosts
    {
        // TODO: 
        // Replace the brute-force-and-stupidity "loop until the Score is reached" logic 
        // with some a proper means of calculating the arithmetic sequence 
        // (or table-lookup up to some N, if that proves to be an optimization).

        #region List methods
        //public static List<int> ListXpRequiredForScore(int maxScore, int baseXpCost)
        //{
        //    ValidateAbilityScoreValue( maxScore );
        //    ValidateBaseXpCostValue(baseXpCost);
        //
        //    var results = new List<int>();
        //    for (int score = 0; score <= maxScore; score++ )
        //    {
        //        int nn = XPRequiredForScore(score, baseXpCost);
        //        results.Add( nn );
        //    }
        //    return results;
        //}
        #endregion

        #region XP/Score methods
        public static int XPRequiredForScore( int score, decimal baseXpCost )
        {
            var unroundedValue = ArithmeticSequence(score) * baseXpCost;
            var xp = RoundAsInt( unroundedValue );  
            // Note:
            // Results differ in some cases from my previously printed table,
            // such as (10 * (2/3*5)) resulting in 33 rather than 34.
            // It seems like that table was rounding UP sometimes, for .3333 ?
            // As I recall, that printed table used numbers printed by 
            // a Perl script, so uncertain what rounding mode was used...
            return xp;
        }

        public static int ScoreForXP( int currentXp, decimal baseXpCost )
        {
            ValidateXpValue( currentXp );
            ValidateBaseXpCostValue(baseXpCost);

            int result = 0;
            for (int score = 0; score <= 99; score++ )
            {
                int nn = XPRequiredForScore( score, baseXpCost );
                if (nn > currentXp)
                {
                    result = score - 1;
                    break;
                }
                if (nn == currentXp)
                {
                    result = score;
                    break;
                }
                // Otherwise, if (mm < currentXp) keep on looking...
            }
            return result;
        }

        //public static int RemainingXpUntilAbilityScoreIncrease(int currentXp, int baseXpCost)
        //{
        //    ValidateXpValue( currentXp );
        //    ValidateBaseXpCostValue(baseXpCost);
        //
        //    int score = ScoreForXP(currentXp, baseXpCost);
        //    int required = XPRequiredForScore(score, baseXpCost);
        //    int nn = currentXp = required;
        //
        //    return nn;
        //}
        #endregion

        public static decimal BaseXpCostWithAffinity(decimal baseXpCost)
        {
            ValidateBaseXpCostValue(baseXpCost);

            decimal adjustedForAffinity = baseXpCost * (2.0m / 3.0m);
            return adjustedForAffinity;
        }

        public static decimal ArithmeticSequence( int score )
        {
            ValidateAbilityScoreValue( score );

            decimal value = 0.0m;
            for ( int ii = 0; ii <= score; ii++ )
            {
                value += ii;
            }
            return value;
        }

        public static int RoundAsInt ( decimal value )
        {
            decimal rounded =  Math.Round(value, MidpointRounding.AwayFromZero);
            int result = (int)rounded;
            return result;
        }

        public static int CeilingAsInt( decimal value )
        {
            decimal roundedUp = Math.Ceiling(value);
            int result = (int)roundedUp;
            return result;
        }

        #region Argument Validation methods
        public static void ValidateAbilityScoreValue( int score )
        {
            if (score < 0)
            {
                throw new ArgumentException("score < 0");
            }
            if ( score > 99 )
            {
                throw new ArgumentException("score > 99");
            }
            // Otherwise, OK
        }

        public static void ValidateXpValue( int xp )
        {
            if (xp < 0)
            {
                throw new ArgumentException("xp < 0");
            }
            if ( xp > 9999)
            {
                throw new ArgumentException("xp > 9999");
            }
            // Otherwise, OK
        }

        public static void ValidateBaseXpCostValue(decimal baseXpCost)
        {
            if (baseXpCost <= 0.0m)
            {
                throw new ArgumentException("baseXpCost <= 0.0");
            }
            if ( baseXpCost > 60.0m)
            {
                throw new ArgumentException("baseXpCost > 60.0");
            }
            // Otherwise, OK
        }
        #endregion
    }

}
