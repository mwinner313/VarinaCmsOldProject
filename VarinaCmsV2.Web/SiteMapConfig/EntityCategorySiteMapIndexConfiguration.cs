using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleMvcSitemap;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Web.SiteMapConfig
{
    public class EntityCategorySiteMapIndexConfiguration : SitemapIndexConfiguration<Category>
    {
        private readonly ICmsUrlBuilderFactory _cmsUrlBuilder;
        private readonly string _schemeUrl;
        public EntityCategorySiteMapIndexConfiguration(IQueryable<Category> dataSource, int? currentPage, ICmsUrlBuilderFactory cmsUrlBuilder, string schemeUrl) : base(dataSource, currentPage)
        {
            _cmsUrlBuilder = cmsUrlBuilder;
            _schemeUrl = schemeUrl;
        }

        public override SitemapIndexNode CreateSitemapIndexNode(int currentPage)
        {
            return new SitemapIndexNode(new UrlHelper(HttpContext.Current.Request.RequestContext).Action("EntiryCategory", "SiteMap", new { schemeUrl = _schemeUrl, pageNumber = currentPage }));
        }

        public override SitemapNode CreateNode(Category source)
        {
            return new SitemapNode(_cmsUrlBuilder.GetUrlBuilder(source).Generate(source)) { ChangeFrequency = ChangeFrequency.Weekly };
        }
    }
}