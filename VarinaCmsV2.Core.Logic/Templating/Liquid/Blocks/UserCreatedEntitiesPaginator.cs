using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotLiquid;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.DTO;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.Core.Logic.Templating.Liquid.Blocks
{
    public class UserCreatedEntitiesPaginator : Block
    {
        private readonly IEntityDataService _entityData;
        private readonly IEntitySchemeDataService _schemeData;
        private string _userKey = string.Empty;
        private string _pageNumber = "1";
        private string _schemeHandle = string.Empty;

        public UserCreatedEntitiesPaginator(IEntityDataService entityData, IEntitySchemeDataService schemeData)
        {
            _entityData = entityData;
            _schemeData = schemeData;
        }

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            var keys = markup.Split(' ').Where(x=>!string.IsNullOrEmpty(x)).ToArray();
            try
            {
                _userKey = keys[0];
                _schemeHandle = keys[1];
                if (keys.Length == 3)
                    _pageNumber = keys[2];
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"user created entities paginator {markup}");
            }
            base.Initialize(tagName, markup, tokens);
        }

        public override void Render(Context context, TextWriter result)
        {
            var user = context[_userKey] as UserLiquidViewModel;
            var scheme = _schemeData.Query.First(x => x.Handle == _schemeHandle).AsLiquidAdapted();
            var pageNumber = GetPageNumber(context);
            var entities = CreateQuery(user, scheme).PaginateByConfig(pageNumber).ToList().AsLiquidAdapted();
            var allPagesCount = CreateQuery(user, scheme)
                .GetAllPagesCount(FrontEndOptions.FrontEndDeveloperOptions.Instance.Pagination.Default);


            context["entiry"] = new UserEntitiesPaginatedLiquidAdapted()
            {
                Entities = entities,
                AllPagesCount = allPagesCount,
                CurrentPage = pageNumber,
                Scheme = scheme,
                User = user
            };


            base.Render(context, result);
        }

        private int GetPageNumber(Context context)
        {
            if (int.TryParse(context[_pageNumber].ToString(), out var number))
            {
                return number;
            }
            if (int.TryParse(_pageNumber, out number))
            {
                return number;
            }
            return 1;
        }

        private IQueryable<Entity> CreateQuery(UserLiquidViewModel user, EntitySchemeLiquidViewModel scheme)
        {
            return _entityData.Query.Where(x => x.CreatorId == user.Id && x.SchemeId == scheme.Id).IncludeNecessaryData().OrderByDescending(x=>x.PublishDateTime);
        }
    }
}
