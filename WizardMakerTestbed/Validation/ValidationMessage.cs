﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Validation
{
    public class ValidationMessage
    {
        public string message { get; private set; }

        public ValidationMessage(string message)
        {
            this.message = message;
        }       
    }
}
