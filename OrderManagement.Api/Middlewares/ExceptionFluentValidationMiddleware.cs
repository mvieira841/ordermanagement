using FluentValidation;

namespace OrderManagement.API.Middlewares;

public class ExceptionFluentValidationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static async Task HandleException(HttpContext context, Exception? exception)
    {
        string jsonContent = String.Empty;

        if (!(exception is ValidationException validationException))
        {

            jsonContent = JsonSerializer.Serialize(new
            {
                Errors = new { Detail = "Internal error" }
            });

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        }
        else
        {
            var errors = validationException.Errors.Select(err => err.ErrorMessage);

            jsonContent = JsonSerializer.Serialize(errors);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(jsonContent, encoding: Encoding.UTF8);

    }
}
