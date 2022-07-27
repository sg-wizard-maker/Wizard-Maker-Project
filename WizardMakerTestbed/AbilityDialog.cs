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

namespace WizardMakerPrototype
{
    public partial class AbilityDialog : Form
    {
        public AbilityDialog()
        {
            InitializeComponent();
        }

        private void abilityListBoxDialog_SelectedIndexChanged(object sender, EventArgs e)
        {
            specialtyComboBox1.Items.Clear();
            if (abilityListBoxDialog.SelectedItems.Count == 1) {
                string a = abilityListBoxDialog.SelectedItems[0].ToString();
                specialtyComboBox1.Items.AddRange(ArchAbility.lookupCommonAbilities(a).CommonSpecializations.ToArray());
            }
            
        }

        private void AbilityDialog_Load(object sender, EventArgs e)
        {
            // Load the list of Abilities
            abilityListBoxDialog.Items.AddRange(ArchAbility.getCommonAbilities());
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void specialtyComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
