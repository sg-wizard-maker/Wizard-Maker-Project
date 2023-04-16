using System;

namespace WizardMaker.DataDomain.Models
{
    public interface ICharacterCommand
    {
        void Execute(Character c);
        void Undo();
    }
}
