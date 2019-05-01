using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using DotLiquid.Exceptions;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.Logic.Web.UrlBuilding;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Category;
using VarinaCmsV2.ViewModel.DTO;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Tag;
using VarinaCmsV2.ViewModel.User.Account;
using ArgumentException = DotLiquid.Exceptions.ArgumentException;

namespace VarinaCmsV2.Core.Logic.Templating.Liquid.Blocks
{
    public class PaginationLinkBuilder : DotLiquid.Tag
    {
        private string _item = string.Empty;
        private string _pageNumber = string.Empty;
        private string _selectedScheme = string.Empty;
        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            try
            {
                var parameters = markup.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                _item = parameters[0];
                _pageNumber = parameters[1];
                if (parameters.Length == 3)
                    _selectedScheme = parameters[2];
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"error while reading pagination parameters => {markup}");
            }

            base.Initialize(tagName, markup, tokens);
        }

        public override void Render(Context context, TextWriter result)
        {
            var item = context[_item];
            int page = GetPageNumber(context);

            if (page is 0) return;

            if (item is CategoryLiquidViewModel cat)
                result.Write(new CategoryUrlBuilder().Generate(AutoMapper.Mapper.Map<Category>(cat), page));

            if (item is EntityTagLiquidViewModel tag)
                result.Write(new EntityTagUrlBuilder().Generate(AutoMapper.Mapper.Map<DomainClasses.Entities.Tag>(tag), page));

            if (item is UserEntitiesPaginatedLiquidAdapted userCreateds)
                result.Write(new UserCreatedEntityUrlBuilder().Generate(userCreateds.MapToModel(),page));
            if(item is ProductCategoryLiquidVeiwModel pCat) result.Write(new ProductCategoryUrlBuilder().Generate(new ProductCategory()
            {
                Url = pCat.Url
            },page));

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
            throw new ArgumentException("page_number is invalid");
        }
    }
}
