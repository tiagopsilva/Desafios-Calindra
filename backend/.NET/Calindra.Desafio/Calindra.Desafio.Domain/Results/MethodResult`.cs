using Calindra.Desafio.Domain.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Calindra.Desafio.Domain.Results
{
    public class MethodResult<T>
    {
        private readonly IList<Failure> _failures = new List<Failure>();

        public MethodResult() { }

        public MethodResult(T data)
            => Data = data;

        public MethodResult(string propertyName, params string[] messages)
            => Add(propertyName, messages);

        public MethodResult(int errorCode, string propertyName, params string[] messages)
        {
            ErrorCode = errorCode;
            Add(propertyName, messages);
        }

        public IReadOnlyCollection<Failure> Failures
            => new ReadOnlyCollection<Failure>(_failures);

        public bool Success => _failures.IsEmpty();
        public bool Failure => _failures.Count > 0;

        public T Data { get; set; }
        public int? ErrorCode { get; set; }

        public void Add(string propertyName, params string[] messages)
        {
            messages = messages.Where(m => !m.IsEmpty()).ToArray();
            if (messages.IsEmpty())
                messages = new[] { "Is invalid" };

            var failure = _failures.SingleOrDefault(f => f.Property.Equals(propertyName?.Trim(), System.StringComparison.InvariantCultureIgnoreCase));
            if (failure != null)
            {
                failure.Add(messages);
            }
            else
            {
                _failures.Add(new Failure(propertyName, messages));
            }
        }
    }
}
