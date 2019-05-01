using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Templating.Strategies;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Templating.Exceptions
{
    [Serializable]
    internal class InvalidITempatedItemException : Exception
    {
       

        public InvalidITempatedItemException()
        {
        }

        public InvalidITempatedItemException(string message) : base(message)
        {
        }

        public InvalidITempatedItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidITempatedItemException(ITemplatedItem item, ILiquidTemplateFinderStrategy categoryTemplateFinderStrategy):base(CunstructMessage(item, categoryTemplateFinderStrategy))
        {
           
        }

        private static string CunstructMessage(ITemplatedItem item, ILiquidTemplateFinderStrategy strategy)
        {
            return $"invalid templated item : {item.GetType().Name} for strategy : {strategy.GetType().Name}";
        }

        protected InvalidITempatedItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}