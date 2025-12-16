using System.Net;
using System.Text.Json;

namespace InvoiceManagement.API.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode;

            switch (ex)
            {
                case ArgumentNullException:
                    statusCode = (int)HttpStatusCode.BadRequest; // 400
                    break;

                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized; // 401
                    break;

                case Exception:
                    statusCode = (int)HttpStatusCode.InternalServerError; // 500 pour toutes les autres exceptions génériques
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError; // 500
                    break;
            }

            context.Response.StatusCode = statusCode;

            await SendResponse(context,ex.Message);
        }

        private async Task SendResponse(HttpContext context, string message)
        {
            var response = new
            {
                message = message
            };

            var responseText = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(responseText);
        }
    }
}