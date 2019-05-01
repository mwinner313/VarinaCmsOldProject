using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using EntityFramework.DynamicFilters;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.GlobalFilters
{
    public class VisibilityControlledGlobalFilter: GlobalFilter
    {
        [SetterProperty]
        public IIdentityManager IdentityManager { get; set; }
        public override void ApplyFilter(DbModelBuilder modelBuilder)
        {
           modelBuilder.Filter("filter_base_upon_visibility",
               (IVisibliltyControlled item, bool isAdminAreaRequest, bool isSchudeldProccess) =>
                   item.IsVisible || isAdminAreaRequest|| isSchudeldProccess
               , IsAdminAreaRequest, IsCurrentThreadScheduledItem);
        }

        private bool IsCurrentThreadScheduledItem()
        {
            return    Thread.CurrentThread.IsBackground;
        }

        private bool IsAdminAreaRequest()
        {
            return HttpContext.Current != null && (HttpContext.Current.Request.Headers.AllKeys.Any(x => x == "X-Panel-Request") &&
                                                   IdentityManager.GetCurrentIdentity().IsAuthenticated &&
                                                   IdentityManager.HasPremission(IdentityManager.GetCurrentIdentity(), PanelPremission.Default));
        }
    }
}
