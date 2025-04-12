using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Factories
{
    public class ApiResponseFactory
    {
        //custom model validation response method --> 400 Bad Request


        public static IActionResult CustomValidationsResponse(ActionContext actionContext)
        {





            var errors = actionContext.ModelState.Where(error => error.Value.Errors.Any()).Select(error =>


            new ValidationError
            {


                Field = error.Key,
                Errors = error.Value.Errors.Select(e => e.ErrorMessage)
            }


            );
            // KeyValuePair<string, ModelStateEntry>
            //So, error is a key - value pair where:
            //error.Key is the name of the field(e.g., "Email", "Password")
            //error.Value is the ModelStateEntry for that field, which contains the validation errors.

            var Response = new ValidationErrorResponse()
            {

                Errors = errors,
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validation Fields"
            };


            return new BadRequestObjectResult(Response);


        }
    }
}

//What's ActionContext?
//ActionContext contains all the context about the current HTTP request and action being executed, including 
//the ModelState
//actionContext.ModelState contains all the validation errors for the current 
//request's model binding (like [Required], [StringLength], etc.).


//ModelState is a dictionary-like object in ASP.NET Core that tracks the state of model binding and validation during a request.
//--> Dictionary<string, ModelStateEntry>

//--> string — the key Represents the name of the field (property name from the model).
//--> ModelStateEntry — the value Holds validation state and error messages for that field.
//["Email"] = ModelStateEntry {
//    Errors = ["The Email field is required."],
//        ValidationState = Invalid
//    },


//ActionContext is a class that represents the context of an HTTP action — basically everything about the 
//request that is being handled.

//It contains:

//HttpContext – full HTTP request/response
//RouteData – info about the current route
//ActionDescriptor – metadata about the action method
//ModelState – the validation state of the incoming model


//Example:

//ModelState = new Dictionary<string, ModelStateEntry>
//{
//    { "Email":key, new ModelStateEntry:value
//        {
//            Errors = new List<ModelError>
//            {
//                new ModelError("Email is required":ErrorMessage),
//                new ModelError("Email format is invalid")
//            }
//        }
//    },
//    { "Password", new ModelStateEntry
//        {
//            Errors = new List<ModelError>
//            {
//                new ModelError("Password is too short")
//            }
//        }
//    },
//    { "Username", new ModelStateEntry
//        {
//            Errors = new List<ModelError>() // No errors for this field
//        }
//    }
//};


//public class ModelStateEntry
//{
//    public ModelStateEntry();
//    public List<ModelError> Errors { get; }
//    public object RawValue { get; set; }
//    public bool Valid { get; }
//}

//public class ModelError
//{
//    public ModelError(string errorMessage);
//    public ModelError(string errorMessage, string errorCode);
//    public string ErrorMessage { get; }
//    public string ErrorCode { get; }
//}
