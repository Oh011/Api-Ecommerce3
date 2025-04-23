using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_Commerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {


        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        private readonly RequestDelegate? _next;
        //delegate that represents the next middleware in the HTTP pipeline


        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {


            _next = next;
            _logger = logger;



            //ASP.NET Core automatically injects the RequestDelegate into the constructor of middleware classes 
            //when they're set up in the HTTP request pipeline
        }


        public async Task InvokeAsync(HttpContext context)
        {


            try
            {

                await _next(context);


                //404 for :--> //A user tries to access a URL that does not exist.

                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandleNotFoundApiAsync(context);
                }


            }

            catch (Exception ex)
            {


                //Log Exception

                _logger.LogError(ex, "An unhandled exception occurred.");

                //Handle Exception


                await HandleException(context, ex);

            }

        }

        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var Response = new ErrorDetails()
            {

                StatusCode = context.Response.StatusCode,
                ErrorMessage = $"The End point {context.Request.Path} not found"
            };

            await context.Response.WriteAsJsonAsync(Response);
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {


            //Set Content Type:

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;



            var Response = new ErrorDetails()
            {


                ErrorMessage = ex.Message
            };


            context.Response.StatusCode = ex switch
            {

                //all have error message

                NotFoundException => (int)HttpStatusCode.NotFound, //->// 404 for NotFoundException
                UnauthorizedException => (int)HttpStatusCode.Unauthorized, //-->401 Unauthorized
                ValidationException validationException => HandleValidationException(validationException, Response), //If validation Exception add :--> errors
                _ => (int)HttpStatusCode.InternalServerError, //->This is a default case (denoted by _), which is a catch-all for any exception that doesn't match the NotFoundException case.
            };



            Response.StatusCode = context.Response.StatusCode;



            //var Response = new ErrorDetails()
            //{

            //    StatusCode = context.Response.StatusCode,
            //    ErrorMessage = ex.Message
            //};



            var SerializedResponse = JsonSerializer.Serialize(Response);

            await context.Response.WriteAsync(SerializedResponse);
        }

        private int HandleValidationException(ValidationException ex, ErrorDetails response)
        {
            //Adds validation errors to the response body.



            response.Errors = ex.Errors;

            return (int)HttpStatusCode.BadRequest;

        }
    }
}

//GlobalExceptionMiddleware handles all unhandled exceptions that occur downstream in the middleware pipeline —
//that includes:

//NullReferenceException
//InvalidOperationException
//DivideByZeroException
//SqlException (if a DB error bubbles up)
//UnauthorizedAccessException
//HttpRequestException
//Any custom exceptions you throw
//Anything else that inherits from Exception


//Internal Server Error (500 - Server Error)
//Description: An unexpected error occurred on the server.

//Examples:
//Database connection failure.
//Unhandled exceptions in the application.
//Bugs or issues in server-side code.


//Not Found(404 - Client Error)
//Description: The requested resource could not be found on the server.

//Examples:
//A product with a specific ID does not exist.
//A user tries to access a URL that does not exist.


//Bad Request(400 - Client Error)
//Description: The client has sent a request that is malformed, missing required data, or contains invalid parameters.

//Examples:

//Missing required fields in the request body.
//Invalid JSON or incorrect request format.
//Invalid model state or validation errors.