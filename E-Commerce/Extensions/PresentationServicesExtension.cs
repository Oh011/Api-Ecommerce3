using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Extensions
{
    public static class PresentationServicesExtension
    {


        public static IServiceCollection AddPresentationServices(this IServiceCollection Services)
        {

            Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


            //AddApplicationPart() adds additional assemblies that contain controllers. 
            //--> adds controllers in another assemblies 

            //Presentation.AssemblyReference is just a placeholder or a marker class in the Presentation project.
            //The.Assembly property accesses the assembly that contains this class. This is useful when the 
            //controllers are in another project, and you want to reference that project in your Program.cs 
            //to ensure the controllers are discovered.


            //-----------------------------------------------------------------------

            //AddEndpointsApiExplorer(): Helps generate API endpoint metadata.

            //AddSwaggerGen(): Adds Swagger(OpenAPI) documentation generation for API endpoints.


            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            //---------------------------------------------------------------

            //The ApiBehaviorOptions class allows you to configure default behaviors for API controllers

            //behavior of model validation and response generation when the model state is invalid

            Services.Configure<ApiBehaviorOptions>(

                //Func ==> IActionResult
                Options => Options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationsResponse

            );

            // changes the default behavior of how ASP.NET Core handles invalid model state 


            return Services;

        }
    }
}
