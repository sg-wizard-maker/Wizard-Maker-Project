using System;

namespace WizardMaker.DataDomain.Models
{
    //TODO: If a user edits an XP Value *during character creation*,
    //   should we optimize the journal entries by removing the original (or combining into one)?
    // That would break an undo command....
    // Dev note:  Deletion of abilities are assumed to link to multiple journal entries.  No XpAbilitySpendJournalEntry can grant XP to more than one ability.  If this
    //  capability is needed, then a more complex mechanism will be needed for implementation.
    public class XpAbilitySpendJournalEntry : Journalable
    {

        public SimpleJournalEntry text;
        public string ability;
        public int xp;
        public string specialty; 

        // Note, when initializing make sure to include the ability name in the input entry string.
        //  We do some matching on the XP Ability journal entries when in the same season for consolidation.
        public XpAbilitySpendJournalEntry(string entry, SeasonYear sy, string ability, 
            int xp, string specialty)
        {
            this.text = new SimpleJournalEntry(entry, sy);

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
            CharacterRenderer.addAbility(character, ability, xp, specialty, character.IsInitialCharacterFinished(), this.getId());
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
