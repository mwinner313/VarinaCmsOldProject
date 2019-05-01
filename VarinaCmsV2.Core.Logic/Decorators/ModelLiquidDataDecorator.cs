using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using DotLiquid;
using StructureMap;
using VarinaCmsV2.Core.Contracts.Decorators;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Decorators
{
    public class ModelLiquidDataDecorator: LiquidDataDecorator
    {
        private readonly Drop _wrappe;
        public ModelLiquidDataDecorator()
        {

        }
        public ModelLiquidDataDecorator(Drop wrappe)
        {
            _wrappe = wrappe;
        }

        public override bool ShouldDecorate(Drop wrappe)
        {
            return true;
        }

        public override LiquidAdapter Decorate(Drop wrapee)
        {
            return new ModelLiquidDataDecorator(wrapee) {Container = Container};
        }

        public override object BeforeMethod(string method)
        {
            if (method == "model") return _wrappe;
            return base.BeforeMethod(method);
        }

     public override int LevelToReachRealWrappe { get; } = 2;
    }
}
