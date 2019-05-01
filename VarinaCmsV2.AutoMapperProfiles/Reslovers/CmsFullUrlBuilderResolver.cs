using AutoMapper;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.AutoMapperProfiles.Reslovers
{
    internal class CmsFullUrlBuilderResolver : IValueResolver<IUrlable, IUrlableViewModel, string>
    {
        private readonly ICmsUrlBuilderFactory _cmsUrlBuilderFactory;

        public CmsFullUrlBuilderResolver(ICmsUrlBuilderFactory cmsUrlBuilderFactory)
        {
            _cmsUrlBuilderFactory = cmsUrlBuilderFactory;
        }

        public string Resolve(IUrlable source, IUrlableViewModel destination, string destMember, ResolutionContext context)
        {
            var cmsUrlBuilder = _cmsUrlBuilderFactory.GetUrlBuilder(source);
            return cmsUrlBuilder.GenerateFull(source);
        }
    }
}