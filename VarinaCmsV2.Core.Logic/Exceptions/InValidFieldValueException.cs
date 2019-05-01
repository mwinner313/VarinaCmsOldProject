using System;
using System.Runtime.Serialization;
using System.Text;

using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Exceptions
{
    [Serializable]
    public class InValidFieldValueException : Exception
    {
        public InValidFieldValueException()
        {
        }

        public InValidFieldValueException(string message) : base(message)
        {
        }

        public InValidFieldValueException(string message, Exception innerException) : base(message, innerException)
        {
        }
        private static string ConstructMessage(FieldDefenition model, Type factoryType)
        {
            var str = new StringBuilder();
            str.AppendLine($"Invalid value for field {model.Type}");
            str.AppendLine($"Target Factory For This Field : {factoryType.AssemblyQualifiedName}");
            return str.ToString();
        }
        public InValidFieldValueException(FieldDefenition model,Type factoryType) :base(ConstructMessage(model, factoryType))
        {
           
      
        }

        protected InValidFieldValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

      
    }
}