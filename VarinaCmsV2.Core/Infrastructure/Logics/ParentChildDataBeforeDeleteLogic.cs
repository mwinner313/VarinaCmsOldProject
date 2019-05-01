using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.Logics
{
    public class ParentChildDataBeforeDeleteLogic:IBaseBeforeDeleteEntityLogic
    {
        [SetterProperty]
        public IContainer Container { get; set; }

        private IUnitOfWork unitOfWork;

        [SetterProperty]
        public IUrlBuilder UrlBuilder { get; set; }
        public async Task ApplyLogicAsync<T>(T obj, IPrincipal requestOwner) where T :class 
        {
            if (!(obj is IParentChildUrlChainedItem<T>))
                return;
            RefreshunitOfWork();
            var item = obj as IParentChildUrlChainedItem<T>;
            unitOfWork.Attach(item);
            unitOfWork.Entry(item).Collection(x=>x.Childs).Load();
            foreach (var child in item.Childs.ToList())
            {
                RemoveRootBetwenSelfAndChilds(item, child as IParentChildUrlChainedItem<T>);
                await FixUrlByRemovingRootBetwenSelfAndChilds(item, child as IParentChildUrlChainedItem<T>);
            }
        }

        private async Task FixUrlByRemovingRootBetwenSelfAndChilds<T>(IParentChildUrlChainedItem<T> root, IParentChildUrlChainedItem<T> child) where T : class
        {
            var parent=child.ParentId==root.ParentId?root.Parent as IParentChildUrlChainedItem<T> :
            LoadParent(child);
            child.Url = parent == null ? child.UrlSegment : $"{parent.Url}/{child.UrlSegment}";
            unitOfWork.Update(child);
            unitOfWork.Entry(child).Collection(x => x.Childs).Load();
            await unitOfWork.SaveChangesAsync();
            foreach (var ch in child.Childs.ToList())
            {
              await  FixUrlByRemovingRootBetwenSelfAndChilds(root, ch as IParentChildUrlChainedItem<T>);
            }
        }

        private IParentChildUrlChainedItem<T> LoadParent<T>(IParentChildUrlChainedItem<T> child) where T : class
        {
            unitOfWork.Entry(child).Reference(x => x.Parent).Load();
            return child.Parent as IParentChildUrlChainedItem<T>;
        }

        private void RemoveRootBetwenSelfAndChilds<T>(IParentChildUrlChainedItem<T> root, IParentChildUrlChainedItem<T> child) where T : class
        {

            child.ParentId = root.ParentId;
            unitOfWork.Update(child);
        }

        public void RefreshunitOfWork()
        {
            unitOfWork = Container.GetInstance<IUnitOfWork>();
        }
    }
}
