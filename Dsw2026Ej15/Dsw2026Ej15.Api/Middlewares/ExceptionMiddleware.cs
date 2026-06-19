using Dsw2026Ej15.Domain.Exceptions;
using System.Net;
using System.Text.Json;

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
            

                await HandleExceptionAsync(context,ex);
            }
            
        }
        //Metdod visto hola vicenete no soy ia  en clase para menjar la excepcion del Middleware
        public async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "ocurrio un error inesperado al ejecutar la solicitud";
            if (ex is ValidationException ve) 
            {
                message = ve.Message;
                status = HttpStatusCode.BadRequest;
            }

            var result = JsonSerializer.Serialize(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(result);
        }





    }
}




