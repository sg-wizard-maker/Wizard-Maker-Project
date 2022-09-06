using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Journal
{
    internal class JournalSortingConstants
    {
        // within the season year, how to sort.  Lower goes first.
        public static int SEASON_YEAR_SORT_ORDER = 100;
        public static int NEW_CHARACTER_INIT_SORT_ORDER = SEASON_YEAR_SORT_ORDER - 100;
        public static int ADD_VIRTUE_SORT_ORDER = SEASON_YEAR_SORT_ORDER - 50;
    }
}
