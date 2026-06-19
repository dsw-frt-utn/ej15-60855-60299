using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.Clear();

                await Results.BadRequest(new
                {
                    message = ex.Message
                }).ExecuteAsync(context);
            }
            catch (Exception)
            {
                context.Response.Clear();

                await Results.Problem(
                    title: "Ocurrió un error interno en la aplicación.",
                    statusCode: StatusCodes.Status500InternalServerError
                ).ExecuteAsync(context);
            }
        }
    }
}