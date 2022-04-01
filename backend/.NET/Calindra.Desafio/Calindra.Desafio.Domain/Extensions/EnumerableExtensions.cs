using System.Collections.Generic;
using System.Linq;

namespace Calindra.Desafio.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Count() == 0;
        }
    }
}
