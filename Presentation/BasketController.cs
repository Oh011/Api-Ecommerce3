using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dtos;

namespace Presentation
{

    [ApiController]
    [Route("/api/[controller]")] // BaseUrl/api/Basket
    [Authorize]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {


        [HttpGet("{id}")] //-->path

        public async Task<ActionResult<BasketDto>> Get(string id)
        {

            var basket = await _serviceManager.BasketService.GetBasketAsync(id);

            return Ok(basket);


        }

        [HttpPost]

        public async Task<ActionResult<BasketDto>> Update([FromBody] BasketDto basketDto)
        {


            var basket = await _serviceManager.BasketService.UpdateBasketAsync(basketDto);

            return Ok(basket);
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<bool>> Delete(string id)
        {

            await _serviceManager.BasketService.DeleteBasketAsync(id);

            return NoContent(); //--> 204
        }
    }
}

//HTTP Method: DELETE

//Request Body: Empty(Typically, you don’t need to send a body with a DELETE request, especially 
//when you're only providing an id)