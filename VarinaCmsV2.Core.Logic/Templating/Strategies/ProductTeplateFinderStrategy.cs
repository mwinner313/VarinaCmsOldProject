using System;
using System.IO;
using System.Web.Configuration;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Templating.Exceptions;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    internal class ProductTeplateFinderStrategy : ILiquidTemplateFinderStrategy
    {
        private readonly string _base = AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["template-base-path"];
        private readonly string _extension = WebConfigurationManager.AppSettings["template-extension"];
        public string GetTemplatePathDefault(ITemplatedItem item = null)
        {
            if (!(item is ProductLiquidAdapter product)) throw new InvalidITempatedItemException(item, this);

            if (product.Type == ProductType.Grouped && File.Exists(_base + "product-grouped" + _extension))
                return "product-grouped";

            return "product";
        }

        public string GetTemplatePathByConvention(ITemplatedItem item = null)
        {
            if (!(item is ProductLiquidAdapter product)) throw new InvalidITempatedItemException(item, this);

            return "product-" + product.Handle;
        }
    }
}