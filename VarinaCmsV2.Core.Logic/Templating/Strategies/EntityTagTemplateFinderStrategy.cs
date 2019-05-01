using System;
using System.IO;
using System.Web.Configuration;
using VarinaCmsV2.Core.Contracts.Templating;
using VarinaCmsV2.Core.Logic.Templating.Exceptions;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Tag;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class EntityTagTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {
        private readonly string _base = AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["template-base-path"];
        private readonly string _extension = WebConfigurationManager.AppSettings["template-extension"];

        public EntityTagTemplateFinderStrategy()
        {

        }

        public string GetTemplatePathDefault(ITemplatedItem item)
        {
            if (!(item is EntityTagLiquidViewModel tag)) throw new InvalidITempatedItemException(item, this);
            if (tag.EntityScheme == null) return FindWithoutScheme(tag);
            return FindWithScheme(tag);
        }

        public string GetTemplatePathByConvention(ITemplatedItem item = null)
        {
            return GetTemplatePathDefault(item);
        }

        private string FindWithScheme(EntityTagLiquidViewModel entityTag)
        {
            var filename = $"Tag/tag-{entityTag.EntityScheme.Handle}-{entityTag.Handle}";
            return File.Exists(_base + filename + _extension)
                ? filename
                : $"tag-{entityTag.EntityScheme.Handle}";
        }

        private string FindWithoutScheme(EntityTagLiquidViewModel entityTag)
        {
            var filename = $"Tag/tag-{entityTag.Handle}";
            return File.Exists(_base + filename + _extension)
                ? filename
                : $"tag";
        }

    }
}
