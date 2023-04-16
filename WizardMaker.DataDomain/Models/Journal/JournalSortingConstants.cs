using System;

namespace WizardMaker.DataDomain.Models.Journal
{
    internal class JournalSortingConstants
    {
        // Within the season+year, how to sort.
        // Lower goes first.
        // We want virtues to come before new character init, which defines XP Pools.
        public static int SEASON_YEAR_SORT_ORDER        = 100;
        public static int NEW_CHARACTER_INIT_SORT_ORDER = SEASON_YEAR_SORT_ORDER -  50;
        public static int ADD_VIRTUE_SORT_ORDER         = SEASON_YEAR_SORT_ORDER - 100;
    }
}
