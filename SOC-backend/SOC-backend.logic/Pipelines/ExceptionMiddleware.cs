using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace SOC_backend.logic.Pipelines
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = ex.Message;

            switch (ex)
            {
                case SqlException:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "Database connection could not be established";
                    break;
            }

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message,
            };

            string jsonResponse = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
