using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VarinaCmsV2.Core.Services;
using VarinaCmsV2.Core.Services.Entity;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class EntityQueryHelper
    {
        public static IQueryable<Entity> FilterByEntityQuery(this IQueryable<Entity> entityQuery, EntityQuery query)
        {
            if (query == null) return entityQuery;

            entityQuery = FilterByScheme(entityQuery, query);
            entityQuery = FilterByCategoryId(entityQuery, query);
            entityQuery = FilterBySearchText(entityQuery, query.SearchText);
            entityQuery = FilterByTagIds(entityQuery, query.TagIds);
            entityQuery = FilterByPublishState(entityQuery, query.PublishState);
            entityQuery = query.NotVisibles ? entityQuery.Where(x => !x.IsVisible) : entityQuery;
            return entityQuery;
        }

        private static IQueryable<Entity> FilterByPublishState(IQueryable<Entity> entityQuery, PublishState state)
        {
            switch (state)
            {
                case PublishState.NotPublished: return entityQuery.Where(x => x.PublishDateTime > DateTime.Now.AddMinutes(3));
                case PublishState.Published:
                    return entityQuery.Where(x => x.PublishDateTime < DateTime.Now.AddMinutes(3));
                default: return entityQuery;
            }
        }

        private static IQueryable<Entity> FilterByTagIds(IQueryable<Entity> entityQuery, List<Guid> tagIds)
        {
            return (tagIds != null && tagIds.Count != 0)
                ? entityQuery.Where(x => x.Tags.Any(t => tagIds.Contains(t.Id)))
                : entityQuery;
        }

        private static IQueryable<Entity> FilterByScheme(IQueryable<Entity> entityQuery, EntityQuery query)
        {
            return (string.IsNullOrEmpty(query.Scheme) && !query.SchemeId.HasValue) ? entityQuery :
             entityQuery.Where(x => x.SchemeId == query.SchemeId || x.Scheme.Handle == query.Scheme);
        }

        private static IQueryable<Entity> FilterByCategoryId(IQueryable<Entity> entityQuery, EntityQuery query)
        {
            entityQuery = query.PrimaryCategoryId == Guid.Empty ? entityQuery :
                entityQuery.Where(x => x.PrimaryCategoryId == query.PrimaryCategoryId);
            entityQuery = query.RelatedCategoryIds?.Count == null ? entityQuery :
                entityQuery.Where(x => x.RelatedCategories.Any(r => query.RelatedCategoryIds.Contains(r.Id)));
            entityQuery = query.IncludeChildCategories
                ? entityQuery.Where(x => x.PrimaryCategory.ParentId == query.PrimaryCategoryId)
                : entityQuery;
            
            return entityQuery;
        }

        private static IQueryable<Entity> FilterBySearchText(IQueryable<Entity> entityQuery, string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
                entityQuery = entityQuery.Where(x => x.Title.Contains(searchText) ||
                                                 x.Fields.Any(f => f.RawValueString.Contains(searchText)) ||
                                                 x.Description.Contains(searchText) ||
                                                 x.Tags.Any(t => t.Title.Contains(searchText)));
            return entityQuery;
        }


        public static IOrderedQueryable<Entity> ApplyEntityOrderBy(this IQueryable<Entity> entities, string order)
        {
            switch (order)
            {
                case "create_date_asc": return entities.OrderBy(x => x.CreateDateTime);
                case "create_date_desc": return entities.OrderByDescending(x => x.CreateDateTime);
                case "update_date_asc": return entities.OrderBy(x => x.UpdateDateTime);
                case "publish_date_asc": return entities.OrderBy(x => x.PublishDateTime);
                case "update_date_desc": return entities.OrderByDescending(x => x.UpdateDateTime);
                case "publish_date_desc": return entities.OrderByDescending(x => x.PublishDateTime);
                case "title_asc": return entities.OrderBy(x => x.Title);
                case "title_desc": return entities.OrderByDescending(x => x.CreateDateTime);
                case "visit_count_asc": return entities.OrderBy(x => x.VisitCount);
                case "like_count_asc": return entities.OrderBy(x => x.LikeCount);
                case "visit_count_desc": return entities.OrderByDescending(x => x.VisitCount);
                case "like_count_desc": return entities.OrderByDescending(x => x.LikeCount);
                default: return entities.OrderByDescending(x => x.Id);
            }
        }
        public static IQueryable<Entity> IncludeNecessaryData(this IQueryable<Entity> query)
        {
            return query
                .Include(x => x.PrimaryCategory)
                .Include(x => x.PrimaryCategory.Scheme)
                .Include(x => x.Fields)
                .Include(x => x.RelatedCategories)
                .Include(x => x.RelatedCategories.Select(c => c.Scheme))
                .Include(x => x.Creator)
                .Include(x => x.Tags)
                .Include(x => x.Scheme)
                .Include(x => x.Fields.Select(f => f.FieldDefenition));
        }
    }
}
