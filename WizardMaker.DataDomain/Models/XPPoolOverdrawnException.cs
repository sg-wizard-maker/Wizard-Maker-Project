using System;
using System.Runtime.Serialization;

namespace WizardMaker.DataDomain.Models
{
    [Serializable]
    internal class XPPoolOverdrawnException : Exception
    {
        public XPPoolOverdrawnException()
        {
        }

        public XPPoolOverdrawnException(string message) 
            : base(message)
        {
        }

        public XPPoolOverdrawnException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected XPPoolOverdrawnException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
