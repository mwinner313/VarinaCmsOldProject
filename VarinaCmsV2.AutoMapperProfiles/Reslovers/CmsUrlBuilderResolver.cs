using AutoMapper;
using VarinaCmsV2.Core.Contracts.Web;
using VarinaCmsV2.DomainClasses.Contracts;

namespace VarinaCmsV2.AutoMapperProfiles.Reslovers
{
    internal class CmsUrlBuilderResolver : IValueResolver<IUrlable, IUrlableViewModel, string>
    {
        private readonly ICmsUrlBuilderFactory _urlBuilderFactory;
        public CmsUrlBuilderResolver(ICmsUrlBuilderFactory urlBuilderFactory)
        {
            _urlBuilderFactory = urlBuilderFactory;
        }

        public string Resolve(IUrlable source, IUrlableViewModel destination, string destMember, ResolutionContext context)
        {
            var cmsUrlBuilder = _urlBuilderFactory.GetUrlBuilder(source);
            var url = cmsUrlBuilder.Generate(source);
            return url;
        }
    }
}