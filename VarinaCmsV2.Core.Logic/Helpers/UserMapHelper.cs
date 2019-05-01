using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using VarinaCmsV2.Core.Contracts.WebClientServices;
using VarinaCmsV2.Core.Contracts.WebClientServices.Entities;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Users;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.User;
using VarinaCmsV2.ViewModel.User.Account;

namespace VarinaCmsV2.Core.Logic.Helpers
{
    public static class UserMapHelper
    {
        public static UserViewModel MapToViewModel(this User user)
        {
            return Mapper.Map<UserViewModel>(user);
        }
        public static User MapToModel(this UserAddOrUpdateViewModel user)
        {
            return Mapper.Map<User>(user);
        }
        public static User MapToModel(this UserAddOrUpdateViewModel user, User model)
        {
            return Mapper.Map(user, model);
        }
        public static UserLiquidViewModel AsLiquidAdapted(this User user)
        {
            return Mapper.Map<UserLiquidViewModel>(user);
        }
        public static UserLiquidViewModel Attach(this UserLiquidViewModel user, List<Entity> entities)
        {
            user.Entities = new List<EntityLiquidAdapter>();
            entities.ForEach(x => user.Entities.Add(Mapper.Map<EntityLiquidAdapter>(x)));
            return user;
        }
        public static UserLiquidViewModel Attach(this UserLiquidViewModel user, PaginatedEntities entities)
        {
            user.Attach(entities.Entities);
            return user;
        }

        public static async Task<List<UserViewModel>> ToViewModelListAsync(this IQueryable<User> users)
        {
            return Mapper.Map<List<User>, List<UserViewModel>>(await users.ToListAsync());
        }

        public static  void MapToExisting(this UserInfoEditViewModel model, User existing)
        {
            Mapper.Map(model, existing);
        }
    }
}
