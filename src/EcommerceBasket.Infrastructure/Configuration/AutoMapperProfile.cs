using AutoMapper;

using EcommerceBasket.Domain.Entities;
using EcommerceBasket.Infrastructure.Persistence;

namespace EcommerceBasket.Infrastructure.Configuration
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
