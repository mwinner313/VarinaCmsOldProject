using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel.Eshop;
using VarinaCmsV2.ViewModel.Eshop.Orders;

namespace VarinaCmsV2.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>().ForMember(x => x.PrimaryImage, opt => opt.MapFrom(s =>
                      s.Pictures.OrderBy(p => p.DisplayOrder).FirstOrDefault()
               ));
            CreateMap<ProductShipment, ProductShipmentViewModel>();
            CreateMap<ProductShipmentViewModel, ProductShipment>();
            CreateMap<Inventory, ProductInventoryViewModel>();
            CreateMap<ProductInventoryViewModel, Inventory>();
            CreateMap<ProductAddOrUpdateModel, Product>().ForMember(x => x.Fields, opt => opt.Ignore());
            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductIdModel, Product>();
            CreateMap<ProductPicture, ProductPictureViewModel>();
            CreateMap<ProductPictureViewModel, ProductPicture>();
            CreateMap<Product, ProductLiquidAdapter>().ForMember(x => x.PrimaryPicture, opt => opt.MapFrom(s =>
                s.Pictures.OrderBy(p => p.DisplayOrder).FirstOrDefault()));
            CreateMap<ProductsCompareModel, ProductsCompareModelLiquidVeiwModel>();
            CreateMap<Product, OrderedProductViewModel>().ForMember(x => x.PrimaryImage, opt => opt.MapFrom(s =>
                s.Pictures.OrderBy(p => p.DisplayOrder).FirstOrDefault()
            ));
            CreateMap<Product, ProductSimpleModel>().ForMember(x => x.PrimaryImage, opt => opt.MapFrom(s =>
                s.Pictures.OrderBy(p => p.DisplayOrder).FirstOrDefault()
            ));
            CreateMap<ProductReview, ProductReviewLiquidViewModel>();
            CreateMap<Inventory, InventoryLiquidViewModel>();
            CreateMap<ProductShipment, ProductShipmentLiquidViewModel>();

            CreateMap<ProductPicture, ProductPictureLiquidViewModel>();
        }
    }
}
