using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.User;

namespace VarinaCmsV2.AutoMapperProfiles
{
    internal class UserRoleResolver : IValueResolver<User, UserViewModel, List<RoleViewModel>>
    {
        //TODo get rid of this bullshit l from solid is mising
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<Role> _roles;

        public UserRoleResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _roles = unitOfWork.Set<Role>();
        }

        public List<RoleViewModel> Resolve(User source, UserViewModel destination, List<RoleViewModel> destMember, ResolutionContext context)
        {
            var roleIds = source.Roles.Select(x => x.RoleId).ToList();
            var roles = _roles.Where(x => roleIds.Contains(x.Id)).ToList();
            var vm = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                vm.Add(new RoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Premissions = role.PermissionsJObject.ToList()
                });
            }
            return vm;
        }
    }
}