using Domain.Entities;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //Get Basket


        Task<CustomerBasket?> GetBasketAsync(string id);


        //Delete Basket


        Task<bool> DeleteBasketAsync(string id);


        //

        Task<CustomerBasket?> UpdateBasket(CustomerBasket basket, TimeSpan? TTL = null);
    }
}
