using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Category;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class CategoryMapHeleper
    {
        public static CategoryViewModel MapToViewModel(this Category category)
        {
            return Mapper.Map<CategoryViewModel>(category);
        }
        public static Category MapToModel(this CategoryAddOrUpdateViewModel category)
        {
            return Mapper.Map<Category>(category);
        }
        public static void MapToExisting(this CategoryAddOrUpdateViewModel viewModel, Category category)
        {
            Mapper.Map(viewModel, category);
        }
        public static async Task<List<CategoryViewModel>> ToViewModelListIncludeMetaDataAsync(this IQueryable<Category> model, IMetaDataDataService metaDataService)
        {
            var items = Mapper.Map<List<CategoryViewModel>>(await model.ToListAsync());
            var ids = items.SelectDeep(x => x.Id);
            metaDataService.Query.Where(x => ids.Contains(x.TargetId) && x.TargetType == Category.MetaTypeName).ToList().ForEach(x =>
            {
                items.ForEachDeep(c =>
                {
                    if (c.Id == x.TargetId)
                        c.MetaData.Add(x.MapToViewModel());
                });
            });
            return items;
        }
        public static List<T> SelectDeep<T>(this IEnumerable<CategoryViewModel> queriable, Func<CategoryViewModel, T> select)
        {
            List<T> list = new List<T>();
            foreach (var x in queriable)
            {
                list.Add(select(x));
                if (x.Childs != null && x.Childs.Count != 0)
                    list.AddRange(x.Childs.SelectDeep(select));
            }
            return list;
        }
        public static void ForEachDeep(this IEnumerable<CategoryViewModel> queriable, Action<CategoryViewModel> action)
        {

            foreach (var x in queriable)
            {
                action(x);
                if (x.Childs != null && x.Childs.Count != 0)
                    x.Childs.ForEachDeep(action);
            }
        }
        public static List<CategoryViewModel> ToViewModelList(this IEnumerable<Category> model)
        {
            return Mapper.Map<List<CategoryViewModel>>(model.ToList());
        }

        public static CategoryLiquidViewModel AsLiquidAdaptedWithEntitiesAndMetaData(this Category category, PaginatedEntities entities, IMetaDataDataService metaData)
        {
            var adapted = category.AsLiquidAdaptedWithEntities(entities.Entities);
            adapted.CurrentPage = entities.CurrentPage;
            adapted.AllPagesCount = entities.AllPagesCount;
            adapted.Metas = metaData.Query.Where(x => x.TargetId == category.Id && x.TargetType == Category.MetaTypeName).MapToLiquid();
            return adapted;
        }
        public static CategoryLiquidViewModel AsLiquidAdaptedWithEntities(this Category category, List<Entity> entities)
        {
            var adapted = Mapper.Map<CategoryLiquidViewModel>(category);
            adapted.Entities = new List<EntityLiquidAdapter>();
            foreach (var x in entities)
            {
                adapted.Entities.Add(Mapper.Map<EntityLiquidAdapter>(x));
            }
            return adapted;
        }
        public static CategoryLiquidViewModel AsLiquidAdapted(this Category category)
        {
            return Mapper.Map<CategoryLiquidViewModel>(category);
        }
    }
}
