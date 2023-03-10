using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Models
{
    public interface ICharacterCommand
    {
        void Execute(Character c);
        void Undo();
    }
}
