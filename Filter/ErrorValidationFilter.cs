using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Utility.ErrorResponse.Helper;
using Utility.OKResponse.Helper;

namespace Utility.Validation.Filter
{
    public class ErrorValidationFilter :  IExceptionFilter,IActionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            context.Result = new BadRequestObjectResult(ErrorHelper.GetErrorResponse(ex));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var validatorModel = new List<string>();
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                //var errorList = new ErrorList {title = key};
                
                if (errors.Count > 0)
                {
                    foreach (var t in errors)
                    {
                        string message = $"{t.ErrorMessage}";
                        validatorModel.Add(message);
                    }
                    
                }
                
            }

            ResponseModel<List<string>> response = new()
            {
                ResponseResult = false,
                data = validatorModel,
                message = "validasi gagal"
            };

            if (!context.ModelState.IsValid)
            {
                context.Result = new OkObjectResult(response);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }

    public class ErrorList
    {
        public string message { get; set; }
    }


}
