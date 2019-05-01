using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Services.UserManagement;
using VarinaCmsV2.DomainClasses.Users;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class UserQueryHelper
    {
        public static IQueryable<User> FilterByQuery(this IQueryable<User> userQuery, UserQuery query)
        {
            userQuery = FilterByIsBanned(userQuery, query);
            userQuery = FilterBySearchText(userQuery, query);
            userQuery = FilterByRole(userQuery, query);
            return userQuery;
        }

        private static IQueryable<User> FilterByRole(IQueryable<User> userQuery, UserQuery query)
        {
            return query.IsBanned ? userQuery.Where(x => x.IsBanned) : userQuery;
        }

        private static IQueryable<User> FilterBySearchText(IQueryable<User> userQuery, UserQuery query)
        {
            var searchText = query.SearchText;
            return string.IsNullOrEmpty(searchText)
                ? userQuery
                : userQuery
                    .Where(x => x.Name.Contains(searchText) ||
                                x.UserName.Contains(searchText) ||
                                x.Email.Contains(searchText) || x.PhoneNumber.Contains(searchText) ||
                                x.Url.Contains(searchText));
        }

        private static IQueryable<User> FilterByIsBanned(IQueryable<User> userQuery, UserQuery query)
        {
            return query.RoleIds.Count != 0
                ? userQuery.Where(x => x.Roles.Any(r => query.RoleIds.Contains(r.RoleId)))
                : userQuery;
        }
    }
}