using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using WizardMakerPrototype.Models;

namespace WizardMakerPrototype.Models
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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            SeasonYear other = obj as SeasonYear;
            return this.Year == other.Year && this.season == other.season; 
        }
    }

    /** Just a note with a season and year attached to it.
    * Does not actually do anything.
    */
    public class SimpleJournalEntry : Journalable
    {
        public string JournalEntryText { get; set; }
        public SeasonYear SeasonYear { get; set; }
        public String Id { get; }

        public SimpleJournalEntry(string journalEntryText, SeasonYear seasonYear)
        {
            JournalEntryText = journalEntryText;
            SeasonYear = seasonYear;

            Guid myuuid = Guid.NewGuid();
            Id = myuuid.ToString();

        }

        public override string getText()
        {
            return JournalEntryText;
        }

        public override SeasonYear getDate()
        {
            return SeasonYear;
        }

        public override void Execute(Character character)
        {
            //no-op
        }

        public override void Undo()
        {
            //no-op
        }

        public override string getId()
        {
            return Id;
        }
    }
}
