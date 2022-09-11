using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardMakerPrototype.Validation
{
    public class ValidationLog
    {
        static List<ValidationMessage> messages = new List<ValidationMessage>();

        public static void AddValidationMessage(ValidationMessage msg)
        {
            messages.Add(msg);
        }

        public static void AddValidationMessage(string msg)
        {
            AddValidationMessage(new ValidationMessage(msg));
        }

        public static List<ValidationMessage> GetMessages() { return messages; }
    }
}
