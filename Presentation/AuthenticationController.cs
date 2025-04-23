using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dtos;
using Shared.OrderModels;
using System.Security.Claims;

namespace Presentation
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {



        [HttpPost("LogIn")]


        public async Task<ActionResult<UserResultDto>> LogIn(LogInDto logInDto)
        {


            var result = await _serviceManager.AuthenticationService.LogIn(logInDto);



            return Ok(result);



        }


        [HttpPost("Register")]


        public async Task<ActionResult<UserResultDto>> Register([FromBody] RegisterDto registerDto)
        {


            var result = await _serviceManager.AuthenticationService.Register(registerDto);


            return Ok(result);
        }



        [HttpGet("EmailExists")]

        // no need for Authorization


        public async Task<ActionResult<bool>> EmailExists(string email)
        {


            return Ok(_serviceManager.AuthenticationService.EmailExists(email));

        }


        [Authorize]
        [HttpGet]

        public async Task<ActionResult<UserResultDto>> GetUser()
        {

            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.AuthenticationService.GetUserByEmail(email);


            return Ok(result);
        }


        [Authorize]
        [HttpGet("Address")]

        public async Task<ActionResult<AddressDto>> GetAddress()
        {

            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.AuthenticationService.GetUserAddress(email);

            return Ok(result);
        }


        [Authorize]
        [HttpPut("Address")]

        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
        {

            var email = User.FindFirstValue(ClaimTypes.Email);

            var result = await _serviceManager.AuthenticationService.UpdateUserAddress(address, email);


            return Ok(result);
        }

    }
}


//In js :

//const formData = new FormData();
//formData.append("email", "test@example.com");
//formData.append("password", "StrongPass123!");

//fetch("https://yourapi.com/api/account/register", {
//method: "POST",
//  body: formData
//});

//That would not bind to [FromBody] — instead, you’d need to use [FromForm] in your C# controller:

//-->public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)