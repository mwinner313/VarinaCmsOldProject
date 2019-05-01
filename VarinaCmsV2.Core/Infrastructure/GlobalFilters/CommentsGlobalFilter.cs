using System.Data.Entity;
using System.Linq;
using System.Web;
using EntityFramework.DynamicFilters;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Core.Infrastructure.GlobalFilters
{
    public class CommentsGlobalFilter : GlobalFilter
    {
        [SetterProperty]
        public IIdentityManager IdentityManager { get; set; }
        public override void ApplyFilter(DbModelBuilder modelBuilder)
        {
            modelBuilder.Filter("Comment_Approve", (Comment cmt, bool isAdminAreaRequest) => cmt.Approved || isAdminAreaRequest, IsAdminAreaRequest);
        }
        private bool IsAdminAreaRequest()
        {
            return HttpContext.Current != null && (HttpContext.Current.Request.Headers.AllKeys.Any(x => x == "X-Panel-Request") &&
                                                   IdentityManager.GetCurrentIdentity().IsAuthenticated &&
                                                   IdentityManager.HasPremission(IdentityManager.GetCurrentIdentity(), PanelPremission.Default));
        }
    }
}
