using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WizardMakerTestbed.Models;

namespace WizardMakerPrototype.Models
{
    public class Character
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<AbilityInstance> abilities { get; set; }

        List<Journalable> journalEntries { get; set; }

        public Character(string name, string description, List<AbilityInstance> abilities, List<Journalable> journalEntries)
        {
            Name = name;
            Description = description;
            this.abilities = abilities;
            this.journalEntries = journalEntries;

        }
    }
}
