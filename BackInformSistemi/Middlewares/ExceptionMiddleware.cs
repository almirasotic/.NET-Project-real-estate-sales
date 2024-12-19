using System.Net;
using System.Threading.Tasks;
using BackInformSistemi.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackInformSistemi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Postavljanje statusnog koda na 500
                var statusCode = HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = "application/json";
                string message;
                var exceptionType = ex.GetType();
                if(exceptionType == typeof(UnauthorizedAccessException)) {
                
                    statusCode= HttpStatusCode.Forbidden;
                    message = "you are not authorized";
                }
                else 
                {
                    statusCode= HttpStatusCode.InternalServerError;
                    message = "some unknown error";
                }
                ApiError response;

                // Provera okruženja
                if (_env.IsDevelopment())
                {
                    response = new ApiError((int)statusCode, ex.Message, ex.StackTrace?.ToString());
                }
                else
                {
                    response = new ApiError((int)statusCode, message);
                }

                // Logovanje greške
                _logger.LogError(ex, ex.Message);

                // Serijalizacija odgovora u JSON
                var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
