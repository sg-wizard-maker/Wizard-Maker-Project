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
            //TODO: Magic number 25.  Make this specifiable by user
            this.characterManager = new CharacterManager(25);
            this.Text = characterManager.getCharacterName();
            updateCharacterDisplay();
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
                    characterManager.updateAbilityDuringCreation(abilityDialog.abilityListBoxDialog.SelectedItem.ToString(),
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

            // Update the XPPools json
            XPPoolsJson.Text = characterManager.renderXPPoolsAsJson();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            // Handle a deletion
            if (dataGridView1.Columns[colIndex].HeaderText == "Delete")
            {
                
                string deletedAbility = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                string abilityId = retrieveAbilityIdFromAbilityName(deletedAbility);
                if (abilityId == null)
                {
                    throw new ShouldNotBeAbleToGetHereException("Could not find ID for " + deletedAbility);
                }
                characterManager.deleteAbility(abilityId);
                updateCharacterDisplay();
            }
        }

        private string retrieveAbilityIdFromAbilityName(string name)
        {
            CharacterData c = characterManager.renderCharacterAsCharacterData();
            foreach (var a in c.abilities)
            {
                if (a.Name == name)
                {
                    return a.Id;
                }
            }

            return null;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColumnIndex;

            string ability = getDataGridAbility(rowIndex);
            if (isDataGridXPCell(colIndex))
            {
                characterManager.updateAbilityDuringCreation(ability, int.Parse(getCellValueAsString(rowIndex, colIndex)), getCellValueAsString(rowIndex, 3));
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
