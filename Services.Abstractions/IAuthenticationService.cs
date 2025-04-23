using Shared.Dtos;
using Shared.OrderModels;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {


        //Result --> Display name , email , Token
        public Task<UserResultDto> LogIn(LogInDto loginDto);
        public Task<UserResultDto> Register(RegisterDto registerDto);


        //Get current User


        public Task<UserResultDto> GetUserByEmail(string email);

        //Check If email Exists


        public Task<bool> EmailExists(string email);

        //update User Address

        public Task<AddressDto> UpdateUserAddress(AddressDto addressDto, string email);

        //Get User Address


        public Task<AddressDto> GetUserAddress(string email);


    }
}
