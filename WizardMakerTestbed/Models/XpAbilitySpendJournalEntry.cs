﻿using System;
using WizardMakerPrototype.Models;

namespace WizardMakerTestbed.Models
{
    //TODO: If a user edits an XP Value *during character creation*,
    //   should we optimize the journal entries by removing the original (or combining into one)?
    // That would break an undo command....
    public class XpAbilitySpendJournalEntry : IJournalable
    {

        public SingleJournalEntry text;
        private Character character;
        private string ability;
        private int xp;
        private string specialty; 

        public XpAbilitySpendJournalEntry(string entry, SeasonYear sy, Character character, string ability, 
            int xp, string specialty)
        {
            this.text = new SingleJournalEntry(entry, sy);
        
            this.character = character;
            this.ability = ability;
            this.xp = xp;
            this.specialty = specialty; 
        }

        public string getText()
        {
            return text.JournalEntryText;
        }

        public SeasonYear getDate()
        {
            return text.getDate();
        }

        public void Execute()
        {
            CharacterRenderer.addAbility(character, ability, xp, specialty);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public string getId()
        {
            return text.getId();
        }
    }
}
