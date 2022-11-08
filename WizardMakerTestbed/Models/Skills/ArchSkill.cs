using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Models.Skills
{
    public class ArchSkill
    {
        public string Abbreviation { get; private set; }
        public string Name { get; private set; }

        public decimal BaseXpCost { get; private set; }

        public ArchSkill(string abbreviation, string name, decimal baseXpCost)
        {
            Abbreviation = abbreviation;
            Name = name;
            BaseXpCost = baseXpCost;
        }
    }
}
