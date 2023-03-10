using System;
using System.Drawing;
using System.Windows.Forms;

using WizardMaker.DataDomain.Models;
using WizardMaker.Controls;

namespace WizardMaker
{
    partial class MainProgramWindow : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            const int COL1_LEFT    =  25;
            const int COL2_LEFT    = 125;
            const int ROW1_TOP     =   1;
            const int ROW2_TOP     = 280;
            const int ROW3_TOP     = 380;
            const int LABEL_HEIGHT =  15;

            #region These can be moved out of Designer:
            this.GridForCharacteristics = new CharacteristicsGridControl(350, 1, true);
            #endregion

            #region Below here, cannot yet be moved from Designer:
            this.GridForAbilities = new AbilityGridControl();  //new DataGridView();
            #endregion

            #region Probably will remove these:
            this.TheTextBoxLabel = new Label();
            this.TheTextBox = new TextBox();

            //this.TheListBoxLabel = new Label();
            //this.TheListBox = new ListBox();

            this.TheButton = new Button();
            //this.TheNumericUpDown = new NumericUpDown();
            #endregion

            this.SuspendLayout();


            #region Experimenting with other Controls
            //// TheNumericUpDown
            //this.TheNumericUpDown.Location = new Point( COL2_LEFT, ROW3_TOP );
            //this.TheNumericUpDown.Minimum = 0;
            //this.TheNumericUpDown.Maximum = 99;
            //this.TheNumericUpDown.DecimalPlaces = 0;
            //this.TheNumericUpDown.Value = 11;

            // TheButton
            this.TheButton.Location = new Point( COL1_LEFT, ROW2_TOP );
            this.TheButton.Name = "TheButton";
            this.TheButton.Size = new Size( 75, 23 );
            this.TheButton.TabIndex = 0;
            this.TheButton.Text = "The Button";
            this.TheButton.UseVisualStyleBackColor = true;

            //// TheListBoxLabel and TheListBox
            //this.TheListBoxLabel.Location = new Point( COL1_LEFT, ROW1_TOP );
            //this.TheListBoxLabel.BackColor = Color.PaleGreen;
            //this.TheListBoxLabel.Height = LABEL_HEIGHT;
            //this.TheListBoxLabel.AutoSize = true;
            //this.TheListBoxLabel.Text = "ListBox for TheListBox.Items.Add()";

            //this.TheListBox.FormattingEnabled = true;
            //this.TheListBox.Location = new Point( COL1_LEFT, ROW1_TOP + LABEL_HEIGHT );
            //this.TheListBox.Name = "ListBoxForTheListBoxItemsAdd";
            //this.TheListBox.Size = new Size( 300, 200 );
            //this.TheListBox.TabIndex = 1;
            //this.TheListBox.SelectionMode = SelectionMode.MultiExtended;  // Or SelectionMode.One
            //// Elements added in Form1.cs later...

            // TheTextBoxLabel and TheTextBox
            this.TheTextBoxLabel.Location = new Point( COL1_LEFT, ROW3_TOP );
            this.TheTextBoxLabel.BackColor = Color.PaleGreen;
            this.TheTextBoxLabel.Height = LABEL_HEIGHT;
            this.TheTextBoxLabel.AutoSize = true;
            this.TheTextBoxLabel.Text = "TextBox for ConsolePrint";

            this.TheTextBox.Location = new Point( COL1_LEFT, ROW3_TOP + LABEL_HEIGHT );
            this.TheTextBox.Name = "TextBoxForConsole";
            this.TheTextBox.Size = new Size( 600, 120 );
            this.TheTextBox.TabIndex = 2;
            this.TheTextBox.Multiline = true;
            this.TheTextBox.ScrollBars = ScrollBars.Vertical;
            // Text appended in Form1.cs via ConsolePrint() method later...

            //this.Controls.Add( this.TheListBoxLabel );
            //this.Controls.Add( this.TheListBox );

            this.Controls.Add( this.TheTextBoxLabel );
            this.Controls.Add( this.TheTextBox );

            this.Controls.Add( this.TheButton );
            //this.Controls.Add( this.TheNumericUpDown );
            #endregion

            // mainWindow
            this.AutoScaleDimensions = new SizeF( 6F, 13F );
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size( 1024, 600 );

            this.Controls.Add( this.GridForAbilities );
            this.Controls.Add( this.GridForCharacteristics );


            this.Name = "MainWindow";
            this.Text = "Wizard Maker - Main Window";
            this.ResumeLayout( false );
            this.PerformLayout();
        }

        #endregion
    }
}

