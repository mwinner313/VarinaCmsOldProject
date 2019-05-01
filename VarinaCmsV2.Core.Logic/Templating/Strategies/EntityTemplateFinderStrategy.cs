
using StructureMap;
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
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Templating.Strategies
{
    public class EntityTemplateFinderStrategy : ILiquidTemplateFinderStrategy
    {
        private readonly string _base = AppDomain.CurrentDomain.BaseDirectory + WebConfigurationManager.AppSettings["template-base-path"];
        private readonly string _extension =  WebConfigurationManager.AppSettings["template-extension"];
        private readonly IContainer _container;
        public EntityTemplateFinderStrategy(IContainer container)
        {
            _container = container;
        }

        public string GetTemplatePathDefault(ITemplatedItem item)
        {
            CheckTypeCorrection(item);
            return GetTemplate(item);
        }

        public string GetTemplatePathByConvention(ITemplatedItem item = null)
        {
            return GetTemplatePathDefault(item);
        }

        private string GetTemplate(ITemplatedItem item)
        {
            var entity = (EntityLiquidAdapter)item;
            var templatePathByPrimaryCat = string.Empty;
            var cat = entity.Entity.PrimaryCategory;
            while (cat != null)
            {
                templatePathByPrimaryCat = $"{CreateExpectedFileNameForEntityWithPrimaryCat(entity, cat)}";
                if (cat.ParentId.HasValue && !File.Exists(_base + templatePathByPrimaryCat + _extension))
                {
                    cat = _container.GetInstance<IUnitOfWork>().Set<Category>().FirstOrDefault(x => x.Id == cat.ParentId);
                }
                else
                {
                    break;
                }
            }


            return File.Exists(_base + templatePathByPrimaryCat + _extension)
                ? templatePathByPrimaryCat
                : $"single-{entity.Scheme.Handle}";
        }

        private string CreateExpectedFileNameForEntityWithPrimaryCat(EntityLiquidAdapter entity, Category primaryCategory)
        {
            return $"single-{entity.Scheme.Handle}-cat-{primaryCategory.Handle}";
        }

        private void CheckTypeCorrection(ITemplatedItem item)
        {
            if (!(item is EntityLiquidAdapter)) throw new InvalidITempatedItemException(item, this);
        }
    }
}
