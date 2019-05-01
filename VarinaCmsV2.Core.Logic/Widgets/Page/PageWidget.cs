using System.Data.Entity;
using System.Linq;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Contracts.Widget;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.Core.Logic.Widgets.Page
{
    public class PageWidget : BaseWidgetWithMetaData<PageMetaData>, IWidget
    {
        private readonly IPageDataService _pageDataService;

        public PageWidget()
        {
            _pageDataService = Container.GetInstance<IPageDataService>();
        }

        public override string Type { get; } = "page";
        public override BaseWidgetLiquidData Parse(JObject widgetMetaData)
        {
            base.DeserializeMetaData(widgetMetaData);
            var page =
                _pageDataService.Query.Include(x => x.Tags)
                    .Include(x => x.Creator)
                    .First(x => x.Id == MetaData.PageId);
            return new PageWidgetLiquidData() { Page = page.AdaptToLiquid() };
        }
    }
}