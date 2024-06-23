using AutoMapper;

using EcommerceBasket.Domain.Models;
using EcommerceBasket.Infrastructure.Persistence;

namespace EcommerceBasket.API.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Basket, BasketEntity>().ReverseMap();
            CreateMap<BasketItem, BasketItemEntity>().ReverseMap();
        }
    }
}
