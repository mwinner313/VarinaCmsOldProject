using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json.Linq;
using StructureMap.Attributes;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Web.ActionFilters
{

    public class WebApiEnableEntityValidationAttribute : ActionFilterAttribute
    {
        [SetterProperty]
        public IEntitySchemeDataService EntitySchemesDataService { get; set; }
        [SetterProperty]
        public CustomFieldFactoryProvider<JObject> FieldFactoryProvider { get; set; }

        private EntityAddOrUpdateViewModel Model { get; set; }


        private ModelStateDictionary ControllerModelState { get; set; }

        public WebApiEnableEntityValidationAttribute()
        {
        }


        public override void OnActionExecuting(HttpActionContext actionContext)
        {
          
        }

        //public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        //{
        //    ControllerModelState = actionContext.ModelState;
        //    foreach (var actionParametersValue in actionContext.ActionArguments.Values)
        //    {
        //        if (actionParametersValue is EntityAddOrUpdateViewModel)
        //        {
        //            Model = actionParametersValue as EntityAddOrUpdateViewModel;
        //            if ( await ExistsEntitySchemeFromCacheAsync(Model.SchemeId))
        //            {
        //                ApplyValidation();
        //            }
        //            else
        //                throw new InvalidOperationException($"UnRecognized EntityScheme Recieved In Action{actionContext.ActionDescriptor.ActionName}" +
        //                                                    $" Controller : {actionContext.ActionDescriptor.ControllerDescriptor.ControllerName}");
        //        }
        //    }
        // await   base.OnActionExecutingAsync(actionContext, cancellationToken);
        //}

        private async Task< bool> ExistsEntitySchemeFromCacheAsync(Guid modelEntitySchemeId)
        {
           return await EntitySchemesDataService.Query.AnyAsync(x => x.Id == Model.SchemeId); ;
        }

        //private void ApplyValidation()
        //{
        //    foreach (var field in Model.Fields)
        //    {
        //        var fieledFactory = FieldFactoryProvider.GetFieldFactory(field.FieldDefenitionType);
        //        if(!fieledFactory.IsValidForField(field.RawValue))
        //            throw new InvalidOperationException($"invalid data for field:{field.FieldDefenitionType} -- {field.FieldDefenitionTitle} -- {field.RawValue}");

        //    }
        //}
    }

}
