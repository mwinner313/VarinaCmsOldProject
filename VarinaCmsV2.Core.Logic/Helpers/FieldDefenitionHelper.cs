using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Logic.Exceptions;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class FieldDefenitionHelper
    {
        public static List<FieldDefenition> MapToModel(this IEnumerable<FieldDefenitionViewModel> model)
        {
            return AutoMapper.Mapper.Map<List<FieldDefenition>>(model);
        }
        public static List<FieldDefenitionGroup> MapToModel(this IEnumerable<FieldDefenitionGroupAddOrUpdateModel> model)
        {
            return AutoMapper.Mapper.Map<List<FieldDefenitionGroup>>(model);
        }

        public static FieldDefenition MapToModel(this FieldDefenitionViewModel model)
        {
            return AutoMapper.Mapper.Map<FieldDefenition>(model);
        }
        public static FieldDefenitionViewModel MapToViewModel(this FieldDefenition model)
        {
            return AutoMapper.Mapper.Map<FieldDefenitionViewModel>(model);
        }

        public static FieldDefenitionLiquidVeiwModel MapToLiquidViewModel(this FieldDefenition model)
        {
            return AutoMapper.Mapper.Map<FieldDefenitionLiquidVeiwModel>(model);
        }

        public static List<FieldDefenitionLiquidVeiwModel> MapToLiquidViewModel(this List<FieldDefenition> model)
        {
            return AutoMapper.Mapper.Map<List<FieldDefenitionLiquidVeiwModel>>(model);
        }

        public static FieldDefenition MapToExisting(this FieldDefenitionViewModel viewModel,FieldDefenition model)
        {
            return AutoMapper.Mapper.Map(viewModel,  model);
        }
        public static List<FieldDefenition> MapToModel(this IEnumerable<FieldDefenitionAddOrUpdateModel> model)
        {
            return AutoMapper.Mapper.Map<List<FieldDefenition>>(model);
        }

        public static FieldDefenition MapToModel(this FieldDefenitionAddOrUpdateModel model)
        {
            return AutoMapper.Mapper.Map<FieldDefenition>(model);
        }
        public static void MapToModel(this FieldDefenitionAddOrUpdateModel model,FieldDefenition existing)
        {
            AutoMapper.Mapper.Map(model, existing);
        }

        public static FieldDefenition MapToExisting(this FieldDefenitionAddOrUpdateModel model,FieldDefenition fd)
        {
            return AutoMapper.Mapper.Map(model, fd);
        }



        public static void CheckFieldDefenition(this FieldDefenition model, CustomFieldFactoryProvider<JObject> customFieldFactoryProvider)
        {
            var fieldFactory = customFieldFactoryProvider.GetFieldFactory(model.Type);

            if (fieldFactory == null)
                throw new ArgumentNullException($"{nameof(fieldFactory)} => {model.Type}");

            if (model.DefaultValue!=null &&
                !fieldFactory.IsValidForField(model.DefaultValue,model.MetaData))
                throw new InValidFieldValueException(model, fieldFactory.GetType());
        }

        public static void SanitizeHandle(this FieldDefenition defenition)
        {
            defenition.Handle = defenition.Handle.Replace(" ", "");
        }
    }
    
}
