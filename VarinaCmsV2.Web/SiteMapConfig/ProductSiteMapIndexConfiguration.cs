using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleMvcSitemap;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.DomainClasses.Entities.EShop;

namespace VarinaCmsV2.Web.SiteMapConfig
{
    public class ProductSiteMapIndexConfiguration : SitemapIndexConfiguration<Product>
    {
        private readonly ICmsUrlBuilderFactory _cmsUrlBuilder;

        public ProductSiteMapIndexConfiguration(IQueryable<Product> dataSource, int? currentPage, ICmsUrlBuilderFactory cmsUrlBuilder) : base(dataSource, currentPage)
        {
            _cmsUrlBuilder = cmsUrlBuilder;
        }

        public override SitemapIndexNode CreateSitemapIndexNode(int currentPage)
        {
            return new SitemapIndexNode(new UrlHelper(HttpContext.Current.Request.RequestContext).Action("Products", "SiteMap", new { pageNumber = currentPage }));
        }

        public override SitemapNode CreateNode(Product source)
        {
            return new SitemapNode(_cmsUrlBuilder.GetUrlBuilder(source).Generate(source)) { ChangeFrequency = ChangeFrequency.Weekly };
        }
    }
}