using System;
using System.Runtime.Serialization;

namespace VarinaCmsV2.Core.Modules
{
    [Serializable]
    internal class MisingModuleDllException : Exception
    {
        public MisingModuleDllException()
        {
        }

        public MisingModuleDllException(string message) : base(message)
        {
        }

        public MisingModuleDllException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MisingModuleDllException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}