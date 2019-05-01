using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EntityFramework.DynamicFilters;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.GlobalFilters
{
    public class ScheduledItemGlobalFilter : GlobalFilter
    {
        [SetterProperty]
        public IIdentityManager IdentityManager { get; set; }
        public override void ApplyFilter(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("scheduled_items_base_on_publishdatetime",
                (IScheduledItem item, bool isAdminAreaRequest) =>
                    item.PublishDateTime <= DateTime.Now.AddMinutes(4) || isAdminAreaRequest
                , IsAdminAreaRequest);
        }
        private bool IsAdminAreaRequest()
        {
            return HttpContext.Current != null && (HttpContext.Current.Request.Headers.AllKeys.Any(x => x == "X-Panel-Request") &&
                                                   IdentityManager.GetCurrentIdentity().IsAuthenticated &&
                                                   IdentityManager.HasPremission(IdentityManager.GetCurrentIdentity(), PanelPremission.Default));
        }
    }
}
