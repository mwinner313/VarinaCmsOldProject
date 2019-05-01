using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleMvcSitemap;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Web.SiteMapConfig
{
    public class EntityTagSiteMapIndexConfiguration : SitemapIndexConfiguration<Tag>
    {
        private readonly ICmsUrlBuilderFactory _cmsUrlBuilder;
        private readonly string _schemeUrl;
        public EntityTagSiteMapIndexConfiguration(IQueryable<Tag> dataSource, int? currentPage, ICmsUrlBuilderFactory urlBuilder, string schemeUrl) : base(dataSource, currentPage)
        {
            _cmsUrlBuilder = urlBuilder;
            _schemeUrl = schemeUrl;
        }

        public override SitemapIndexNode CreateSitemapIndexNode(int currentPage)
        {
            return new SitemapIndexNode(new UrlHelper(HttpContext.Current.Request.RequestContext).Action("EntiryTags", "SiteMap", new { schemeUrl = _schemeUrl, pageNumber = currentPage }));
        }

        public override SitemapNode CreateNode(Tag source)
        {
           return new SitemapNode(_cmsUrlBuilder.GetUrlBuilder(source).Generate(source));
        }
    }
}