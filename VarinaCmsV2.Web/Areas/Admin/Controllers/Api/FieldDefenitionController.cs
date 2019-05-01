using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Core.Security.Premissions;
using VarinaCmsV2.Core.Web.Security.WebApi;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Web.Areas.Admin.Controllers.Api
{
    [WebApiCmsAuthorize(Premissions = PanelPremission.Default)]
    public class
        FieldDefenitionController : ApiController
    {
        private readonly IFieldDefentionFacade _fieldDefentionFacade;
        private readonly IFieldDataService _fieldDataService;
        private readonly IFieldDefenitionGroupDataService _fieldDefenitionGroupDataService;
        private readonly IUnitOfWork _unitOfWork;
        public FieldDefenitionController(IUnitOfWork unitOfWork, IFieldDataService fieldDataService, IFieldDefenitionGroupDataService fieldDefenitionGroupDataService, IFieldDefentionFacade fieldDefentionFacade)
        {
            _fieldDefentionFacade = fieldDefentionFacade;
            _fieldDataService = fieldDataService;
            _unitOfWork = unitOfWork;
            _fieldDefenitionGroupDataService = fieldDefenitionGroupDataService;
        }
        [HttpPost]
        [WebApiCmsAuthorize(Premissions = FieldDefenitionPremission.CanManage)]
        public async Task<IHttpActionResult> Post(FieldDefenitionAddOrUpdateModel model)
        {
            var fd = model.MapToModel();
            _fieldDefentionFacade.PendToAdd(fd);
            await _fieldDefentionFacade.SaveChangesAsync();
            return Ok(fd);
        }

        [Route("api/fielddefenition/postFdGroup")]
        [HttpPost]
        [WebApiCmsAuthorize(Premissions = FieldDefenitionPremission.CanManage)]
        public async Task<IHttpActionResult> PostFdGroup(FieldDefenitionGroupAddOrUpdateModel model)
        {
            var fieldDefenitionGroup = Mapper.Map<FieldDefenitionGroup>(model);
            await _fieldDefenitionGroupDataService.AddAsync(fieldDefenitionGroup);
            return Ok(fieldDefenitionGroup);
        }
        [WebApiCmsAuthorize(Premissions = FieldDefenitionPremission.CanManage)]
        [HttpPut]
        public async Task<IHttpActionResult> Put(FieldDefenitionAddOrUpdateModel model)
        {
            var fd = model.MapToModel();
            _fieldDefentionFacade.PendToUpdate(fd);
            await _fieldDefentionFacade.SaveChangesAsync();
            return Ok(fd);
        }
        [WebApiCmsAuthorize(Premissions = FieldDefenitionPremission.CanManage)]
        [HttpPut]
        [Route("api/fielddefenition/putbatch")]
        public async Task<IHttpActionResult> Put(List<FieldDefenitionAddOrUpdateModel> model)
        {
            var fds = model.MapToModel();
            _fieldDefentionFacade.PendToUpdate(fds);
            await _fieldDefentionFacade.SaveChangesAsync();
            return Ok(fds);
        }
        [HttpPut]
        [WebApiCmsAuthorize(Premissions = FieldDefenitionPremission.CanManage)]
        [Route("api/fielddefenition/updateFdGroup/{id}")]
        public async Task<IHttpActionResult> PutFdGroup(Guid id, FieldDefenitionGroupViewModel model)
        {
            if (id != model.Id) return BadRequest("id not presented correctly");
            var fdg = await _fieldDefenitionGroupDataService.GetAsync(id);
            if (fdg == null) return BadRequest("item not found to edit");
            Mapper.Map(model, fdg);
            await _fieldDefenitionGroupDataService.UpdateAsync(fdg);
            return Ok();
        }
        [WebApiCmsAuthorize(Premissions = FieldDefenitionPremission.CanManage)]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            if (ExistAnyFiledWithThisFdId(id))
                return BadRequest("موجودیت هایی با این فیلد تعریف شده وجود دارند حدف ممکن نیست .");
            _fieldDefentionFacade.PendToDelete(id);
            await _fieldDefentionFacade.SaveChangesAsync();
            return Ok();
        }

        [WebApiCmsAuthorize(Premissions = FieldDefenitionPremission.CanManage)]
        [Route("api/fielddefenition/deleteFdGroup/{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFdGroup(Guid id)
        {
            var fdg = _fieldDefenitionGroupDataService.Query.Include(x => x.FieldDefenitions)
                .FirstOrDefault(x => x.Id == id);

            if (fdg == null) return BadRequest("item not found. ");
            if (fdg.FieldDefenitions.Any())
            {
                if (ExistAnyFiledWithThisFdIds(fdg.FieldDefenitions.Select(x => x.Id))) return BadRequest("موجودیت هایی با این فیلد های تعریف شده وجود دارند امکان حذف موجود نیست.");
                fdg.FieldDefenitions.ToList().ForEach(_unitOfWork.Delete);
            }

            await _fieldDefenitionGroupDataService.DeleteAsync(id);

            return Ok();
        }

        private bool ExistAnyFiledWithThisFdIds(IEnumerable<Guid> ids)
        {
            return _fieldDataService.Query.Any(x => ids.Contains(x.FieldDefenitionId));
        }
        private bool ExistAnyFiledWithThisFdId(Guid id)
        {
            return _fieldDataService.Query.Any(x => x.FieldDefenitionId == id);
        }
    }
}

