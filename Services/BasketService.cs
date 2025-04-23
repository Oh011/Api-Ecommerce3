using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Services.Abstractions;
using Shared.Dtos;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository, IMapper _mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }

        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            if (basket == null)
            {
                throw new BasketNotFoundException(id);
            }

            return _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {

            var Basket = _mapper.Map<CustomerBasket>(basket);
            var CustomerBasket = await _basketRepository.UpdateBasket(Basket);

            if (CustomerBasket == null)
            {
                throw new Exception("Can not Update the Basket");
            }

            return _mapper.Map<BasketDto?>(CustomerBasket);
        }
    }
}
