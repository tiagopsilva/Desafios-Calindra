namespace Calindra.Desafio.WebApi.Models
{
    public class ErrorModel
    {
        public ErrorModel() { }

        public ErrorModel(int statusCode, string message, object details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
