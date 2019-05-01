using Owin;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Security.Contracts;

namespace VarinaCmsV2.Web
{
    public partial class Startup
    {
	    public void RegisterOwinDepemdencies(IAppBuilder app)
	    {
	        app.CreatePerOwinContext(Container.GetInstance<IEntityDataService>);
	        app.CreatePerOwinContext(Container.GetInstance<IEntitySchemeDataService>);
	        app.CreatePerOwinContext(Container.GetInstance<IAppUserManager>);
	    }
	}
}