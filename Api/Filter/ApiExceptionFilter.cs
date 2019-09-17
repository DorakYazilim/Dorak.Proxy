using System.Net;
using Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filter
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case BadPasswordException exception:
                    context.Exception = null;
                    context.Result = new JsonResult(new
                    {
                        message = exception.Message
                    });
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                default:
                    context.Exception = null;
                    context.Result = new JsonResult(new
                    {
                        message = "Internal server error..."
                    });
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }


            base.OnException(context);
        }
    }
}