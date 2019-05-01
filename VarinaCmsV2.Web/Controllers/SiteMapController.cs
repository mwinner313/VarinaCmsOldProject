using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SimpleMvcSitemap;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.Core.Web;
using VarinaCmsV2.Web.SiteMapConfig;

namespace VarinaCmsV2.Web.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("SiteMap")]
    public class SiteMapController : Controller
    {
        private readonly IEntitySchemeDataService _schemes;
        private readonly IPageDataService _pages;
        private readonly ITagDataService _tags;
        private readonly ICategoryDataService _categories;
        private readonly ICmsUrlBuilderFactory _cmsUrlBuilder;
        private readonly IEntityDataService _entities;
        private readonly IProductDataService _productDataService;
        public SiteMapController(IProductDataService productDataService,ITagDataService tags,ICategoryDataService categories, IEntityDataService entities, IEntitySchemeDataService schemes, IPageDataService pages, ICmsUrlBuilderFactory cmsUrlBuilder)
        {
            _categories = categories;
            _entities = entities;
            _cmsUrlBuilder = cmsUrlBuilder;
            _schemes = schemes;
            _pages = pages;
            _tags = tags;
            _productDataService = productDataService;
        }
        [Route("")]
        public ActionResult Index()
        {
            List<SitemapIndexNode> sitemapIndexNodes = new List<SitemapIndexNode>();

            _schemes.Query.ToList()?.ForEach(x =>
                {
                    sitemapIndexNodes.Add(new SitemapIndexNode(Url.Action("Entiry", new
                    {
                        schemeUrl = x.Url
                    })));
                }
            );

            _schemes.Query.ToList()?.ForEach(x =>
                {
                    sitemapIndexNodes.Add(new SitemapIndexNode(Url.Action("EntiryTags", new
                    {
                        schemeUrl = x.Url
                    })));
                }
            );

            _schemes.Query.ToList()?.ForEach(x =>
                {
                    sitemapIndexNodes.Add(new SitemapIndexNode(Url.Action("EntiryCategory", new
                    {
                        schemeUrl = x.Url
                    })));
                }
            );

            sitemapIndexNodes.Add(new SitemapIndexNode((Url.Action("Products"))));

            sitemapIndexNodes.Add(new SitemapIndexNode(Url.Action("Pages")));

            return new SitemapProvider().CreateSitemapIndex(new SitemapIndexModel(sitemapIndexNodes));
        }

        [Route("Entiry/{schemeUrl}/{pageNumber:int?}")]
        public ActionResult Entiry(string schemeUrl, int? pageNumber)
        {
            var dataSource = _entities.Query.Include(x => x.Scheme).Where(x => x.Scheme.Url == schemeUrl);
            var entitySitemapIndexConfiguration = new EntitySiteMapIndexConfiguration(dataSource, pageNumber, _cmsUrlBuilder, schemeUrl);
            return new DynamicSitemapIndexProvider().CreateSitemapIndex(new SitemapProvider(), entitySitemapIndexConfiguration);
        }
        [Route("Pages")]
        public ActionResult Pages()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();
            _pages.Query.ToList()?.ForEach(x => nodes.Add(new SitemapNode(_cmsUrlBuilder.GetUrlBuilder(x).Generate(x))
            { LastModificationDate = x.UpdateDateTime }));
            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }

        [Route("EntiryTags/{schemeUrl}/{pageNumber:int?}")]
        public ActionResult EntiryTags(string schemeUrl, int? pageNumber)
        {
            var dataSource = _tags.Query.Where(x=>x.SchemeId.HasValue).Include(x => x.Scheme).Where(x => x.Scheme.Url == schemeUrl);
            var entitySitemapIndexConfiguration = new EntityTagSiteMapIndexConfiguration(dataSource, pageNumber, _cmsUrlBuilder, schemeUrl);
            return new DynamicSitemapIndexProvider().CreateSitemapIndex(new SitemapProvider(), entitySitemapIndexConfiguration);
        }

        [Route("EntiryCategory/{schemeUrl}/{pageNumber:int?}")]
        public ActionResult EntiryCategory(string schemeUrl,int? pageNumber)
        {
            var dataSource = _categories.Query.Where(x => x.SchemeId.HasValue).Include(x => x.Scheme).Where(x => x.Scheme.Url == schemeUrl);
            var entityCategorySitemapIndexConfiguration = new EntityCategorySiteMapIndexConfiguration(dataSource, pageNumber, _cmsUrlBuilder, schemeUrl);
            return new DynamicSitemapIndexProvider().CreateSitemapIndex(new SitemapProvider(), entityCategorySitemapIndexConfiguration);
        }
        [Route("Products/{pageNumber:int?}")]
        public ActionResult Products( int? pageNumber)
        {
            var entityCategorySitemapIndexConfiguration = new ProductSiteMapIndexConfiguration(_productDataService.Query, pageNumber, _cmsUrlBuilder);
            return new DynamicSitemapIndexProvider().CreateSitemapIndex(new SitemapProvider(), entityCategorySitemapIndexConfiguration);
        }
    }
}