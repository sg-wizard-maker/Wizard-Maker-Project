using System;
using WizardMakerPrototype.Models.HermeticArts;

namespace WizardMakerPrototype.Models
{
    //TODO: If a user edits an XP Value *during character creation*,
    //   should we optimize the journal entries by removing the original (or combining into one)?
    // That would break an undo command....
    // Dev note:  Deletion of abilities are assumed to link to multiple journal entries.  No XpAbilitySpendJournalEntry can grant XP to more than one ability.  If this
    //  capability is needed, then a more complex mechanism will be needed for implementation.
    public class XpSkillSpendJournalEntry : Journalable
    {

        public SimpleJournalEntry text;
        public string skill;
        public int xp;
        public string specialty; 

        // Note, when initializing make sure to include the ability name in the input entry string.
        //  We do some matching on the XP Ability journal entries when in the same season for consolidation.
        public XpSkillSpendJournalEntry(string entry, SeasonYear sy, string skill, 
            int xp, string specialty)
        {
            this.text = new SimpleJournalEntry(entry, sy);

            this.skill = skill;
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
            // Determine if this is an abillity or Art.  
            ArchHermeticArt archHermeticArt = ArchHermeticArt.lookupArt(skill);
            if (archHermeticArt != null)
            {
                CharacterRenderer.addHermeticArt(character, skill, xp, specialty, character.IsInitialCharacterFinished(), this.getId());
            }
            else
            {
                CharacterRenderer.AddAbility(character, skill, xp, specialty, character.IsInitialCharacterFinished(), this.getId());
            }
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
            XpSkillSpendJournalEntry o2 = (XpSkillSpendJournalEntry)other;
            if (!o2.skill.Equals(skill)) return false;
            if (!o2.specialty.Equals(specialty)) return false;
            if (o2.xp != xp) return false;
            if (!o2.text.IsSameSpecs(text)) return false;
            return true;
        }
    }
}
