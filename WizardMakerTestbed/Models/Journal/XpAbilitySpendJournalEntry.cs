using System;

namespace WizardMakerPrototype.Models
{
    //TODO: If a user edits an XP Value *during character creation*,
    //   should we optimize the journal entries by removing the original (or combining into one)?
    // That would break an undo command....
    public class XpAbilitySpendJournalEntry : Journalable
    {

        public SingleJournalEntry text;
        public string ability;
        public int xp;
        public string specialty; 

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
            CharacterRenderer.addAbility(character, ability, xp, specialty, character.IsInitialCharacterFinished());
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }

        public override string getId()
        {
            return text.getId();
        }

        public override Boolean IsSameSpecs(Journalable other)
        {
            if (!base.IsSameSpecs(other)) return false;
            XpAbilitySpendJournalEntry o2 = (XpAbilitySpendJournalEntry)other;
            if (!o2.ability.Equals(ability)) return false;
            if (!o2.specialty.Equals(specialty)) return false;
            if (o2.xp != xp) return false;
            if (!o2.text.IsSameSpecs(text)) return false;
            return true;
        }
    }
}
