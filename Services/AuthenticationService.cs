using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared;
using Shared.Dtos;
using Shared.OrderModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    internal class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> options, IMapper mapper) : IAuthenticationService
    {
        public async Task<bool> EmailExists(string email)
        {

            var user = await _userManager.FindByNameAsync(email);

            return user != null;
        }

        public async Task<AddressDto> GetUserAddress(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).
               FirstOrDefaultAsync(u => u.Email == email);


            if (user == null)
            {

                throw new UserNotFoundException(email);
            }

            return mapper.Map<AddressDto>(user.Address);
        }

        public async Task<UserResultDto> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByNameAsync(email);


            if (user == null)
                throw new UserNotFoundException(email);



            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), email);

        }

        public async Task<UserResultDto> LogIn(LogInDto loginDto)
        {

            //Check Email

            var user = await _userManager.FindByEmailAsync(loginDto.Email);


            if (user == null) throw new UnauthorizedException(); //--> 401 Unauthorized

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);


            if (!result) throw new UnauthorizedException(); //-> 401 Unauthorized


            return new UserResultDto(user.DisplayName, await CreateTokenAsync(user), user.Email);


        }

        public async Task<UserResultDto> Register(RegisterDto registerDto)
        {


            var user = new User()
            {

                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName,
            };


            var result = await _userManager.CreateAsync(user, registerDto.Password);



            if (!result.Succeeded)
            {

                var errors = result.Errors.Select(e => e.Description).ToList();

                throw new ValidationException(errors);

            }


            return new UserResultDto(registerDto.DisplayName, await this.CreateTokenAsync(user), registerDto.Email);
        }

        public async Task<AddressDto> UpdateUserAddress(AddressDto addressDto, string email)
        {
            //If user Address is null ==> Create User Address from Dto
            //else ==> upadate


            var user = await _userManager.Users.Include(u => u.Address).
              FirstOrDefaultAsync(u => u.Email == email);


            if (user.Address == null)
            {

                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.Street = addressDto.Street;
            }


            else
            {

                var address = mapper.Map<Address>(addressDto);
                user.Address = address;
            }


            await _userManager.UpdateAsync(user);

            return mapper.Map<AddressDto>(user.Address);

        }

        private async Task<string> CreateTokenAsync(User user)
        {

            var JwtOptions = options.Value;

            var claims = new List<Claim> {

                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email),
            };


            var Roles = await _userManager.GetRolesAsync(user);


            foreach (var role in Roles)
            {

                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            //Creates a symmetric security key using a long, secure string.

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));


            //Tells the token generator:

            //What key to use(Key)

            //What algorithm to use(HmacSha256)
            var SignCreds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            //Describes the structure of the JWT token:
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //all of this  goes into the payload
                Subject = new ClaimsIdentity(claims),
                Issuer = JwtOptions.Issuer,
                Audience = JwtOptions.Audiance, //frontend app
                Expires = DateTime.UtcNow.AddDays(JwtOptions.ExpirationInDays),

                //-----------------------------------------------
                SigningCredentials = SignCreds

            };



            var tokenHandler = new JwtSecurityTokenHandler();
            var Token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(Token);
        }
    }
}


//A Claim is a piece of information (like a key-value pair) about the user. It represents an
//identity attribute, such as:

//Name , Email , Role , UserId , Any custom data (e.g., department, location, permissions)

//ClaimTypes is a static class provided by .NET that contains predefined constants for commonly used claim
//Ex:new Claim("email", "john@example.com")

//--> Type:value


//Encoding.UTF8.GetBytes(JwtOptions.SecretKey):
//-->This specifies that you want to use UTF-8 text encoding 
//This method takes a string and returns the raw bytes that represent that string in UTF-8.:

//Why do we need this?
//JWTs are signed using a cryptographic key, and cryptographic libraries like SymmetricSecurityKey
//expect the key to be provided as a byte array, not a plain string.
