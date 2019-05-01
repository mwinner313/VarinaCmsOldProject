using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.Core.Contracts;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Security;
using VarinaCmsV2.Core.Infrastructure;
using VarinaCmsV2.Core.Logic.CustomFields;
using VarinaCmsV2.Core.Logic.CustomFields.Fields;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Form;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.FileManager;
using VarinaCmsV2.FileManager.Files;

namespace VarinaCmsV2.Core.Logic.Services.Form
{
    public class FormService : BaseService<DomainClasses.Entities.Form, FormSubmitResponse, FormListReponse>,
        IFormService
    {
        private readonly IFormDataService _formDataSrv;
        private readonly IFormSchemeDataService _formSchemeDataService;
        private readonly CustomFieldFactoryProvider<JObject> _fieldFactoryPrvider;
        private readonly IFileManager _fileManager;
        private readonly string _formsBaseFolderPath;

        public FormService(List<IBaseBeforeAddingEntityLogic> baseBeforeAddingEntityLogics,
            List<BaseAfterAddingEntityLogic> baseAfterAddingEntityLogics,
            List<IBaseAfterUpdatingEntityLogic> baseAfterUpdateEntityLogics,
            List<IBaseBeforeUpdatingEntityLogic> baseBeforeUpdateEntityLogics,
            List<IBaseBeforeDeleteEntityLogic> baseBeforeDeleteEntityLogics,
            List<BaseAfterDeleteEntityLogic> baseAfterDeleteEntityLogics, IIdentityManager identityManager,
            IRestrictedItemAccessManager accessManager, IFormDataService dataSrv,
            IFormSchemeDataService formSchemeDataService, CustomFieldFactoryProvider<JObject> fieldFactoryPrvider,
            IFileManager fileManager)
            : base(
                baseBeforeAddingEntityLogics, baseAfterAddingEntityLogics, baseAfterUpdateEntityLogics,
                baseBeforeUpdateEntityLogics, baseBeforeDeleteEntityLogics, baseAfterDeleteEntityLogics, identityManager,
                accessManager, dataSrv)
        {
            _formDataSrv = dataSrv;
            _formSchemeDataService = formSchemeDataService;
            _fieldFactoryPrvider = fieldFactoryPrvider;
            _fileManager = fileManager;
            _formsBaseFolderPath = AppDomain.CurrentDomain.BaseDirectory + SettingHelper.UploadsBasePath + "Forms";
        }

        public async Task<FormSubmitResponse> SubmitNewForm(FormSubmitRequest request)
        {
            var formScheme =
                await _formSchemeDataService
                    .Query
                    .Include(x => x.FieldDefenitions)
                    .FirstAsync(x => x.Handle == request.FormSchemeHandle);

            if (!HasRequestRequiredFileds(formScheme, request) || !AreAllFieldsValid(formScheme, request))
            {
                return new FormSubmitResponse()
                {
                    Access = ResponseAccess.BadRequest,
                    Message = "mising or invalid filed value Use CmsFormValidationFilterAttribute To See Details"
                };
            }
            await HandleRequest(formScheme, request);
            return new FormSubmitResponse()
            {
                Access =
                    ResponseAccess.Granted
            };
        }

        private async Task HandleRequest(DomainClasses.Entities.FormScheme formScheme, FormSubmitRequest request)
        {
            var form = new DomainClasses.Entities.Form() { Fields = new List<Field>(), FormSchemeId = formScheme.Id };
            SaveDataFields(formScheme, request, form);
            SaveFileFields(formScheme, request, form);
            await BaseBeforeAddAsync(form, request.RequestOwner);
            await _formDataSrv.AddAsync(form);
            await BaseAfterAddAsync(form, request.RequestOwner);
        }

        private void SaveFileFields(DomainClasses.Entities.FormScheme formScheme, FormSubmitRequest request,
            DomainClasses.Entities.Form form)
        {
            foreach (string prop in request.Files)
            {
                var file = request.Files[prop];
                var fd = formScheme.FieldDefenitions.FirstOrDefault(x => x.Handle == prop);
                if (fd == null) continue;
                var fileSaveRes = _fileManager.Save(file,
                    _formsBaseFolderPath + $"/{formScheme.Handle}/" + file.FileName);
                var data = new FileFieldDataWrapper(file, fileSaveRes.SavedFilePath, fileSaveRes.SavedFileName);

                form.Fields.Add(new Field()
                {
                    FieldDefenitionId = fd.Id,
                    RawValue = _fieldFactoryPrvider.GetFieldFactory(fd.Type).CreateNew(fd.Title, data, fd.MetaData).Value ?? fd.DefaultValue,
                });
            }
        }

        private void SaveDataFields(DomainClasses.Entities.FormScheme formScheme, FormSubmitRequest request,
            DomainClasses.Entities.Form form)
        {
            foreach (string prop in request.Form)
            {
                var fd = formScheme.FieldDefenitions.FirstOrDefault(x => x.Handle == prop);
                if (fd == null) continue;
                form.Fields.Add(new Field()
                {
                    FieldDefenitionId = fd.Id,
                    RawValue =
                        _fieldFactoryPrvider.GetFieldFactory(fd.Type).CreateNew(fd.Title, request.Form[prop], fd.MetaData).Value ?? fd.DefaultValue,
                });
            }
        }

        private bool AreAllFieldsValid(DomainClasses.Entities.FormScheme formScheme, FormSubmitRequest request)
        {
            return AreAllFormFieldsValid(formScheme, request) && AreAllFileFieldsValid(formScheme, request);
        }

        private bool AreAllFileFieldsValid(DomainClasses.Entities.FormScheme formScheme, FormSubmitRequest request)
        {
            bool @is = true;
            foreach (string prop in request.Files)
            {
                var fd = formScheme.FieldDefenitions.FirstOrDefault(x => x.Handle == prop);
                if (fd == null) continue;
                if (
                    !_fieldFactoryPrvider.GetFieldFactory(fd.Type)
                        .IsValidForField(new FileFieldDataWrapper(request.Files[prop]),fd.MetaData))
                {
                    @is = false;
                }
            }
            return @is;
        }

        private bool AreAllFormFieldsValid(DomainClasses.Entities.FormScheme formScheme, FormSubmitRequest request)
        {
            bool @is = true;
            foreach (string prop in request.Form)
            {
                var fd = formScheme.FieldDefenitions.FirstOrDefault(x => x.Handle == prop);
                if (fd == null) continue;
                if (!_fieldFactoryPrvider.GetFieldFactory(fd.Type).IsValidForField(request.Form[prop], fd.MetaData))
                {
                    @is = false;
                }
            }
            return @is;
        }

        private bool HasRequestRequiredFileds(DomainClasses.Entities.FormScheme formScheme, FormSubmitRequest request)
        {
            var requiredProps = formScheme.FieldDefenitions.Where(x => x.IsRequired).Select(x => x.Handle);
            var requestProps = request.Form.AllKeys.ToList();
            requestProps.AddRange(request.Files.AllKeys);
            var @is = true;
            foreach (var prop in requiredProps)
            {
                if (!requestProps.Contains(prop)) @is = false;
            }
            return @is;
        }

        public async Task<FormListReponse> Get(FormListRequest request)
        {
            var forms =
                _formDataSrv.Query.Include(x => x.Fields)
                    .Include(x => x.Fields.Select(f => f.FieldDefenition))
                    .Include(x => x.Creator)
                    .OrderByDescending(x => x.CreateDateTime)
                    .Filter(request.Query);

            return new FormListReponse()
            {
                Models =
                    await forms.Paginate(request.Query)
                        .ToViewModelListAsync(),
                Access = ResponseAccess.Granted,
                Count = forms.LongCount()
            };
        }

        public async Task<SimpleResonse> ChangeIsSeenState(FormChangeIsSeenStateRequest request)
        {
            var form = _formDataSrv.Query.First(x => x.Id == request.FormId);
            form.IsSeen = !form.IsSeen;
            await _formDataSrv.UpdateAsync(form);
            return new SimpleResonse() { Access = ResponseAccess.Granted };
        }
    }
}