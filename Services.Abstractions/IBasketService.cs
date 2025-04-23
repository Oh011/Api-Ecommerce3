using Shared.Dtos;

namespace Services.Abstractions
{
    public interface IBasketService
    {

        //Get , Delete , Update 

        Task<BasketDto?> GetBasketAsync(string id);


        Task<bool> DeleteBasketAsync(string id);


        Task<BasketDto?> UpdateBasketAsync(BasketDto basket);
    }
}
