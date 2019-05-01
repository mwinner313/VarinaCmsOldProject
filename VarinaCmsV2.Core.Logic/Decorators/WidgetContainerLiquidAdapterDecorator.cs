using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using DotLiquid;
using VarinaCmsV2.Core.Contracts.Decorators;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Core.Logic.Widgets;

namespace VarinaCmsV2.Core.Logic.Decorators
{
    public class WidgetContainerLiquidAdapterDecorator: LiquidDataDecorator
    {
        private readonly Drop _wrappe;

        public WidgetContainerLiquidAdapterDecorator(Drop wrapee)
        {
            _wrappe = wrapee;
        }
        public WidgetContainerLiquidAdapterDecorator()
        {
        }

        public override bool ShouldDecorate(Drop wrappe)
        {
            return true;
        }

        public override LiquidAdapter Decorate(Drop wrapee)
        {
          return new WidgetContainerLiquidAdapterDecorator(wrapee);
        }

        public override object BeforeMethod(string method)
        {
            if (method == "widget_containers")
                return new LiquidWidgetFinder();
            return _wrappe.BeforeMethod(method);
        }

        public override int LevelToReachRealWrappe { get; } = 3;
    }
}
