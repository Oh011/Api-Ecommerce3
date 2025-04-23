using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;
using Persistence.Data.DataSeeding;
using Persistence.Identity;
using Persistence.Repositories;
using Shared;
using StackExchange.Redis;
using System.Text;

namespace E_Commerce.Extensions
{
    public static class InfrastructureServicesExtension
    {


        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services, IConfiguration Configuration)
        {

            Services.AddScoped<IDbInitializer, DbInitializer>();
            Services.AddScoped<IBasketRepository, BasketRepository>();


            Services.AddScoped<IUnitOfWork, UnitOfWork>();


            Services.AddDbContext<ApplicationDbContext>(Options =>
             {



                 //Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                 Options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["DefaultConnectionString"]);


                 //ConfigurationManager implements both IConfiguration and IConfigurationManager.
             });


            Services.AddIdentity<User, IdentityRole>(options =>
            {

                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;



            }

            ).AddEntityFrameworkStores<IdentityAppDbContext>();



            Services.AddDbContext<IdentityAppDbContext>(Options =>
            {



                //Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                Options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["IdentityConnection"]);


                //ConfigurationManager implements both IConfiguration and IConfigurationManager.
            });


            Services.AddSingleton<IConnectionMultiplexer>(service =>


          ConnectionMultiplexer.Connect(Configuration.GetSection("ConnectionStrings")["Redis"])
            );


            Services.ConfigureJwtOptions(Configuration);

            return Services;

        }



        public static IServiceCollection ConfigureJwtOptions(this IServiceCollection Services, IConfiguration Configuration)
        {

            //binds configurations to the Type
            var JwtOptions = Configuration.GetSection("JwtOptions").Get<JwtOptions>();

            //Validate Token


            Services.AddAuthentication(options =>
            {


                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {


                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtOptions.Issuer,
                    ValidAudience = JwtOptions.Audiance,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey)),

                };
            });


            Services.AddAuthorization();


            return Services;
        }

    }
}
