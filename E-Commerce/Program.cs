using E_Commerce.Extensions;


namespace E_Commerce
{

    public class Program
    {
        public static async Task Main(string[] args)
        {


            //-----------------------------------------------------------
            var builder = WebApplication.CreateBuilder(args);

            /*
                This initializes a WebApplicationBuilder instance.
                It configures the services and the middleware pipeline for the application.
             
             */

            //--------------------------------------------------------------

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });



            //Presentation Services:



            builder.Services.AddPresentationServices();




            //------------------------------------------------------------------------



            //Cores Services:


            builder.Services.AddCoreServices(builder.Configuration);




            //Infrastructure Services




            builder.Services.AddInfrastructureServices(builder.Configuration);


            //-------------------------------------------------------------------------

            //This compiles the application and creates a WebApplication instance.

            var app = builder.Build();


            app.UseCustomMiddleware();
            await app.SeedDbAsync();

            app.UseCors("AllowAll");


            //------------------------------------------------------------------------------

            //Configuring Middleware

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            //Security Middlewares

            app.UseAuthentication();

            app.UseAuthorization();



            //Maps API controller routes, so API endpoints can handle incoming HTTP requests.
            app.MapControllers();

            app.Run();


        }
    }
}


//AddAuthentication(...)  Configures how authentication should work	:
//-->It does:
//Registers the authentication service in the dependency injection container.
//Specifies what kind of authentication to use (like JWT Bearer).
//Sets default schemes, and configures JWT options (like validation parameters, secret key)

//UseAuthentication()	Actually activates authentication middleware:
//It does:
//Tells ASP.NET Core to look for and validate tokens in incoming requests.
//Sets the current User based on the token's claims.
//-->Without this, even if everything is configured, requests will not be authenticated!