using System;
using System.Runtime.Serialization;

namespace WizardMakerPrototype.Models
{
    [Serializable]
    internal class AbilityNotFoundException : Exception
    {
        public AbilityNotFoundException()
        {
        }

        public AbilityNotFoundException(string message) : base(message)
        {
        }

        public AbilityNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AbilityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}