using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.Logics
{
    public class ParentChildUrlChainLogic : IBaseBeforeAddingEntityLogic, IBaseBeforeUpdatingEntityLogic
    {
        private readonly IUrlBuilder _urlBuilder;

        public ParentChildUrlChainLogic()
        {
            _urlBuilder = new UrlBuilder();
        }

        public async Task CheckParentChildConflict<T>(IParentChildUrlChainedItem<T> item, IDataService<T, Guid> dataService) where T : class
        {
            if(item.ParentId == null) return;
            var conflictIds = new List<Guid>();

            conflictIds.Add(item.Id);
            item.Childs?.ToList().ForEach(x=>AddSelfIdWithChildsIds( x as IParentChildUrlChainedItem<T>, conflictIds));

            if( conflictIds.Contains(item.ParentId.Value)) throw new InvalidOperationException($"this item has one of its child or the self id as parent id => {item}");
        }

        private void AddSelfIdWithChildsIds<T>(IParentChildUrlChainedItem<T> item, List<Guid> ids) where T:class
        {
           ids.Add(item.Id);
            item.Childs?.ToList().ForEach(x => AddSelfIdWithChildsIds(x as IParentChildUrlChainedItem<T>, ids));
        }

        public async Task FixItemAndChildUrls<T>(IParentChildUrlChainedItem<T> item, IDataService<T, Guid> dataService) where T : class
        {
            if (item == null) return;
            var unitOfWork = Container.GetInstance<IUnitOfWork>();
            await FixUrlByParent(item, dataService);
            unitOfWork
                .Entry(item)
                .Collection(x => x.Childs)
                .Load();
            foreach (var ch in item.Childs.ToList())
            {
                await FixItemAndChildUrls(ch as IParentChildUrlChainedItem<T>, dataService);
            }
        }

        private async Task FixUrlByParent<T>(IParentChildUrlChainedItem<T> item, IDataService<T, Guid> dataService) where T : class
        {
            item.UrlSegment = _urlBuilder.GenrateUrlSegment(string.IsNullOrEmpty(item.UrlSegment) ? item.Title : item.UrlSegment);
            if (item.ParentId.HasValue)
            {
                var parent = await ((IQueryable<IParentChildUrlChainedItem<T>>)(dataService.Query)).FirstOrDefaultAsync(x => x.Id == item.ParentId.Value) as IParentChildUrlChainedItem<T>;
                item.Url = $"{parent.Url}/{item.UrlSegment}";
            }
            else
            {
                item.Url = item.UrlSegment;
            }
            var unitOfWork = Container.GetInstance<IUnitOfWork>();
            unitOfWork.Update(item);
        }
        public async Task ApplyLogicAsync<T>(T obj, IPrincipal requestOwner) where T : class
        {
            if (obj is IParentChildUrlChainedItem<T>)
            {
                var dataService = Container.GetInstance<IDataService<T, Guid>>();

                await CheckParentChildConflict((IParentChildUrlChainedItem<T>)obj, dataService);
                await FixItemAndChildUrls(obj as IParentChildUrlChainedItem<T>, dataService);
            }
            var unitOfWork = Container.GetInstance<IUnitOfWork>();
        }



        [SetterProperty]
        public IContainer Container { get; set; }
    }

    public class InvalidCircularParentChildItem
         : Exception
    {
    }
}
