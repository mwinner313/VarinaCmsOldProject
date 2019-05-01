using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using StructureMap;
using StructureMap.Attributes;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.Logics
{
    public class TrackableItemLogic : IBaseBeforeAddingEntityLogic,IBaseBeforeUpdatingEntityLogic
    {
        public  async Task ApplyLogicAsync<T>(T obj,IPrincipal requestOwner) where T : class
        {
            if (obj is ITrackibleItem)
            {
                FillCreatorIdByHttpContextUserIdentity(obj as ITrackibleItem,requestOwner);
            }
            if (obj is IOptionalTrackibleItem)
            {
                FillCreatorIdByHttpContextUserIdentity(obj as IOptionalTrackibleItem, requestOwner);
            }
        }

        private void FillCreatorIdByHttpContextUserIdentity(ITrackibleItem trackible, IPrincipal requestOwner)
        {
            var idAsString =
                (requestOwner.Identity as ClaimsIdentity)?.GetUserId();
            if (idAsString == null) throw new ArgumentNullException("requestOwner.identity");
            trackible.CreatorId = trackible.CreatorId != Guid.Empty ? trackible.CreatorId : Guid.Parse(idAsString);
            trackible.CreateDateTime= trackible.CreateDateTime != DateTime.MinValue ? trackible.CreateDateTime : DateTime.Now;
            trackible.UpdateDateTime=DateTime.Now;
        }
        private void FillCreatorIdByHttpContextUserIdentity(IOptionalTrackibleItem trackible, IPrincipal requestOwner)
        {
            trackible.CreateDateTime = trackible.CreateDateTime != DateTime.MinValue ? trackible.CreateDateTime : DateTime.Now;
            trackible.UpdateDateTime = DateTime.Now;

            var idAsString =
                (requestOwner.Identity as ClaimsIdentity)?.GetUserId();
            if (idAsString == null)return;
            trackible.CreatorId = trackible.CreatorId ?? Guid.Parse(idAsString);
            }
        [SetterProperty]
        public IContainer Container { get; set; }
    }
}
