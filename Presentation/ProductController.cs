using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.Dtos;
using Shared.ParametersTypes;

namespace Presentation
{


    // This is not shown in swagger
    [Authorize]
    public class ProductController(IServiceManager _serviceManager) : ApiController
    {


        [HttpGet("Products")]


        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery] ProductQueryParameters Parameters)
        {


            var Products = await _serviceManager.ProductService.GetAllProductsAsync(Parameters);



            return Ok(Products); //status code : 200


        }



        [HttpGet("Brands")]

        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {


            var Brands = await _serviceManager.ProductService.GetAllBrandsAsync();



            return Ok(Brands);

        }


        [HttpGet("Types")]

        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {


            var Types = await _serviceManager.ProductService.GetAllTypesAsync();



            return Ok(Types);

        }


        [HttpGet("{id}")]



        public async Task<ActionResult<ProductResultDto>> GetById(int id)
        {




            if (!ModelState.IsValid)
                Console.Write("yes");



            var Product = await _serviceManager.ProductService.GetProductById(id);

            return Ok(Product);
        }

    }
}


//In ASP.NET Core Web API, ControllerBase is a base class for controllers that don't need to
//return views (i.e., API controllers). It is used when you're building RESTFUl APIs.

//ControllerBase provides several useful methods and properties for working with HTTP requests and responses,
//such as:

//Ok(): To return a 200 OK status with data.
//NotFound(): To return a 404 Not Found status.
//BadRequest(): To return a 400 Bad Request status.
//StatusCode(): To return a custom HTTP status code.
//Request: Provides access to the HTTP request.

// To use install --> Install-Package Microsoft.AspNetCore.Mvc



//ActionResult is a type used in ASP.NET Core MVC/Web API controllers to represent the result of an action. 
//It can represent different types of HTTP responses, such as OK, NotFound, BadRequest, etc.

//ActionResult<T> is a more specific version that wraps a return type T in an HTTP response. 
// It allows you to return a status code along with a value. It simplifies returning different 
// HTTP statuses like 200 OK, 400 BadRequest, 404 NotFound, etc., along with the actual result data.


//-----------------------------------


//[FromQuery] :  Query string GET /api/products?sort=name
//[FromRoute] :  Route segments	GET /api/products/{id}
//[FromBody]  :  Request body(JSON) POST / api / products with a JSON body
//[FromForm]  :  Form data	For form submissions (e.g., file uploads)


//[ProducesResponseType] tells the framework and tools like Swagger:
//-->used for API documentation and response metadata
//What kind of response a controller action might return.
//The HTTP status code associated with each type of response.