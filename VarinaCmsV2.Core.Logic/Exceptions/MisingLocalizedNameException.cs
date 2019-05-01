using System;
using System.Runtime.Serialization;

using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Exceptions
{
    [Serializable]
    internal class MisingLocalizedNameException : Exception
    {

        public MisingLocalizedNameException()
        {
        }

        public MisingLocalizedNameException(string message) : base(message)
        {
        }

        private static string ConstructMessage(FieldDefenition defenition, Language lang)
        {
            return $"Missing LocalizedName : {defenition.Title} In language {lang.Name}";
        }
        public MisingLocalizedNameException(FieldDefenition defenition,Language lang):base(ConstructMessage(defenition, lang))
        {
           
        }

        public MisingLocalizedNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MisingLocalizedNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}