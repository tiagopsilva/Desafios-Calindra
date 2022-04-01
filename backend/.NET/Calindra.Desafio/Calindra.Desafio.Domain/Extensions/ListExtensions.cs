using System.Collections.Generic;

namespace Calindra.Desafio.Domain.Extensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> otherList)
        {
            foreach (var item in otherList)
                list.Add(item);
        }
    }
}
