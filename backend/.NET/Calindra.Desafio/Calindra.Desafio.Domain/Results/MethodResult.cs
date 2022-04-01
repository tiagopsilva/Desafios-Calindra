using System.Collections.Generic;
using System.Linq;

namespace Calindra.Desafio.Domain.Results
{
    public class MethodResult : MethodResult<object>
    {
        public MethodResult()
            : base() { }

        public MethodResult(object data)
            : base(data) { }

        public MethodResult(string propertyName, params string[] messages)
            : base(propertyName, messages) { }

        public void Add(IReadOnlyCollection<Failure> failures)
        {
            foreach (var failure in failures)
                Add(failure.Property, failure.Messages.ToArray());
        }
    }
}
