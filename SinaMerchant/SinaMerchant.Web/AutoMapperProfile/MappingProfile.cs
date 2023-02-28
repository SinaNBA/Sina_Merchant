using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using SinaMerchant.Web.Entities;
using SinaMerchant.Web.Models.ViewModels;

namespace SinaMerchant.Web
{
    #region MappingProfile
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserAddEditViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<IQueryable<User>, IQueryable<RegisterViewModel>>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();
            CreateMap<ShopOrder, ShopOrderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();
            CreateMap<IQueryable<ProductCategory>, IQueryable<ProductCategoryViewModel>>().ReverseMap();
            CreateMap<ProductConfig, ProductConfigViewModel>().ReverseMap();
            CreateMap<ProductItem, ProductItemViewModel>().ReverseMap();
            CreateMap<Variation, VariationViewModel>().ReverseMap();
            CreateMap<VariationOption, VariationOptionViewModel>().ReverseMap();

        }

    }
    #endregion


    #region MappingConfig
    //public class MappingConfig
    //{
    //public static MapperConfiguration RegisterMaps()
    //{
    //    var mappingConfig = new MapperConfiguration(config =>
    //    {
    //        config.AddExpressionMapping();

    //        config.CreateMap<User, UserAddEditViewModel>().ReverseMap();
    //        config.CreateMap<User, RegisterViewModel>().ReverseMap();
    //        config.CreateMap<IQueryable<User>, IQueryable<RegisterViewModel>>().ReverseMap();
    //        config.CreateMap<OrderDetail, OrderDetailViewModel>().ReverseMap();
    //        config.CreateMap<ShopOrder, ShopOrderViewModel>().ReverseMap();
    //        config.CreateMap<Product, ProductViewModel>().ReverseMap();
    //        config.CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap();
    //        config.CreateMap<IQueryable<ProductCategory>, IQueryable<ProductCategoryViewModel>>().ReverseMap();
    //        config.CreateMap<ProductConfig, ProductConfigViewModel>().ReverseMap();
    //        config.CreateMap<ProductItem, ProductItemViewModel>().ReverseMap();
    //        config.CreateMap<Variation, VariationViewModel>().ReverseMap();
    //        config.CreateMap<VariationOption, VariationOptionViewModel>().ReverseMap();


    //        //config.CreateMap<ProductModel, Product>().ForMember(x => x.Name, mf => mf.MapFrom(s=>s.Name.ToLower()));
    //    });
    //    return mappingConfig;
    //}
    //}
    #endregion


}
