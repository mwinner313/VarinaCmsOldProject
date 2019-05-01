using System.Linq;
using System.Web;
using SimpleMvcSitemap;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.DomainClasses.Entities;
using UrlHelper = System.Web.Mvc.UrlHelper;

namespace VarinaCmsV2.Web.SiteMapConfig
{
    public class EntitySiteMapIndexConfiguration : SitemapIndexConfiguration<Entity>

    {
        private readonly ICmsUrlBuilderFactory _cmsUrlBuilder;
        private readonly string _schemeUrl;
        public EntitySiteMapIndexConfiguration(IQueryable<Entity> dataSource, int? currentPage, ICmsUrlBuilderFactory cmsUrlBuilder, string schemeUrl) : base(dataSource, currentPage)
        {
            _cmsUrlBuilder = cmsUrlBuilder;
            _schemeUrl = schemeUrl;
        }

        public override SitemapIndexNode CreateSitemapIndexNode(int currentPage)
        {
            return new SitemapIndexNode(new UrlHelper(HttpContext.Current.Request.RequestContext).Action("Entiry", "SiteMap", new { schemeUrl = _schemeUrl, pageNumber = currentPage }));
        }

        public override SitemapNode CreateNode(Entity source)
        {
            return new SitemapNode(_cmsUrlBuilder.GetUrlBuilder(source).Generate(source)) { LastModificationDate = source.UpdateDateTime };
        }
    }
}