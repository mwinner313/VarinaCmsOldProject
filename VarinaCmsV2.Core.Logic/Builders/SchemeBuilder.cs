using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.Builders;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Builders
{
    public class SchemeBuilder <TScheme> : ISchemeBuilder<TScheme> where TScheme :IScheme
    {
        private readonly CustomFieldFactoryProvider<JObject> _customFieldFactoryProvider;
        private  TScheme _scheme;
        public IUrlBuilder UrlBuilder { get; set; }
        public void SetBuildingScheme(TScheme scheme)
        {
            CheckIsNotNullScheme(scheme);
            _scheme = scheme;
            CleanFieldDefenitions();
            CleanFieldDefenitionGroups();
            GenerateUniqueUrl();
            SanitizeSchemeHandle();
        }

        private void CleanFieldDefenitionGroups()
        {
            if (_scheme.FieldDefenitionGroups != null)
                _scheme.FieldDefenitionGroups.Clear();
            else
            {
                _scheme.FieldDefenitionGroups = new List<FieldDefenitionGroup>();
            }
        }

        private void SanitizeSchemeHandle()
        {
            _scheme.Handle = _scheme.Handle.Replace(" ","");
        }

        private void CheckIsNotNullScheme(TScheme scheme)
        {
            if (scheme == null)
                throw new ArgumentNullException(nameof(scheme));
        }

        private void GenerateUniqueUrl()
        {
            if (string.IsNullOrEmpty(_scheme.Url))
                throw new ArgumentNullException($"null urlHandle for scheme{_scheme.Title}");
            _scheme.Url = UrlBuilder.GenrateUrlSegment(_scheme.Url);
        }

        private void CleanFieldDefenitions()
        {
            if (_scheme.FieldDefenitions != null)
                _scheme.FieldDefenitions.Clear();
            else
            {
                _scheme.FieldDefenitions = new List<FieldDefenition>();
            }
        }

        public void AddFieldDefenition(FieldDefenition model)
        {
            model.CheckFieldDefenition(_customFieldFactoryProvider);
            model.SanitizeHandle();
            _scheme.FieldDefenitions.Add(model);
        }

        public void AddFieldDefenition(List<FieldDefenition> model)
        {
             model.ForEach(AddFieldDefenition);
        }
     
        public TScheme GetResult()
        {
            return _scheme;
        }

        public void AddFieldDefenitionGroup(List<FieldDefenitionGroup> fieldDefenitionGroups)
        {
            fieldDefenitionGroups.ForEach(x =>
            {
                foreach (var fieldDefenition in x.FieldDefenitions)
                {
                    fieldDefenition.CheckFieldDefenition(_customFieldFactoryProvider);
                    fieldDefenition.SanitizeHandle();
                }
                _scheme.FieldDefenitionGroups.Add(x);
            });
        }

        public SchemeBuilder(CustomFieldFactoryProvider<JObject> customFieldFactoryProvider ,UrlBuilder urlBuilder)
        {
            _customFieldFactoryProvider = customFieldFactoryProvider;
            UrlBuilder = urlBuilder;
        }
    }
}
