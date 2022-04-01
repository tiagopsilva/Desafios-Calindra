using Calindra.Desafio.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Calindra.Desafio.WebApi.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ErrorModel
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Valores inválidos para a requisição!",
                    Data = new SerializableError(context.ModelState)
                });
            }
        }
    }
}
