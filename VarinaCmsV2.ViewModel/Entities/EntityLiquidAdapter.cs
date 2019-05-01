using System;
using System.Collections.Generic;
using System.Linq;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Common.CustomFields;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Comment;
using VarinaCmsV2.ViewModel.Tag;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.ViewModel.Entities
{
    [LiquidType("Title", "Description", "PublishDateTime", "CreateDateTime")]
    public class EntityLiquidAdapter : UrlableLiquidAdapter
    {

        #region Props
        public string Title { get; set; }
        public string Description { get; set; }
        public EntityScheme Scheme { get; set; }
        public Guid Id { get; set; }
        public int LikeCount { get; set; }
        public int VisitCount { get; set; }
        public LiquidDateTime PublishDateTime { get; set; }
        public LiquidDateTime UpdateDateTime { get; set; }
        #endregion

        #region Navigation
        public Entity Entity { get; set; }
        public List<EntityTagLiquidViewModel> Tags { get; set; }
        public List<EntityCatLiquid> RelatedCategories { get; set; }
        public EntityCatLiquid PrimaryCategory { get; set; }
        public UserLiquidViewModel Creator { get; set; }
        public List<CommentLiquidViewModel> Comments { get; set; }
        public List<DomainClasses.Entities.Meta> Metas { get; set; }
        #endregion

        public CustomFieldFactoryProvider<JObject> FieldFactoryProvider { set;  get; }
        public override object BeforeMethod(string method)
        {
            var field = GetField(method);
            if (field != null) return field.ToLiquid();

            //if (MetaAdapter.HasMeta(Metas, method)) return MetaAdapter.AdaptAsLiquid(Metas, method);

            return base.BeforeMethod(method);
        }
        private CustomField<JObject> GetField(string method)
        {
            var rawField = Entity.Fields?.FirstOrDefault(x => x.FieldDefenition.Handle == method);
            if (rawField == null) return null;

            var fieldFactory = FieldFactoryProvider.GetFieldFactory(rawField.FieldDefenition.Type);
            var field = fieldFactory.LoadField(rawField.RawValue, rawField.FieldDefenition.Title,rawField.FieldDefenition.MetaData);
            return field;
        }
    }
}
