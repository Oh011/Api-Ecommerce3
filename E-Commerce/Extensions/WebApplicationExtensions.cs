using Domain.Contracts;
using E_Commerce.Middlewares;

namespace E_Commerce.Extensions
{
    public static class WebApplicationExtensions
    {


        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {

            //Return a WebApplication --> used for chaining

            //------------------------------------------

            using var scope = app.Services.CreateScope();

            //This allows retrieving services like IDbInitializer that are registered in dependency injection (DI).

            var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            //Gets an instance of IDbInitializer

            await DbInitializer.InitializeAsync();

            await DbInitializer.InitializeIdentityAsync();


            return app;
        }

        public static WebApplication UseCustomMiddleware(this WebApplication app)
        { //--> Used if we add more Middlewares


            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            // This will call InvokeAsync() in GlobalExceptionMiddleware
            //ASP.NET Core framework itself calls the InvokeAsync() method for every middleware in the pipeline.

            return app;
        }
    }
}
