using Shared.OrderModels;

namespace Services.Abstractions
{
    public interface IOrderService
    {

        //Get Order By Id ==> OrderResultDto (Guid Id)
        //Get All order for user by email
        //Create Order
        //Get all delivery Methods


        Task<OrderResultDto> GetOrderByIdAsync(Guid id);

        Task<IEnumerable<OrderResultDto>> GetAllOrdersByEmailAsync(string userEmail);


        Task<OrderResultDto> CreateOrderAsync(OrderRequest request, string email);

        Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethodsAsync();





    }
}
