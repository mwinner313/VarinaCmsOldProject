using System;
using System.Runtime.Serialization;
using System.Text;

using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Exceptions
{
    [Serializable]
    internal class MisingEntityField : Exception
    {


        public MisingEntityField()
        {
        }

        public MisingEntityField(string message) : base(message)
        {
        }

        public MisingEntityField(string message, Exception innerException) : base(message, innerException)
        {
        }

        public MisingEntityField( FieldDefenition defenition) : base(ConstructMessage(defenition))
        {
        }
        private static string ConstructMessage(FieldDefenition model)
        {
            var str = new StringBuilder();
            str.AppendLine($" Mising value for field {model.Title}");
            return str.ToString();
        }
        protected MisingEntityField(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}