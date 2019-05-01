using DotLiquid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.FrontEndOptions;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.DomainClasses.Entities;
using Block = DotLiquid.Block;

namespace VarinaCmsV2.Core.Logic.Templating.Liquid.Blocks
{
    public class EntityPaginator : Block
    {
        private readonly IEntityDataService _entityData;

        public EntityPaginator(IEntityDataService entityData)
        {
            _entityData = entityData;
        }


        public override void Render(Context context, TextWriter result)
        {
            var paginationSize = FrontEndDeveloperOptions.Instance.Pagination.Default;
            int currentPage = GetCurrentPage();
            var data = new
            {
                items = CreateQuery().Skip((currentPage - 1) * paginationSize)
                     .Take(paginationSize).ToList().AsLiquidAdapted(),

                current_page = currentPage,

                all_pages_count = GetAllPagesCount(),

                items_count= CreateQuery().Count()
            };
            context["entiry"] = data;
            base.Render(context, result);
        }

        private int GetAllPagesCount()
        {
            var entityCount = CreateQuery().Count();
            if (entityCount != 0 && entityCount < FrontEndDeveloperOptions.Instance.Pagination.Default) return 1;

            return  (int) Math.Ceiling((double)entityCount / FrontEndDeveloperOptions.Instance.Pagination.Default);
        }

        private static int GetCurrentPage()
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values.ContainsKey("pageNumber") ?
                Convert.ToInt32(HttpContext.Current.Request.RequestContext.RouteData.Values["pageNumber"].ToString()) : 1;
        }

        private IQueryable<Entity> CreateQuery()
        {
            return _entityData
                .Query
                .Where(x => x.Scheme.Handle == Markup)
                .IncludeNecessaryData().OrderByDescending(x => x.PublishDateTime);
        }
    }
}
