using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMaker.DataDomain.Validation
{
    public static class ValidationLog
    {
        // TODO:
        // For testing, we always have to reset the ValidationLog manually.
        // We need to put some automation so that it is reset before every test.
        // Also, this has implications for running tests in parallel.

        static List<ValidationMessage> Messages = new List<ValidationMessage>();

        public static void AddValidationMessage(ValidationMessage msg)
        {
            Messages.Add(msg);
        }

        public static void AddValidationMessage(string msg)
        {
            AddValidationMessage(new ValidationMessage(msg));
        }

        public static List<ValidationMessage> GetMessages() 
        {
            return Messages; 
        }

        public static void Reset() 
        {
            Messages = new List<ValidationMessage>(); 
        }
    }
}
