using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Validation;

public class ValidationMessage
{
    public string Message { get; private set; }

    public ValidationMessage(string message)
    {
        this.Message = message;
    }       
}
