﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardMakerPrototype.Models.Virtues;

namespace WizardMakerPrototype.Models.Journal
{
    public class AddVirtueJournalEntry : Journalable
    {
        public SimpleJournalEntry text;
        public ArchVirtue archVirtue;
        public const string PREFIX = "Added virtue: ";

        public AddVirtueJournalEntry(SeasonYear sy, ArchVirtue archVirtue)
        {
            this.text = new SimpleJournalEntry(PREFIX + archVirtue.Name, sy);
            this.archVirtue = archVirtue;
        }

        public override void Execute(Character character)
        {
            character.virtues.Add(new VirtueInstance(archVirtue));
        }

        public override SeasonYear getDate()
        {
            return text.getDate();
        }

        public override string getId()
        {
            return text.getId();
        }

        public override string getText()
        {
            return text.getText();
        }

        public bool isMatch(string virtueName)
        {
            return (PREFIX + archVirtue.Name) == (PREFIX + virtueName);
        }
    }
}
