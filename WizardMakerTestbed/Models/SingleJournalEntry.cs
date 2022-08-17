using System;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace WizardMakerTestbed.Models
{
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
    }

    /** Just a note with a season and year attached to it.
    * Does not actually do anything.
    */
    public class SingleJournalEntry : Journalable
    {
        public string JournalEntryText { get; set; }
        public SeasonYear SeasonYear { get; set; }
        public String Id { get; }

        public SingleJournalEntry(string journalEntryText, SeasonYear seasonYear)
        {
            JournalEntryText = journalEntryText;
            SeasonYear = seasonYear;

            Guid myuuid = Guid.NewGuid();
            Id = myuuid.ToString();

        }

        public string getText()
        {
            return JournalEntryText;
        }

        public SeasonYear getDate()
        {
            return SeasonYear;
        }

        public void Execute()
        {
            //no-op
        }

        public void Undo()
        {
            //no-op
        }

        public string getId()
        {
            return Id;
        }
    }
}
