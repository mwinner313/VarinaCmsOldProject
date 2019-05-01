using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.AutoMapperProfiles.Security
{
    public class RolesPremissionsProfile:Profile
    {
        public RolesPremissionsProfile()
        {
            CreateMap<AllowedRole, RolePremissionViewModel>();
            CreateMap<RolePremissionViewModel, AllowedRole>();
        }
    }
}
