using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.OrderModels;
using System.Security.Claims;

namespace Presentation
{
    public class OrderController(IServiceManager serviceManager) : ApiController
    {

        [HttpPost]

        public async Task<ActionResult<OrderResultDto>> Create(OrderRequest request)
        {


            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await serviceManager.OrderService.CreateOrderAsync(request, email);


            return Ok(order);

        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllOrders()
        {

            var email = User.FindFirstValue(ClaimTypes.Email);

            var orders = await serviceManager.OrderService.GetAllOrdersByEmailAsync(email);


            return Ok(orders);


        }


        [HttpGet("{id}")]

        public async Task<ActionResult<OrderResultDto>> GetOrder(Guid id)
        {

            var order = await serviceManager.OrderService.GetOrderByIdAsync(id);


            return Ok(order);
        }



        [HttpGet("DeliveryMethods")]

        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {


            var methods = await serviceManager.OrderService.GetAllDeliveryMethodsAsync();

            return Ok(methods);
        }

    }
}


//User property refers to the currently authenticated user — it gives you access to
//their claims, identity, and roles

//🔐 What does “currently authenticated user” really mean?
//In an ASP.NET Core Web API, the User property represents the user making the current HTTP request.
//So while many users can be authenticated overall (like on a big system with thousands of users), 
//each request only comes from one client, and that request has one authenticated identity 
//attached to it — if the user is logged in.


//-->: FindFirstValue(...) is a helper that pulls the value of the first claim matching ClaimTypes.Email.