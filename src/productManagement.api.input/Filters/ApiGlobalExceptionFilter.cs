using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using productManagement.domain.shared.seedWork.exceptions;

namespace ProductManagement.Api.Filters
{
    public class ApiGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _env;
        public ApiGlobalExceptionFilter(IHostEnvironment env)
            => _env = env;

        public void OnException(ExceptionContext context)
        {
            var details = new ProblemDetails();
            var exception = context.Exception;

            if (_env.IsDevelopment())
                details.Extensions.Add("StackTrace", exception.StackTrace);

            if (exception is EntityValidationException)
            {
                details.Title = "One or more validation errors ocurred";
                details.Status = StatusCodes.Status422UnprocessableEntity;
                details.Type = "UnprocessableEntity";

                var entityValidationException = (EntityValidationException)exception;

                if (entityValidationException.MessagesNotification.Count() == 0)
                    details.Detail = exception.Message;
                else
                    details.Detail = string.Join("; ",((EntityValidationException)exception).MessagesNotification.Select(x=>x.Text));
            }
            else if (exception is NotFoundException)
            {
                details.Title = "Not Found";
                details.Status = StatusCodes.Status404NotFound;
                details.Type = "NotFound";
                details.Detail = exception!.Message;
            }
            else
            {
                details.Title = "An unexpected error ocurred";
                details.Status = StatusCodes.Status422UnprocessableEntity;
                details.Type = "UnexpectedError";
                details.Detail = exception.GetBaseException().Message;
            }

            context.HttpContext.Response.StatusCode = (int)details.Status;
            context.Result = new ObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}
