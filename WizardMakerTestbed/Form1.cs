using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

using WizardMakerTestbed.Models;
using WizardMakerTestbed.Controls;
using WizardMakerPrototype.Models;

namespace WizardMakerTestbed
{
    // A skeleton project (with a bit of GUI using WinForms, for the sake of displaying a few things)
    // for the purpose of testing out data structures,
    // to be used in the back-end of the Wizard Maker Project.
    // 
    public partial class MainProgramWindow : Form
    {
        private AbilityGridControl GridForAbilities { get; set; }
        private DataGridView GridForCharacteristics { get; set; }

        private Label         TheTextBoxLabel  { get; set; }
        private TextBox       TheTextBox { get; set; }

        //private Label         TheListBoxLabel  { get; set; }
        //private ListBox       TheListBox       { get; set; }
        private Button        TheButton        { get; set; }
        //private NumericUpDown TheNumericUpDown { get; set; }

        BindingList<AbilityInstance> bindingListOfAbilityInstances = new BindingList<AbilityInstance>();

        public const bool AFF    = true;
        public const bool NO_AFF = false;
        public const bool PUI    = true;
        public const bool NO_PUI = false;

        protected void OnTheButtonClicked ( object sender, EventArgs ea )
        {
            ConsolePrint("TheButton clicked");

            int ii = 1;
            foreach ( var a in bindingListOfAbilityInstances )
            {
                string str = string.Format("{0} - {1}", ii, a.ToString() );
                ConsolePrint( str );
                ii++;
            }

            //decimal cost1 = 1m;
            //decimal cost5 = 5m;
            //decimal cost1Affinity = (2m/3m) * cost1;
            //decimal cost5Affinity = (2m/3m) * cost5;
            //decimal cost5Linguist = (4m/5m) * cost5;
            //ConsolePrint( "Score:  XP(1), XP(2/3) --- XP(5), XP(2/3*5)" );
            //for ( int nn = 1; nn <= 20; nn++ )
            //{
            //    int xpCost1    = AbilityXpCosts.XPRequiredForScore(nn, cost1);
            //    int xpCost1Aff = AbilityXpCosts.XPRequiredForScore(nn, cost1Affinity);
            //    int xpCost5    = AbilityXpCosts.XPRequiredForScore(nn, cost5);
            //    int xpCost5Aff = AbilityXpCosts.XPRequiredForScore(nn, cost5Affinity);
            //    int xpCost5Lng = AbilityXpCosts.XPRequiredForScore(nn, cost5Linguist);
            //    string str = string.Format("{0}: {1}, {2} --- {3}, {4}, {5}", nn, xpCost1, xpCost1Aff, xpCost5, xpCost5Aff, xpCost5Lng);
            //    ConsolePrint( str );
            //}
        }

        public MainProgramWindow()
        {
            this.InitializeComponent();

            this.TheButton.Click += new EventHandler( OnTheButtonClicked );

            #region GridForAbilities
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.Brawl,  AbilityInstance.createID(), 30, "Dodging"          ) );
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.Awareness, AbilityInstance.createID(), 15, "At night",         NO_AFF, PUI, 2 ) );
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.Charm, AbilityInstance.createID(), 4, "First impressions"                ) );
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.FolkKen,  AbilityInstance.createID(), 50, "Motivations",      AFF, NO_PUI    ) );
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.Guile,  AbilityInstance.createID(), 6, "Not lying"        ) );

            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.LangHighGerman,  AbilityInstance.createID(), 75, "(dialect)"     ) );
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.LangLatin,  AbilityInstance.createID(), 50, "Hermetic usage") );
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.ArtesLiberales,  AbilityInstance.createID(), 15, "Astronomy"     ) );
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.HermeticMagicTheory,  AbilityInstance.createID(), 5, "Spell research") );
            bindingListOfAbilityInstances.Add( new AbilityInstance( ArchAbility.Concentration,  AbilityInstance.createID(), 140, "Duration",      AFF, PUI, 2 ) );

            this.GridForAbilities.DataSource = bindingListOfAbilityInstances;

            int w = this.GridForAbilities.ColumnCount;
            int h = this.GridForAbilities.RowCount;
            DataGridViewCell cell_X_Y = this.GridForAbilities[2,2];  // Specialty, Metaphysics
            cell_X_Y.ToolTipText = "Custom Tooltip Text for this cell";
            #endregion

            #region Experimenting with other Controls
            #endregion

        }

        public void ConsolePrint( string text )
        {
            // This method emits text to a particular TextBox, to demo a text stream output
            TheTextBox.AppendText( text + "\r\n" );
        }
    }
}
