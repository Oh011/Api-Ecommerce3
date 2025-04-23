using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace Services.Mapping
{
    public class BasketProfile : Profile
    {


        public BasketProfile()
        {


            CreateMap<CustomerBasket, BasketDto>().ReverseMap();

            //--> Customer Basket has :  public IEnumerable<BasketItem> Items { get; set; }

            //BasketDto ahs :   public IEnumerable<BasketItemDto> Items { get; init; }

            //need to convert from BasketItem --> BasketItemDto

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        }
    }
}
