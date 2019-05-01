
using System.Threading.Tasks;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.Enums;

namespace VarinaCmsV2.Services.DataServices
{
    public class MenuItemDataService:BaseDataService<MenuItem>,IMenuItemDataService
    {
        public MenuItemDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override Task AddAsync(MenuItem model)
        {
            if (model.TargetType != MenuItemTargetType.CustomLink)
                model.Url = string.Empty;
            return base.AddAsync(model);
        }

        public override Task UpdateAsync(MenuItem model)
        {
            if (model.TargetType != MenuItemTargetType.CustomLink)
                model.Url = string.Empty;
            return base.UpdateAsync(model);
        }
    }
}
