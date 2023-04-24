using System;

namespace WizardMaker.DataDomain.Models;

public enum Season { SPRING, SUMMER, AUTUMN, WINTER }

public class SeasonYear
{
    public int Year { get;  }
    public Season season { get; }

    public SeasonYear(int year, Season season)
    {
        this.Year = year;
        this.season = season;
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        SeasonYear other = obj as SeasonYear;
        bool result = (this.Year == other.Year) && (this.season == other.season);
        return result;
    }
}

// Just a note with a season and year attached to it.
// Does not actually do anything.
public class SimpleJournalEntry : Journalable
{
    public string     JournalEntryText { get; set; }
    public SeasonYear SeasonYear       { get; set; }
    public string     Id               { get; }

    public SimpleJournalEntry(string journalEntryText, SeasonYear seasonYear)
    {
        JournalEntryText = journalEntryText;
        SeasonYear       = seasonYear;

        Guid myGuid = Guid.NewGuid();
        Id = myGuid.ToString();
    }

    public override string GetText()
    {
        return JournalEntryText;
    }

    public override SeasonYear GetDate()
    {
        return SeasonYear;
    }

    public override void Execute(Character character)
    {
        // no-op
    }

    public override void Undo()
    {
        // no-op
    }

    public override string GetId()
    {
        return Id;
    }
}
