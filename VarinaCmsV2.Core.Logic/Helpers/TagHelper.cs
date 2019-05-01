using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Page;
using VarinaCmsV2.ViewModel.Tag;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class TagHelper
    {
        public static EntityTagLiquidViewModel AsLiquidAdaptedWithEntities(this Tag tag, List<Entity> entities)
        {
            var adapted = Mapper.Map<EntityTagLiquidViewModel>(tag);
            adapted.Entities = new List<EntityLiquidAdapter>();
            entities.ForEach(x => adapted.Entities.Add(Mapper.Map<EntityLiquidAdapter>(x)));
            return adapted;
        }
        public static EntityTagLiquidViewModel AsLiquidAdaptedWithEntities(this Tag tag, PaginatedEntities entities)
        {
            var adapted = tag.AsLiquidAdaptedWithEntities(entities.Entities);
            adapted.AllPagesCount = entities.AllPagesCount;
            adapted.CurrentPage = entities.CurrentPage;
            return adapted;
        }
        public static PageTagLiquidViewModel AsLiquidAdaptedWithPages(this Tag tag, List<Page> pages)
        {
            var adapted = Mapper.Map<PageTagLiquidViewModel>(tag);
            adapted.Pages = new List<PageLiquidViewModel>();
            pages.ForEach(x => adapted.Pages.Add(Mapper.Map<PageLiquidViewModel>(x)));
            return adapted;
        }
        public static EntityTagLiquidViewModel AsLiquidAdapted(this Tag tag)
        {
            return Mapper.Map<EntityTagLiquidViewModel>(tag);
        }
        public static List<TagViewModel> MapToViewModel(this IEnumerable<Tag> tags)
        {
            return Mapper.Map<List<TagViewModel>>(tags);
        }
    }
}

