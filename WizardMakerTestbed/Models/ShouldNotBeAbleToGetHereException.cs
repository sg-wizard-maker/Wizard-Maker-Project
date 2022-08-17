using System;
using System.Runtime.Serialization;

namespace WizardMakerPrototype.Models
{
    [Serializable]
    internal class ShouldNotBeAbleToGetHereException : Exception
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