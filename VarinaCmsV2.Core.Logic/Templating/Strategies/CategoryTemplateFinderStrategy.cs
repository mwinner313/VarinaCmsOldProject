using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.Templating.Exceptions;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Category;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class CategoryTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {
        private readonly string _base = AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["template-base-path"];
        private readonly string _extension = WebConfigurationManager.AppSettings["template-extension"];

        public CategoryTemplateFinderStrategy()
        {
        }

        public string GetTemplatePathDefault(ITemplatedItem item)
        {
            CheckCorrectType(item);
            return GetTemplate(item);
        }

        public string GetTemplatePathByConvention(ITemplatedItem item = null)
        {
            return GetTemplatePathDefault(item);
        }

        private string GetTemplate(ITemplatedItem item)
        {
            var cat = item as CategoryLiquidViewModel;
            var path = $"Category/{CreateExpectedFileNameForCateory(cat)}";
            return File.Exists(_base + path + _extension)
                ? path
                : cat.Parent == null
                ? $"category-{cat.Scheme.Handle}"
                : GetTemplatePathDefault(cat.Parent);
        }

        private string CreateExpectedFileNameForCateory(CategoryLiquidViewModel cat)
        {
            return $"category-{cat.Scheme.Handle}-{cat.Handle}";
        }

        private void CheckCorrectType(ITemplatedItem item)
        {
            if (!(item is CategoryLiquidViewModel))
                throw new InvalidITempatedItemException(item, this);
        }
    }
}
