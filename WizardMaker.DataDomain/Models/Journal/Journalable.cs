using Newtonsoft.Json;

using WizardMaker.DataDomain.Models.CharacterPersist;
using WizardMaker.DataDomain.Models.Journal;

namespace WizardMaker.DataDomain.Models;

// TODO:
// Give it an ID field.
// This way we can support searching and easier deletion.
public abstract class Journalable: ICharacterCommand
{
    // This indicates that the sorting should be by the SeasonYear
    // For now, this is all journal entries.
    // Note that this also determines the order of rendering within the season.

    public abstract string GetText();

    public abstract SeasonYear GetDate();

    public abstract string GetId();

    public virtual string SerializeJson()
    {
        var settings = WMJsonSerializerSettings.CommonJsonSerializerSettings;
        var result   = JsonConvert.SerializeObject(this, Formatting.Indented, settings);
        return result;
    }

    public static Journalable? DeserializeJson(string json)
    {
        var settings = WMJsonSerializerSettings.CommonJsonSerializerSettings;
        var result   = JsonConvert.DeserializeObject(json, settings) as Journalable;
        return result;
    }

    #region Implementing ICharacterCommand
    public abstract void Execute(Character character);

    public virtual void Undo()
    {
        throw new NotImplementedException();
    }
    #endregion

    public virtual bool IsSameSpecs(Journalable other)
    {
        if (other == null) return false;
        if (this.GetType() != other.GetType())       return false;
        if (this.GetText() != other.GetText())       return false;
        if (!this.GetDate().Equals(other.GetDate())) return false;
        // Note that we do not check ID,
        // since this is just comparing equality across instances
        // and each instance should have its own ID
        return true;
    }

    public virtual int SortOrder()
    {
        return JournalSortingConstants.SEASON_YEAR_SORT_ORDER;
    }
}
