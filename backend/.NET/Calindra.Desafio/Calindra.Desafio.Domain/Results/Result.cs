namespace Calindra.Desafio.Domain.Results
{
    public static class Result
    {
        public static MethodResult Ok()
            => new();

        public static MethodResult Ok(object data)
            => new(data);

        public static MethodResult Fail(string propertyName, params string[] messages)
            => new(propertyName, messages);

        public static MethodResult FailWithData(string propertyName, object data, params string[] messages)
        {
            return new MethodResult(propertyName, messages)
            {
                Data = data
            };
        }

        public static MethodResult Parse<T>(MethodResult<T> methodResult)
        {
            var result = new MethodResult(methodResult.Data);
            if (methodResult.Failure)
                result.Add(methodResult.Failures);
            return result;
        }
    }
}
