using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.DataSeeding;
using Persistence.Repositories;
using Services;
using Services.Abstractions;


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



            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


            //AddApplicationPart() adds additional assemblies that contain controllers. 
            //--> adds controllers in another assemblies 

            //Presentation.AssemblyReference is just a placeholder or a marker class in the Presentation project.
            //The.Assembly property accesses the assembly that contains this class. This is useful when the 
            //controllers are in another project, and you want to reference that project in your Program.cs 
            //to ensure the controllers are discovered.






            builder.Services.AddScoped<IDbInitializer, DbInitializer>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(typeof(Services.Mapping.ProductProfile));


            //-----------------------------------------------------------------------

            //AddEndpointsApiExplorer(): Helps generate API endpoint metadata.

            //AddSwaggerGen(): Adds Swagger(OpenAPI) documentation generation for API endpoints.


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //---------------------------------------------------------------



            builder.Services.AddDbContext<ApplicationDbContext>(Options =>
            {



                //Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                Options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnectionString"]);


                //ConfigurationManager implements both IConfiguration and IConfigurationManager.
            });


            //-------------------------------------------------------------------------

            //This compiles the application and creates a WebApplication instance.

            var app = builder.Build();


            await InitializeDbAsync(app);

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

            app.UseAuthorization();

            app.UseStaticFiles();


            //Maps API controller routes, so API endpoints can handle incoming HTTP requests.
            app.MapControllers();

            app.Run();

            //This method InitializeDbAsync is a helper function that initializes the database
            async Task InitializeDbAsync(WebApplication app)
            {


                using var scope = app.Services.CreateScope();

                //This allows retrieving services like IDbInitializer that are registered in dependency injection (DI).

                var DbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

                //Gets an instance of IDbInitializer

                await DbInitializer.InitializeAsync();


            }
        }
    }
}
