using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace WizardMakerTestbed.Models
{
    public class ExtremelyPrimitiveAbilityModel
    {
        // To make a public property NOT present in the DataGridView, apply an attribute such as:
        //     [System.ComponentModel.Browsable(false)]
        // 
        //[Browsable(false)]
        //public string PropertyNotAppearingInThisDataGridView { get; set; }
        // 
        // To make a public property present in the DataGridView (and thus accessible to business logic), but NOT displayed:
        //     dataGridView1.Columns[0].Visible = false;

        public string Category  { get; set; }
        public string Name      { get; set; }
        public string Specialty { get; set; }
        public int    Score     { get; set; }
        public int    XP        { get; set; }  // Current XP

        //public bool HasAffinity   { get; set; }
        //public int  PuissantBonus { get; set; }


        public ExtremelyPrimitiveAbilityModel (string category, string name, string specialty, int score, int currentXp)
        {
            this.Category  = category;
            this.Name      = name;
            this.Specialty = specialty;
            this.Score     = score;
            this.XP = currentXp;
        }

        public override string ToString()
        {
            string str = string.Format("[{0}] {1} ({2}) {3} / {4}", Category, Name, Specialty, Score, XP);
            return str;
        }
    }
}
