using System.Web.Mvc;
using VarinaCmsV2.Core.Web;

namespace VarinaCmsV2.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            
            context.MapRoute(
                name: Routes.Panel,
                url: "panel/{*.}",
                defaults: new { controller = "Panel", action = "Index" ,area = "Admin" }
            );
        
        }
    }
}