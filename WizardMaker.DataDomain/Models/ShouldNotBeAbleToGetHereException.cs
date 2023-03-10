using System;
using System.Runtime.Serialization;

namespace WizardMaker.DataDomain.Models
{
    [Serializable]
    public class ShouldNotBeAbleToGetHereException : Exception
    {
        public ShouldNotBeAbleToGetHereException()
        {
        }

        public ShouldNotBeAbleToGetHereException(string message) : base(message)
        {
        }

        public ShouldNotBeAbleToGetHereException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ShouldNotBeAbleToGetHereException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}