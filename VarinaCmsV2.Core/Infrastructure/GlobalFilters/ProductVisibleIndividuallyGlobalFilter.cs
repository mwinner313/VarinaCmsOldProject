using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using EntityFramework.DynamicFilters;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.GlobalFilters
{
    public class ProductVisibleIndividuallyGlobalFilter:GlobalFilter
    {
        
        [SetterProperty]
        public IIdentityManager IdentityManager { get; set; }
        public override void ApplyFilter(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("filter_base_upon_visibile_individually",
                (Product item, bool isAdminAreaRequest, bool isSchudeldProccess) =>
                    (item.VisibleIndividually ||item.Type==ProductType.Grouped ) || isAdminAreaRequest || isSchudeldProccess
                , IsAdminAreaRequest, IsCurrentThreadScheduledItem);
        }

        private bool IsCurrentThreadScheduledItem()
        {
            return Thread.CurrentThread.IsBackground;
        }

        private bool IsAdminAreaRequest()
        {
            return HttpContext.Current != null && (HttpContext.Current.Request.Headers.AllKeys.Any(x => x == "X-Panel-Request") &&
                                                   IdentityManager.GetCurrentIdentity().IsAuthenticated &&
                                                   IdentityManager.HasPremission(IdentityManager.GetCurrentIdentity(), PanelPremission.Default));
        }
    }
}
