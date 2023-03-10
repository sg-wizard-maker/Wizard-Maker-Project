namespace WizardMaker
{
    partial class AbilityDialog
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
            this.abilityListBoxDialog = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.xpUpdown1 = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.specialtyComboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.xpUpdown1)).BeginInit();
            this.SuspendLayout();
            // 
            // abilityListBoxDialog
            // 
            this.abilityListBoxDialog.FormattingEnabled = true;
            this.abilityListBoxDialog.Location = new System.Drawing.Point(12, 12);
            this.abilityListBoxDialog.Name = "abilityListBoxDialog";
            this.abilityListBoxDialog.Size = new System.Drawing.Size(270, 420);
            this.abilityListBoxDialog.TabIndex = 3;
            this.abilityListBoxDialog.SelectedIndexChanged += new System.EventHandler(this.abilityListBoxDialog_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(394, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 52);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // xpUpdown1
            // 
            this.xpUpdown1.Location = new System.Drawing.Point(316, 17);
            this.xpUpdown1.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.xpUpdown1.Name = "xpUpdown1";
            this.xpUpdown1.Size = new System.Drawing.Size(137, 20);
            this.xpUpdown1.TabIndex = 5;
            this.xpUpdown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.xpUpdown1.ValueChanged += new System.EventHandler(this.xpUpdown1_ValueChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(291, 380);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 51);
            this.button2.TabIndex = 6;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "XP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Specialty";
            // 
            // specialtyComboBox1
            // 
            this.specialtyComboBox1.FormattingEnabled = true;
            this.specialtyComboBox1.Location = new System.Drawing.Point(293, 81);
            this.specialtyComboBox1.Name = "specialtyComboBox1";
            this.specialtyComboBox1.Size = new System.Drawing.Size(177, 21);
            this.specialtyComboBox1.TabIndex = 10;
            // 
            // AbilityDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 450);
            this.Controls.Add(this.specialtyComboBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.xpUpdown1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.abilityListBoxDialog);
            this.Name = "AbilityDialog";
            this.Text = "AbilityDialog";
            this.Load += new System.EventHandler(this.AbilityDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xpUpdown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ListBox abilityListBoxDialog;
        public System.Windows.Forms.NumericUpDown xpUpdown1;
        public System.Windows.Forms.ComboBox specialtyComboBox1;
    }
}