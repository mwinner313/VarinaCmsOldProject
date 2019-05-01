using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Mvc;
using StructureMap.Attributes;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Web.ActionFilters
{
    public class MvcEnableEntityValidationAttribute : ActionFilterAttribute
    {
        [SetterProperty]
        public  IEntitySchemeDataService EntitySchemesDataService { get; set; }
        private EntityViewModel Model { get; set; }
        private ModelStateDictionary ControllerModelState { get; set; }

        public MvcEnableEntityValidationAttribute()
        {
           // EntitySchemesDataService = DependencyResolver.Current.GetService<IEntitySchemeDataService>();
        }
      

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ControllerModelState = filterContext.Controller.ViewData.ModelState;
            foreach (var actionParametersValue in filterContext.ActionParameters.Values)
            {
                if (actionParametersValue is EntityViewModel)
                {
                    Model = actionParametersValue as EntityViewModel;
                    if (ExistsEntitySchemeFromCache(Model.SchemeId))
                    {
                        ApplyValidation();
                    }
                    else
                        throw new InvalidOperationException($"UnRecognized EntityScheme Recieved In Action{filterContext.ActionDescriptor.ActionName}" +
                                                            $" Controller : {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}");
                }
            }
        }

        private bool ExistsEntitySchemeFromCache(Guid modelEntitySchemeId)
        {
            return EntitySchemesDataService.FromCacheAsync().Result.Any(x => x.Id == Model.SchemeId);
        }

        private void ApplyValidation()
        {

        }
    }
}
