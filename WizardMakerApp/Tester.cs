﻿using System.ComponentModel;
using System.Data;

using WizardMaker.DataDomain.Models;

namespace WizardMaker;

public partial class Tester : Form
{
    public CharacterManager characterManager;
    

    public Tester()
    {
        InitializeComponent();
    }

    private void Tester_Load(object sender, EventArgs e)
    {
        //TODO: Magic number 25.  Make this specifiable by user
        this.characterManager = new CharacterManager(25, ArchAbility.LangEnglish);
        this.Text = characterManager.GetCharacterName();
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
                characterManager.UpdateAbilityDuringCreation(abilityDialog.abilityListBoxDialog.SelectedItem.ToString(),
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

        CharacterData c = characterManager.RenderCharacterAsCharacterData();

        foreach (var a in c.Abilities)
        {
            //TODO: Make constants from the magic numbers
            dataGridView1.Rows.Add(a.Name);
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = a.XP.ToString();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = a.Score.ToString();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = a.Specialty;
        }
        dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

        // Update the XPPools text
        string xpPoolText = String.Join("\n", c.XPPools.Select(x => x.Name + " (Remaining: " + x.RemainingXP + ")").ToList());
        XPPoolsJson.Text = xpPoolText;
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        int rowIndex = e.RowIndex;
        int colIndex = e.ColumnIndex;

        // Handle a deletion
        if (dataGridView1.Columns[colIndex].HeaderText == "Delete")
        {

            string deletedAbility = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            List<string> abilityIds = retrieveAbilityIdsFromAbilityName(deletedAbility);
            if (abilityIds == null)
            {
                throw new ShouldNotBeAbleToGetHereException("Could not find ID for " + deletedAbility);
            }
            foreach (string abilityId in abilityIds) { 
                characterManager.DeleteJournalEntry(abilityId);
            }
            updateCharacterDisplay();
        }
    }

    private List<string> retrieveAbilityIdsFromAbilityName(string name)
    {
        CharacterData c = characterManager.RenderCharacterAsCharacterData();
        List<string> result = new List<string>();
        foreach (var a in c.Abilities)
        {
            if (a.Name == name)
            {
                result.AddRange(a.Id);
            }
        }

        return result;
    }

    private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        int rowIndex = e.RowIndex;
        int colIndex = e.ColumnIndex;

        string ability = getDataGridAbility(rowIndex);
        if (isDataGridXPCell(colIndex))
        {
            characterManager.UpdateAbilityDuringCreation(ability, int.Parse(getCellValueAsString(rowIndex, colIndex)), getCellValueAsString(rowIndex, 3));
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
