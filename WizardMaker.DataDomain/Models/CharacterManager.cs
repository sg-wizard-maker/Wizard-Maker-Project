using System.Runtime.CompilerServices;

using WizardMaker.DataDomain.Models.CharacterPersist;

[assembly: InternalsVisibleTo("WizardMakerTests")]
[assembly: InternalsVisibleTo("WizardMakerTests.Models")]
[assembly: InternalsVisibleTo("WizardMakerTests.Models.Tests")]

namespace WizardMaker.DataDomain.Models;

// This class is meant to interface with a front end, be it API calls or a GUI.
// This happens by adding journal entries and then rendering the Character from the assembled list of journal entries.
public class CharacterManager
{
    private Character Character;

    public const string ABILITY_CREATION_NAME_PREFIX = "Initial: ";
    public const int    SAGA_START_YEAR = 1220;

    #region Constructors
    public CharacterManager(int startingAge, ArchAbility? childhoodLanguage = null)
    {
        if (childhoodLanguage == null)
        {
            // Arguably, no Living Language is a more likely default than any other.
            // Possibly, the parameter should not be optional?
            childhoodLanguage = ArchAbility.LangEnglish;
        }
        this.Character = new Character("New Character", "", startingAge);
        
        // TODO: Re-assess whether initializing a journal entry with "this" has a smell.
        this.Character.AddJournalable(new NewCharacterInitJournalEntry(startingAge, childhoodLanguage, SAGA_START_YEAR));

        // TODO:
        // Need a layer that will judge what abilities a character is allowed to choose AT ANY TIME
        // (given that virtues and flaws can change this access).
        UpdateAbilityDuringCreation(childhoodLanguage.Name, NewCharacterInitJournalEntry.CHILDHOOD_LANGUAGE_XP, "");

        // Last step:  Render the character with all journal entries.
        CharacterRenderer.RenderAllJournalEntries(Character);
    }
    #endregion

    #region Methods (various)
    // TODO:
    // Make class to wrap character pools.
    // This way we can just obtain the pool for childhood, etc, through that interface.
    // And look at aggregate information.

    // TODO: Delete this method.  Caller should render the CharacterData and get the name there.
    public string GetCharacterName()
    {
        return Character.Name;
    }

    // Use during character creation, not as part of advancement.
    // This method can handle a new ability or an existing one.
    public void UpdateAbilityDuringCreation(string ability, int absoluteXp, string specialty)
    {
        // TODO: Fix the ordering because the SeasonYear must always be later than the NewCharacterJournalInit
        XpAbilitySpendJournalEntry xpAbilitySpendJournalEntry = 
            new XpAbilitySpendJournalEntry(
                ABILITY_CREATION_NAME_PREFIX + ability,
                new SeasonYear(1219, Season.SUMMER), 
                ability, 
                absoluteXp, 
                specialty
            );

        Character.AddJournalable(xpAbilitySpendJournalEntry);
        CharacterRenderer.RenderAllJournalEntries(Character);
    }

    public void DeleteJournalEntry(string id)
    {
        Character.RemoveJournalable(id);

        // Render, which will handle the resetting of XP Pools.
        CharacterRenderer.RenderAllJournalEntries(Character);
    }

    public CharacterData RenderCharacterAsCharacterData()
    {
        var result = CharacterRenderer.RenderCharacterAsCharacterData(Character);
        return result;
    }

    public int GetXPPoolCount() { return Character.XPPoolList.Count; }

    // This will always return a number >= 0.  
    // This will not include overdrawn.
    // Assumes that the overdrawn pool is the last one on the list.
    public int TotalRemainingXPWithoutOverdrawn()
    {
        var result = Character.TotalRemainingXPWithoutOverdrawn();
        return result;
    }

    public int GetJournalSize() 
    {
        var result = Character.GetJournal().Count;
        return result;
    }

    // TODO: A lot more error checking needs to take place here
    // TODO: How would this work for API front end? 
    // Note: This will overwrite any file
    public static void WriteToFile(Character c, string absoluteFilename)
    {
        FileStream   fs = new FileStream(absoluteFilename, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        RawCharacter rc = new RawCharacter(c);
        string  payload = rc.SerializeJson();
        sw.Write(payload);
        sw.Flush();
        sw.Close();
        fs.Close();
    }

    public static Character ReadFromFile(string absoluteFilename)
    {
        FileStream   fs = new FileStream(absoluteFilename, FileMode.Open, FileAccess.Read);
        StreamReader sw = new StreamReader(fs);
        string     json = sw.ReadToEnd();
        RawCharacter rc = RawCharacter.DeserializeJson(json);  // Hmmm...we may want to do something more fangled, to avoid calling ctor for many duplicate ArchAbility, ArchVirtue, etc...
        sw.Close();
        fs.Close();

        var result = new Character(rc);
        return result;
    }

    // TODO: Do we need this anymore?
    public static void RemoveLaterLifeXPPool(Character c)
    {
        c.XPPoolList.Remove(
            c.XPPoolList
                .Where(xppool => xppool.Name == NewCharacterInitJournalEntry.LATER_LIFE_POOL_NAME)
                .First()
        );
    }
    #endregion
}
