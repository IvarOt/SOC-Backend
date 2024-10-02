using Microsoft.AspNetCore.Http;
using SOC_backend.logic.ExceptionHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SOC_backend.logic.ExceptionHandling
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
            string message = "Something unforeseen happened..";

            switch (ex)
            {
                case NotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    message = ex.Message;
                    break;
                case PropertyException _:
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
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
