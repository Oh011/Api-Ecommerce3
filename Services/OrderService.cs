using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Entities.OrderEntities;
using Domain.Exceptions;
using Services.Abstractions;
using Services.Specifications;
using Shared.OrderModels;
using Shippingaddress = Domain.Entities.OrderEntities.Address;



namespace Services
{
    internal class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string email)
        {

            //Shipping address
            //OrderItems ==> Basket {Basket Id } ==> BasketItems 
            //DeliveryMethod

            var ShippingAddress = _mapper.Map<Shippingaddress>(request.shippingAdress);

            var Basket = await _basketRepository.GetBasketAsync(request.BasketId);

            if (Basket == null)
                throw new BasketNotFoundException(request.BasketId);


            var OrderItems = new List<OrderItem>();


            foreach (var item in Basket.Items)
            {


                var Product = await unitOfWork.GetRepository<Product, int>().GetById(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);


                OrderItems.Add(CreateOrderItem(item, Product));

            }

            //delivery Method


            var DeliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetById(request.DeliveryMethodId);


            if (DeliveryMethod == null)
                throw new DeliveryMethodNotFoundException(request.DeliveryMethodId.ToString());


            var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

            var order = new Order(email, ShippingAddress, OrderItems, DeliveryMethod, subTotal);



            await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);

            await unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResultDto>(order);
        }


        public OrderItem CreateOrderItem(BasketItem item, Product product)
        {

            //ProductInOrderItem:

            var ProductInOrderItem = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);


            return new OrderItem(ProductInOrderItem, item.Quantity, item.Price);
        }


        public async Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethodsAsync()
        {


            var methods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync(true);

            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(methods);
        }

        public async Task<IEnumerable<OrderResultDto>> GetAllOrdersByEmailAsync(string userEmail)
        {

            var order = await unitOfWork.GetRepository<Order, Guid>().GetAllWithSpecificationsAsync(

                new OrderWithIncludeSpecifications(userEmail)

                );




            return _mapper.Map<IEnumerable<OrderResultDto>>(order);

        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>().GetWithIDSpecificationsAsync(


              new OrderWithIncludeSpecifications(id));


            if (order == null)
            {

                throw new OrderNotFoundException(id);
            }


            return _mapper.Map<OrderResultDto>(order);


        }
    }
}
