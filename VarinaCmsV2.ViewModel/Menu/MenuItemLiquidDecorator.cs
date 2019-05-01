using AutoMapper;

namespace VarinaCmsV2.ViewModel.Menu
{
    public abstract class MenuItemLiquidDecorator:MenuItemLiquidViewModel
    {
        public MenuItemLiquidDecorator()
        {

        }

        public MenuItemLiquidDecorator(MenuItemLiquidViewModel wrapee)
        {
            Mapper.Map(wrapee, this);
        }

        public abstract MenuItemLiquidViewModel Decorate(MenuItemLiquidViewModel wrappe);
      
    }
}