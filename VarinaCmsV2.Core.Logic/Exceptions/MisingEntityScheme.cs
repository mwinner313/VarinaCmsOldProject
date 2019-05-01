using System;
using System.Runtime.Serialization;

namespace VarinaCmsV2.Core.Logic.Exceptions
{
    [Serializable]
    internal class MisingEntityScheme : Exception
    {
        public MisingEntityScheme()
        {
        }

        public MisingEntityScheme(string message) : base(message)
        {
        }

        public MisingEntityScheme(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MisingEntityScheme(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}