using Services;
using Services.Abstractions;
using Services.Mapping;
using Shared;

namespace E_Commerce.Extensions
{
    public static class CoreServicesExtension
    {


        public static IServiceCollection AddCoreServices(this IServiceCollection Services, IConfiguration configuration)
        {


            Services.AddScoped<IServiceManager, ServiceManager>();


            Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            //This line tells AutoMapper to:
            //Scan the entire assembly where ProductProfile is defined
            //Automatically register all Profile classes inside it


            //--------------------------------------

            //bind the configuration values from your appsettings.json(or any other configuration source)
            //to a strongly typed class (JwtOptions)
            //accessed by : jwtOptions.Value;

            Services.Configure<JwtOptions>(
               configuration.GetSection("JwtOptions"));

            //This allows you to inject IOptions<JwtOptions> anywhere in your code




            return Services;

        }
    }
}
