using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Services.Form;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class FormQueryHelper
    {
        public static IQueryable<Form> Filter(this IQueryable<Form> formsQuery, FormQuery query)
        {
            formsQuery = FilterByScheme(formsQuery, query);
            formsQuery = FilterBySearchText(formsQuery, query);
            formsQuery = FilterByNotSeen(formsQuery, query);

            return formsQuery;
        }

        private static IQueryable<Form> FilterByNotSeen(IQueryable<Form> formsQuery, FormQuery query)
        {
            return query.NotSeen ? formsQuery.Where(x => !x.IsSeen).Include(x=>x.FormScheme) : formsQuery;
        }

        private static IQueryable<Form> FilterBySearchText(IQueryable<Form> formsQuery, FormQuery query)
        {
            return string.IsNullOrEmpty(query.SearchText)
                ? formsQuery
                : formsQuery.Where(x => x.Fields.Any(f => f.RawValueString.Contains(query.SearchText)));
        }

        private static IQueryable<Form> FilterByScheme(IQueryable<Form> formsQuery, FormQuery query)
        {
            return string.IsNullOrEmpty(query.FormSchemeHandle)
                ? formsQuery
                : formsQuery.Where(x => x.FormScheme.Handle==query.FormSchemeHandle);
        }
    }
}