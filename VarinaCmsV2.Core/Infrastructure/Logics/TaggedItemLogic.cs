using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Infrastructure.Logics
{
    public class TaggedItemLogic : IBaseBeforeAddingEntityLogic, IBaseBeforeUpdatingEntityLogic
    {
        private ITagDataService _tagDataService;
        private IUnitOfWork _unitOfWork;
        public TaggedItemLogic()
        {

        }

        [SetterProperty]
        public IContainer Container { get; set; }
        public async Task ApplyLogicAsync<T>(T obj, IPrincipal requestOwner) where T : class
        {
          
            if (!(obj is ITaggedItem)) return;
            _tagDataService = Container.GetInstance<ITagDataService>();
            _unitOfWork = Container.GetInstance<IUnitOfWork>();

            var taggedItem = obj as ITaggedItem;
            var tags = new List<Tag>();
            taggedItem.Tags.ToList().ForEach(tags.Add);
           taggedItem.Tags.Clear();
            foreach (var tag in tags)
            {
                var dbtag = _tagDataService.Query.FirstOrDefault(x => x.Id == tag.Id);
                if (dbtag != null)
                {
                    taggedItem.Tags.Add(dbtag);
                    _unitOfWork.Entry(dbtag).State = EntityState.Unchanged;
                }
                else
                {
                    var urlBuilder = Container.GetInstance<IUrlBuilder>();
                    tag.Url = urlBuilder.GenrateUrlSegment(tag.Title);
                    tag.Handle = urlBuilder.GenrateUrlSegment(tag.Title);
                    _unitOfWork.Entry(tag).State=EntityState.Added;
                    taggedItem.Tags.Add(tag);
                }
            }
        }
    }
}
