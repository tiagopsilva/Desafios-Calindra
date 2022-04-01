using Calindra.Desafio.WebApi.Models;
using Calindra.Desafio.WebApi.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Calindra.Desafio.WebApi.Middlewares
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly JsonHelperSerializer _jsonSerializer;

        public NotFoundMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, JsonHelperSerializer jsonSerializer)
        {
            _next = next;
            _logger = logger;
            _jsonSerializer = jsonSerializer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                context.Response.ContentType = "application/json";
                var json = _jsonSerializer.SerializeObject(new ErrorModel(context.Response.StatusCode, "Not Found"));
                await context.Response.WriteAsync(json);
                await _next(context);
            }
        }
    }
}
