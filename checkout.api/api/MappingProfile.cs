using Api.Cart;
using Application.Domain;
using Application.UseCases.CreateCart;
using Application.UseCases.ProvideShippingDetails;
using Application.UseCases.ProvideShopperDetails;
using AutoMapper;

namespace Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCartRequest, CreateCart>();
            CreateMap<CreateCartProductRequest, CartProduct>();
            CreateMap<ShopperDetailsRequest, Application.UseCases.ProvideShopperDetails.ShopperDetails>();
            CreateMap<ProvideShopperDetailsRequest, ProvideShopperDetails>();
            CreateMap<ShippingDetailsRequest, Application.UseCases.ProvideShippingDetails.ShippingDetails>();
            CreateMap<ProvideShippingDetailsRequest, ProvideShippingDetails>();
        }
    }
}
