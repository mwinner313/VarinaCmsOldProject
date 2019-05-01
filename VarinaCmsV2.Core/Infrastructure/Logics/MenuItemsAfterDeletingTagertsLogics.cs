using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;
using VarinaCmsV2.DomainClasses.Users;
using Menu = System.Web.UI.WebControls.Menu;

namespace VarinaCmsV2.Core.Infrastructure.Logics
{
    public class MenuItemsAfterDeletingTagertsLogics:BaseAfterDeleteEntityLogic
    {
        private  IUnitOfWork _unitOfWork;

        public MenuItemsAfterDeletingTagertsLogics()
        {
        }

        public override async Task ApplyLogicAsync<T>(T obj, IPrincipal requestOwner)
        {
            _unitOfWork = Container.GetInstance<IUnitOfWork>(); ;

            if (obj is Category)
            {
                var cat = obj as Category;
                await DeleteMenuItemIfExists(cat,MenuItemTargetType.Category);
            }
            if (obj is Entity)
            {
                var entity = obj as Entity;
                await DeleteMenuItemIfExists(entity, MenuItemTargetType.Entity);
            }
            if (obj is User)
            {
                var user = obj as User;
                await DeleteMenuItemIfExists(user, MenuItemTargetType.UserProfile);
            }
            if (obj is Page)
            {
                var page = obj as Page;
                await DeleteMenuItemIfExists(page, MenuItemTargetType.Page);
            }
        }

        private async Task DeleteMenuItemIfExists<T>(T item, MenuItemTargetType type) where T : IBaseEntity
        {
            var menuItem =
                _unitOfWork.Set<MenuItem>().Include(x=>x.Childs).Include(x=>x.Parent).FirstOrDefault(
                    x => x.TargetId == item.Id && x.TargetType == type);
            if (menuItem == null) return;

            menuItem.Childs.ToList().ForEach(x =>
            {
                x.ParentId = menuItem.ParentId;
                _unitOfWork.Update(x);
            });

            _unitOfWork.Delete(menuItem);

          await  _unitOfWork.SaveChangesAsync();
        }
    }
}
