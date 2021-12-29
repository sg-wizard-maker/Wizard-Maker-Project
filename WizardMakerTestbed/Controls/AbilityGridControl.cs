using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardMakerTestbed.Controls
{
    public class AbilityGridControl : DataGridView
    {
        private const int COL_TYPE      = 0;
        private const int COL_ABILITY   = 1;
        private const int COL_SPECIALTY = 2;
        private const int COL_SCORE     = 3;
        private const int COL_XP        = 4;

        private const int NUM_COLUMNS = 5;

        // TODO: Provide for COL_SCORE and COL_XP being formatted with commas, when needed...
        // TODO: Provide for COL_SCORE and COL_XP being sortable via numeric VALUE, though display is a STRING value...

        // TODO: Much refactoring (out of Form1.Designer.cs) still needed...
        // TODO: Refactor characteristics grid into a class like this one...

        public AbilityGridControl()
        {
            //this.ForeColor = Color.Red;  // grid cells text color
            //this.BackgroundColor = Color.Blue;  // color for background of control where grid is not painted

            this.EnableHeadersVisualStyles = false;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;  // Disallow manual vertical resize of column headers
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            this.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            this.ColumnHeadersDefaultCellStyle.Font = new Font( this.Font, FontStyle.Bold );

            this.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.DefaultCellStyle.SelectionBackColor = Color.PowderBlue;

            this.Name = "GridForAbilities";
            this.Location = new Point( 350, 101 + 30 );
            this.Size = new Size( 600, 250 );

            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.AllowUserToResizeRows = false;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            this.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            this.GridColor = Color.Black;
            this.RowHeadersVisible = false;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // or DataGridViewSelectionMode.CellSelect
            this.MultiSelect = false;
            this.Dock = DockStyle.None;  // or DockStyle.Fill to fill parent


            //this.ColumnCount = NUM_COLUMNS;

            // Columns and Column Headers / Column Header Cells
            //this.Columns[COL_TYPE     ].Name = "Type";
            //this.Columns[COL_ABILITY  ].Name = "Ability";
            //this.Columns[COL_SPECIALTY].Name = "Specialty";
            //this.Columns[COL_SCORE    ].Name = "Score";
            //this.Columns[COL_XP       ].Name = "XP";

            //this.Columns[COL_TYPE     ].Width = 40;
            //this.Columns[COL_ABILITY  ].Width = 120;  // Hmmm...might want a dynamic width sometimes...
            //this.Columns[COL_SPECIALTY].Width = 120;  // Hmmm...might want a dynamic width sometimes...
            //this.Columns[COL_SCORE    ].Width = 50;
            //this.Columns[COL_XP       ].Width = 50;

            //#region Center the HeaderCell and cell contents of the "Score" and "XP" columns...
            //this.Columns[COL_SCORE].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.Columns[COL_XP   ].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //this.Columns[COL_SCORE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  // .MiddleCenter;
            //this.Columns[COL_XP   ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;  // .MiddleCenter;
            //#endregion

            //this.Columns[COL_TYPE     ].ToolTipText = "Type of Ability (Gen, Gc, Martial, Academic, Arcane, Supernatural, etc)";
            //this.Columns[COL_ABILITY  ].ToolTipText = "Name of the Ability";
            //this.Columns[COL_SPECIALTY].ToolTipText = "Specialty of the Ability (when relevant, effective Score +1)";
            //this.Columns[COL_SCORE    ].ToolTipText = "Score of the Ability (a value 0 or higher; 5 is professional, 9+ is extremely high)";
            //this.Columns[COL_XP       ].ToolTipText = "Current Experience Points for the Ability";

            //// TODO: Once data is loaded, set ToolTipText for the CELLS in COL_XP to show current_XP / needed_to_advance...

        }
    }
}
