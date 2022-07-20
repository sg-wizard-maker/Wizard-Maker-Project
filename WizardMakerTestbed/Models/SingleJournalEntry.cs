using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace WizardMakerTestbed.Models
{
    public enum Season { SPRING, SUMMER, AUTUMN, WINTER }

    public class SeasonYear
    {
        public int Year { get; set; }
        public Season season { get; set; }
    }

    public interface Journalable
    {
        string getText();

        // TODO: We should have a class for season.
        SeasonYear getDate();
    }

    public class SingleJournalEntry : Journalable
    {
        public string JournalEntryText { get; set; }
        public SeasonYear SeasonYear { get; set; }

        public string getText()
        {
            return JournalEntryText;
        }

        public SeasonYear getDate()
        {
            throw new NotImplementedException();
        }
    }

    public class XpAbilitySpendJournalEntry : Journalable
    {

        public SingleJournalEntry text;

        public string getText()
        {
            return text.JournalEntryText;
        }

        public SeasonYear getDate()
        {
            return text.getDate();
        }
    }

}
