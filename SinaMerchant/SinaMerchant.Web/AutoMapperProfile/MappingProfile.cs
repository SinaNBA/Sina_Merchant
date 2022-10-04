using AutoMapper;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;

namespace SinaMerchant.Web.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SiteUser, SiteUserViewModel>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();
            CreateMap<ShopOrder, ShopOrderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();
            CreateMap<ProductConfig, ProductConfigViewModel>().ReverseMap();
            CreateMap<ProductItem, ProductItemViewModel>().ReverseMap();
            CreateMap<Variation, VariationViewModel>().ReverseMap();
            CreateMap<VariationOption, VariationOptionViewModel>().ReverseMap();

        }

    }
}
