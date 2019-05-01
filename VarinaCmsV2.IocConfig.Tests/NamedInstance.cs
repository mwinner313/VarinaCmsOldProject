using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using StructureMap.Web;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Logic.Widgets;
using VarinaCmsV2.Core.Logic.Widgets.Menu;
using VarinaCmsV2.IocCofig;
using VarinaCmsV2.IocCofig.ContractRegistries;

namespace VarinaCmsV2.IocConfig.Tests
{
    [TestClass]
    public class NamedInstance
    {
        [TestMethod]
        public void get_a_named_instance()
        {
            var container = new Container(x =>
            {
              x.AddRegistry<WidgetRegstries>();
                TypeHelper.FindListOf<IWidget>().ForEach(widgetType =>
                {
                    x.For<IWidget>().HybridHttpOrThreadLocalScoped().Use(()=>widgetType).Named(widgetType.Type);
                });
            });
            
            var widget = container.GetInstance<IWidget>("menu");
            Assert.IsTrue((widget is MenuWidget));
            Console.WriteLine(widget.Type);
        }

    }
}
