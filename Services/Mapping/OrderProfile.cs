using AutoMapper;
using Domain.Entities.OrderEntities;
using Shared.OrderModels;
using Address = Domain.Entities.Address;
using ShippingAddress = Domain.Entities.OrderEntities.Address;

namespace Services.Mapping
{
    public class OrderProfile : Profile
    {


        public OrderProfile()
        {



            CreateMap<ShippingAddress, AddressDto>().ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodResult>();


            CreateMap<Address, AddressDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, options => options.MapFrom(s => s.product.ProductName))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(s => s.product.PictureUrl))
                .ForMember(d => d.ProductId, options => options.MapFrom(s => s.product.ProductId));





            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.PaymentStatus, options => options.MapFrom(s => s.PaymentStatus.ToString()))
                .ForMember(d => d.DeliveryMethod, options => options.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, options => options.MapFrom(s => s.Subtotal + s.DeliveryMethod.Price));



        }

    }
}
