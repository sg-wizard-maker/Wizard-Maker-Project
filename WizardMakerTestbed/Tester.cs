using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WizardMakerTestbed.Models;
using WizardMakerPrototype.Models;
using Newtonsoft.Json;

namespace WizardMakerPrototype
{
    public partial class Tester : Form
    {
        public Models.CharacterManager characterManager;
        

        public Tester()
        {
            InitializeComponent();
        }

        private void Tester_Load(object sender, EventArgs e)
        {
            this.characterManager = new CharacterManager();
            this.Text = characterManager.getCharacterName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbilityDialog abilityDialog = new AbilityDialog();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (abilityDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                // TODO: Make the abilityDialog attributes non-public
                if (abilityDialog.abilityListBoxDialog.SelectedItem != null)
                {
                    characterManager.addAbility(abilityDialog.abilityListBoxDialog.SelectedItem.ToString(),
                        ((int)abilityDialog.xpUpdown1.Value), abilityDialog.specialtyComboBox1.Text);
                    updateCharacterDisplay();
                }
            }

            abilityDialog.Dispose();
            
            }

        /**
         * General update method for updating the display of the character.
         */
        private void updateCharacterDisplay()
        {

            dataGridView1.Rows.Clear();

            CharacterData c = characterManager.renderCharacterAsCharacterData();

            foreach (var a in c.abilities)
            {
                //TODO: Make constants from the magic numbers
                dataGridView1.Rows.Add(a.Name);
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = a.XP.ToString();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = a.Score.ToString();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = a.Specialty;
            }
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            // Handle a deletion
            if (dataGridView1.Columns[colIndex].HeaderText == "Delete")
            {
                characterManager.deleteAbility(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());
                updateCharacterDisplay();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            string ability = getDataGridAbility(rowIndex);
            if (isDataGridXPCell(colIndex))
            {
                characterManager.setAbilityXp(ability, int.Parse(getCellValueAsString(rowIndex, colIndex)), getCellValueAsString(rowIndex, 3));
                updateCharacterDisplay();
            }
        }

        private string getCellValueAsString(int rowIndex, int colIndex)
        {
            if (dataGridView1.Rows[rowIndex].Cells[colIndex].Value != null)
            {
                return dataGridView1.Rows[rowIndex].Cells[colIndex].Value.ToString();
            } else
            {
                return "";
            }
        }

        private bool isDataGridXPCell(int colIndex)
        {
            return (dataGridView1.Columns[colIndex].HeaderText == "XP");
        }

        private string getDataGridAbility(int rowIndex)
        {
            return dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
        }
    }
}
