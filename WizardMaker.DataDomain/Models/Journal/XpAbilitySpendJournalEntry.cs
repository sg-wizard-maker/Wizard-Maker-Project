using System;

namespace WizardMaker.DataDomain.Models
{
    // TODO:
    // If a user edits an XP Value *during character creation*,
    // should we optimize the journal entries by removing the original (or combining into one)?
    // That would break an undo command....
    // 
    // Dev Note:
    // Deletion of abilities are assumed to link to multiple journal entries.
    // No XpAbilitySpendJournalEntry can grant XP to more than one ability.
    // If this capability is needed, then a more complex mechanism will be needed for implementation.
    public class XpAbilitySpendJournalEntry : Journalable
    {
        public SimpleJournalEntry Text;
        public string Ability;
        public int    Xp;
        public string Specialty; 

        // Note, when initializing make sure to include the ability name in the input entry string.
        // We do some matching on the XP Ability journal entries when in the same season for consolidation.
        public XpAbilitySpendJournalEntry(string entry, SeasonYear sy, string ability, int xp, string specialty)
        {
            this.Text      = new SimpleJournalEntry(entry, sy);
            this.Ability   = ability;
            this.Xp        = xp;
            this.Specialty = specialty; 
        }

        public override string GetText()
        {
            return Text.JournalEntryText;
        }

        public override SeasonYear GetDate()
        {
            return Text.GetDate();
        }

        public override void Execute(Character character)
        {
            CharacterRenderer.AddAbility(character, Ability, Xp, Specialty, character.IsInitialCharacterFinished(), this.GetId());
        }

        public override void Undo()
        {
            throw new NotImplementedException();
        }

        public override string GetId()
        {
            return Text.GetId();
        }

        public override bool IsSameSpecs(Journalable other)
        {
            if (!base.IsSameSpecs(other)) return false;
            XpAbilitySpendJournalEntry o2 = (XpAbilitySpendJournalEntry)other;
            if (!o2.Ability.Equals(Ability))     return false;
            if (!o2.Specialty.Equals(Specialty)) return false;
            if (o2.Xp != Xp)                     return false;
            if (!o2.Text.IsSameSpecs(Text))      return false;
            return true;
        }
    }
}
