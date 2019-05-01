using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.Core.Services.CurrentUser;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.Security.Contracts;
using VarinaCmsV2.ViewModel.Eshop.Orders;
using VarinaCmsV2.ViewModel.User;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, CurrentUserViewModel>()
                .ForMember(x => x.Premissions, opt => opt.ResolveUsing<UserPremissionResolver>())
                .ForMember(x=>x.Roles,opt=>opt.ResolveUsing<UserRoleStringResolver>());

            CreateMap<User, UserLiquidViewModel>(); CreateMap<UserLiquidViewModel, User>();
            CreateMap<User, UserViewModel>().ForMember(x=>x.Roles,opt=>opt.ResolveUsing<UserRoleResolver>());
            CreateMap<User, UserWebClientViewModel>();
            CreateMap<UserAddOrUpdateViewModel, User>();
            CreateMap<UserInfoEditViewModel, User>();

            CreateMap<Role, RoleViewModel>();
            CreateMap<UserRoleAddOrUpdateModel, UserRole>();

            CreateMap<RoleViewModel, Role>();
            CreateMap<Address, AddressLiquidViewModel>();
            CreateMap<Address, AddressViewModel>();
            CreateMap<UserAddressesResponse, UserAddressesResponseLiquidViewModel>();
            CreateMap<AddressAddOrUpdateModel, Address>();
        }
    }

    internal class UserRoleStringResolver : IValueResolver<User, CurrentUserViewModel, List<string>>
    {
        private readonly IAppUserManager _userManager;

        public UserRoleStringResolver(IAppUserManager userManager)
        {
            _userManager = userManager;
        }

        public List<string> Resolve(User source, CurrentUserViewModel destination, List<string> destMember, ResolutionContext context)
        {
            return _userManager.GetRoles(source).Select(x => x.Name).ToList();
        }
    }

    internal class UserPremissionResolver : IValueResolver<User, CurrentUserViewModel, List<string>>
    {
        private readonly IAppUserManager _userManager;

        public UserPremissionResolver(IAppUserManager userManager)
        {
            _userManager = userManager;
        }

        public List<string> Resolve(User source, CurrentUserViewModel destination, List<string> destMember, ResolutionContext context)
        {
            return _userManager.GetUserPremissions(source).Select(x => x.Action).ToList(); 
        }
    }
}
