using System.Web.Routing;
using DotLiquid;
using StructureMap;
using StructureMap.Attributes;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Contracts.Decorators
{
    public abstract class LiquidDataDecorator:LiquidAdapter
    {
        public abstract int LevelToReachRealWrappe { get;}
        public RouteData RouteData;
        public abstract bool ShouldDecorate(Drop wrappe);
        public abstract LiquidAdapter Decorate(Drop wrapee);
        [SetterProperty]
        public IContainer Container { get; set; }



    }
}
