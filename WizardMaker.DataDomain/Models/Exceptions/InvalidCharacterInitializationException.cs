using System;
using System.Runtime.Serialization;

namespace WizardMaker.DataDomain.Models
{
    [Serializable]
    internal class InvalidCharacterInitializationException : Exception
    {
        public InvalidCharacterInitializationException()
        {
        }

        public InvalidCharacterInitializationException(string message) 
            : base(message)
        {
        }

        public InvalidCharacterInitializationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected InvalidCharacterInitializationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
