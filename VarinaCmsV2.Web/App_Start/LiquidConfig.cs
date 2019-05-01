using DotLiquid;
using System;
using VarinaCmsV2.Core.Logic.Templating.Liquid.Blocks;
using VarinaCmsV2.Core.Logic.Templating.Liquid.Tags;

namespace VarinaCmsV2.Web
{
    internal class LiquidConfig
    {
        internal static void RegisterLiquidTagBlocks()
        {
            Template.RegisterTag<AntiForgeryTokenTag>("antiforgery_token_input");
            Template.RegisterTagFactory(new SimpleTagFactory<EntityFieldFinder>("render_field"));
            Template.RegisterTagFactory(new SimpleTagFactory<ProductPriceCalculator>("get_price"));
            Template.RegisterTagFactory(new SimpleTagFactory<EntityPaginator>("paginate"));
            Template.RegisterTagFactory(new SimpleTagFactory<PaginationLinkBuilder>("paginate_link"));
            Template.RegisterTagFactory(new SimpleTagFactory<UserCreatedEntitiesPaginator>("paginate_user_entities"));
        }
    }

    internal class SimpleTagFactory<T> : ITagFactory where T : Tag
    {
        public SimpleTagFactory(string name)
        {
            TagName = name;
        }

        public Tag Create()
        {
            return Startup.Container.GetInstance<T>();
        }

        public string TagName { get; }
    }
}