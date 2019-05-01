using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EntityFramework.DynamicFilters;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.GlobalFilters
{
    public class NotApprovedReviewGlobalFilter : GlobalFilter
    {
        [SetterProperty]
        public IIdentityManager IdentityManager { get; set; }

        private bool IsAdminAreaRequest()
        {
            return HttpContext.Current != null && (HttpContext.Current.Request.Headers.AllKeys.Any(x => x == "X-Panel-Request") &&
                                                   IdentityManager.GetCurrentIdentity().IsAuthenticated &&
                                                   IdentityManager.HasPremission(IdentityManager.GetCurrentIdentity(), PanelPremission.Default));
        }

        public override void ApplyFilter(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("ProductReview_Approve", (ProductReview review, bool isAdminAreaRequest) => review.IsApproved || isAdminAreaRequest, IsAdminAreaRequest);
        }
    }
}
