using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Builders;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Exceptions;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Builders
{
    public class GenericIEntityBuilder<T,TCategory> : IEntityBuilder<T,TCategory> 
        where TCategory :ICategory
        where T :IEntity<TCategory>
    {
        private readonly CustomFieldFactoryProvider<JObject> _customFieldFactoryProvider;
        private T _entity;
        private readonly IUrlBuilder _urlBuilder;

        private FieldDefenition WorkingFieldDefention { get; set; }

        public GenericIEntityBuilder(CustomFieldFactoryProvider<JObject> customFieldFactoryProvider, IUrlBuilder urlBuilder)
        {
            _customFieldFactoryProvider = customFieldFactoryProvider;
            _urlBuilder = urlBuilder;
        }

        public void SetBuildingEntity(T model, EntityScheme scheme, TCategory primaryCat)
        {
            _entity = model;
            _entity.Url = _urlBuilder.GenrateUrlSegment(_entity.Url ?? _entity.Title);
            _entity.Scheme = scheme;
            _entity.PrimaryCategory = primaryCat;
        }

        public T GetResult()
        {
            _entity.Fields.ToList().ForEach(CheckFieldValue);
            CheckAllNecessaryFieldsAdded();
            HandleUrlAndHandle();
            
            return _entity;
        }

        private void HandleUrlAndHandle()
        {
            _entity.Url = string.IsNullOrEmpty(_entity.Url)
                ? _urlBuilder.GenrateUrlSegment(_entity.Title)
                : _urlBuilder.GenrateUrlSegment(_entity.Url);

            _entity.Handle = string.IsNullOrEmpty(_entity.Handle)
             ? _urlBuilder.GenrateUrlSegment(_entity.Title)
             : _urlBuilder.GenrateUrlSegment(_entity.Handle);
        }

        private void CheckFieldValue(Field field)
        {
            SetWorkingFieldDefenition(field.FieldDefenitionId);
            if (IsFieldValueNull(field) && HasDefualutValue(field))
            {
                FillWithDefValue(field);
                return;
            }
            if (IsFieldValueNull(field) && !IsFieldRequired(field)) return;

            CheckFieldValueValidation(field);
        }

        private void CheckFieldValueValidation(Field field)
        {
            var factory = _customFieldFactoryProvider.GetFieldFactory(WorkingFieldDefention.Type);
            if (field.RawValue != null && !factory.IsValidForField(field.RawValue, WorkingFieldDefention.MetaData))
                throw new InValidFieldValueException(field.FieldDefenition, factory.GetType());
        }

        private void CheckAllNecessaryFieldsAdded()
        {
            if (_entity.Scheme == null)
                throw new MisingEntityScheme();

            foreach (var defId in _entity.Scheme.FieldDefenitions.Select(x => x.Id))
            {
                var def = _entity.Scheme.FieldDefenitions.First(x => x.Id == defId);

                if (!_entity.Fields.Select(x => x.FieldDefenitionId).Contains(defId) && def.IsRequired)
                    throw new MisingEntityField(def);
            }
        }

        private bool IsFieldValueNull(Field field)
        {
            return field.RawValue == null
                   || field.RawValue.ToString() == "{{}}"
                   || field.RawValue.ToString() == "{}"
                   || field.RawValue.ToString() == "";
        }

        private void FillWithDefValue(Field field)
        {
            field.RawValue = WorkingFieldDefention.DefaultValue;
        }

        private bool HasDefualutValue(Field field)
        {
            return WorkingFieldDefention.DefaultValue != null;
        }

        private bool IsFieldRequired(Field field)
        {
            return WorkingFieldDefention.IsRequired;
        }

        private void SetWorkingFieldDefenition(Guid id)
        {
            WorkingFieldDefention = _entity.Scheme.FieldDefenitions.FirstOrDefault(x => x.Id == id) ??
                                    _entity.PrimaryCategory.FieldDefenitions.FirstOrDefault(x => x.Id == id)??
                                    _entity.Scheme.FieldDefenitionGroups.SelectMany(x=>x.FieldDefenitions).FirstOrDefault(x=>x.Id==id)??
                                    _entity.PrimaryCategory.FieldDefenitionGroups.SelectMany(x=>x.FieldDefenitions).FirstOrDefault(x=>x.Id==id);
        }
    }
}