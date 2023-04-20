namespace WizardMaker.Controls;

public class CharacteristicsGridControl : DataGridView
{
    const int GRID_CHARACTERISTICS_WIDTH_EXTRA_BOLD = 8;
    const int GRID_CHARACTERISTICS_WIDTH_NO_BOLD    = 333;
    const int GRID_CHARACTERISTICS_WIDTH_BOLD       = GRID_CHARACTERISTICS_WIDTH_NO_BOLD + (2 * GRID_CHARACTERISTICS_WIDTH_EXTRA_BOLD);
    const int GRID_CHARACTERISTICS_WIDTH            = GRID_CHARACTERISTICS_WIDTH_BOLD;

    const int GRID_CHARACTERISTICS_HEIGHT_NO_HEADERS   = 91;
    const int GRID_CHARACTERISTICS_HEIGHT_WITH_HEADERS = (GRID_CHARACTERISTICS_HEIGHT_NO_HEADERS + 22);
    const int GRID_CHARACTERISTICS_HEIGHT              = GRID_CHARACTERISTICS_HEIGHT_WITH_HEADERS;

    public CharacteristicsGridControl ( int locationX, int locationY, bool hasIntelligence = true, bool showColumnHeaders = true )
    {
        string abilityNameIntOrCunning   = hasIntelligence ? "Intelligence" : "Cunning";
        string abilityAbbrevIntOrCunning = hasIntelligence ? "INT"          : "CUN";

        this.Name     = "GridForCharacteristics";
        this.Location = new Point( locationX, locationY );
        this.Size     = new Size( GRID_CHARACTERISTICS_WIDTH, GRID_CHARACTERISTICS_HEIGHT );
        this.AutoSizeColumnsMode      = DataGridViewAutoSizeColumnsMode.None;
        this.AllowUserToResizeColumns = false;
        this.AllowUserToResizeRows    = false;

        var headerCellStyleBold = this.ColumnHeadersDefaultCellStyle;
        headerCellStyleBold.Font = new Font(headerCellStyleBold.Font, FontStyle.Bold);

        this.ColumnCount = 7;
        this.ColumnHeadersVisible          = showColumnHeaders;  // Adjust GRID_CHARACTERISTICS_WIDTH, GRID_CHARACTERISTICS_HEIGHT based on this...
        this.ColumnHeadersDefaultCellStyle = headerCellStyleBold;
        this.RowHeadersVisible             = false;

        this.Columns[0].Name = "Characteristic";
        this.Columns[1].Name = "Abbr";
        this.Columns[2].Name = "Value";
        this.Columns[3].Name = "";
        this.Columns[4].Name = "Characteristic";
        this.Columns[5].Name = "Abbr";
        this.Columns[6].Name = "Value";

        this.Columns[0].Width = 80 + GRID_CHARACTERISTICS_WIDTH_EXTRA_BOLD;  // adjust if bold/not
        this.Columns[1].Width = 40;
        this.Columns[2].Width = 40;
        this.Columns[3].Width = 10;
        this.Columns[4].Width = 80 + GRID_CHARACTERISTICS_WIDTH_EXTRA_BOLD;  // adjust if bold/not
        this.Columns[5].Width = 40;
        this.Columns[6].Width = 40;

        this.Columns[3].DefaultCellStyle.BackColor = this.BackgroundColor;

        this.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        this.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        this.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        this.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        this.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        this.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;

        object[] charRow1 = { "Strength",  "STR", "-1", "", abilityNameIntOrCunning,  abilityAbbrevIntOrCunning, "+3" };
        object[] charRow2 = { "Stamina",   "STA", "+2", "", "Perception",    "PER", "+2" };
        object[] charRow3 = { "Dexterity", "DEX", "-3", "", "Presence",      "PRE", "+0" };
        object[] charRow4 = { "Quickness", "QIK", "+0", "", "Communication", "COM", "+1" };

        this.Rows.Clear();
        this.AllowUserToAddRows = false;
        this.Rows.Add( charRow1 );
        this.Rows.Add( charRow2 );
        this.Rows.Add( charRow3 );
        this.Rows.Add( charRow4 );
    }

}
