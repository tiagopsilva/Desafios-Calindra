using Calindra.Desafio.WebApi.Models;
using Calindra.Desafio.WebApi.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Calindra.Desafio.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly JsonHelperSerializer _jsonSerializer;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, JsonHelperSerializer jsonSerializer)
        {
            _next = next;
            _logger = logger;
            _jsonSerializer = jsonSerializer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    var json = _jsonSerializer.SerializeObject(new ErrorModel(context.Response.StatusCode, "Not Found"));
                    await context.Response.WriteAsync(json);
                    await _next(context);
                }
                else
                {
                    await _next(context);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong: {e}");
                await HandleExceptionAsync(context, e);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var json = _jsonSerializer.SerializeObject(new ErrorModel(context.Response.StatusCode, "Internal Server Error"));
            await context.Response.WriteAsync(json);
        }
    }
}
