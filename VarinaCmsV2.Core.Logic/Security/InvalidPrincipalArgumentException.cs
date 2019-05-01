using System;
using System.Runtime.Serialization;
using System.Security.Principal;

namespace VarinaCmsV2.Core.Logic.Security
{
    [Serializable]
    internal class InvalidPrincipalArgumentException : Exception
    {
        public InvalidPrincipalArgumentException()
        {
        }

        public InvalidPrincipalArgumentException(string message) : base(message)
        {
        }
        public InvalidPrincipalArgumentException(IPrincipal principal) 
        {
        }

        public InvalidPrincipalArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPrincipalArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}