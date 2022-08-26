﻿using System;
using WizardMakerPrototype.Models;

namespace WizardMakerTestbed.Models
{
    //TODO: If a user edits an XP Value *during character creation*,
    //   should we optimize the journal entries by removing the original (or combining into one)?
    // That would break an undo command....
    public class XpAbilitySpendJournalEntry : Journalable
    {

        public SingleJournalEntry text;
        private string ability;
        private int xp;
        private string specialty; 

        public XpAbilitySpendJournalEntry(string entry, SeasonYear sy, string ability, 
            int xp, string specialty)
        {
            this.text = new SingleJournalEntry(entry, sy);

            this.ability = ability;
            this.xp = xp;
            this.specialty = specialty; 
        }

        public override string getText()
        {
            return text.JournalEntryText;
        }

        public override SeasonYear getDate()
        {
            return text.getDate();
        }

        public override void Execute(Character character)
        {
            CharacterRenderer.addAbility(character, ability, xp, specialty);
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }

        public override string getId()
        {
            return text.getId();
        }
    }
}