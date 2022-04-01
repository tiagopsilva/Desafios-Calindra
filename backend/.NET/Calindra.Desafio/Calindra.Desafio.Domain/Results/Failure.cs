using Calindra.Desafio.Domain.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Calindra.Desafio.Domain.Results
{
    public class Failure
    {
        private readonly IList<string> _messages = new List<string>();

        protected Failure()
            => _messages = new List<string>();

        public Failure(params string[] messages)
            : this() => Add(string.Empty, messages);

        public Failure(string propertyName, params string[] messages)
            : this() => Add(propertyName, messages);

        public string Property { get; set; }

        public IReadOnlyCollection<string> Messages
            => new ReadOnlyCollection<string>(_messages);

        public void Add(string propertyName, params string[] messages)
        {
            Property = propertyName?.Trim();
            Add(messages);
        }

        public void Add(params string[] messages)
        {
            messages = messages?.Where(m => !m.IsEmpty()).ToArray();
            if (messages.IsEmpty())
                return;

            foreach (var message in messages)
                if (!_messages.Any(m => m.ToUpper().Equals(message, System.StringComparison.InvariantCultureIgnoreCase)))
                    _messages.Add(message);
        }
    }
}
