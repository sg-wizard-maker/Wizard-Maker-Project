namespace WizardMaker
{
    partial class Tester
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataXP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataSpecialty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataDeleteRow = new System.Windows.Forms.DataGridViewButtonColumn();
            this.XPPoolsJson = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(19, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Ability";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(93, 142);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 474);
            this.panel1.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataName,
            this.dataScore,
            this.dataXP,
            this.dataSpecialty,
            this.dataDeleteRow});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(545, 474);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // dataName
            // 
            this.dataName.Frozen = true;
            this.dataName.HeaderText = "Name";
            this.dataName.Name = "dataName";
            this.dataName.ReadOnly = true;
            // 
            // dataScore
            // 
            this.dataScore.Frozen = true;
            this.dataScore.HeaderText = "Score";
            this.dataScore.Name = "dataScore";
            this.dataScore.ReadOnly = true;
            // 
            // dataXP
            // 
            this.dataXP.HeaderText = "XP";
            this.dataXP.Name = "dataXP";
            // 
            // dataSpecialty
            // 
            this.dataSpecialty.HeaderText = "Specialty";
            this.dataSpecialty.Name = "dataSpecialty";
            // 
            // dataDeleteRow
            // 
            this.dataDeleteRow.HeaderText = "Delete";
            this.dataDeleteRow.Name = "dataDeleteRow";
            this.dataDeleteRow.ReadOnly = true;
            // 
            // XPPoolsJson
            // 
            this.XPPoolsJson.AutoSize = true;
            this.XPPoolsJson.Location = new System.Drawing.Point(676, 9);
            this.XPPoolsJson.Name = "XPPoolsJson";
            this.XPPoolsJson.Size = new System.Drawing.Size(47, 13);
            this.XPPoolsJson.TabIndex = 7;
            this.XPPoolsJson.Text = "XPPools";
            // 
            // Tester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 628);
            this.Controls.Add(this.XPPoolsJson);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Name = "Tester";
            this.RightToLeftLayout = true;
            this.Text = "New Character";
            this.Load += new System.EventHandler(this.Tester_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataXP;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataSpecialty;
        private System.Windows.Forms.DataGridViewButtonColumn dataDeleteRow;
        private System.Windows.Forms.Label XPPoolsJson;
    }
}