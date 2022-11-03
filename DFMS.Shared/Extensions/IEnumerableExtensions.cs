using System.Collections.Generic;
using System.Linq;

namespace DFMS.Shared.Extensions
{
    public static class IEnumerableExtensions
    {
        public static int GetCount<T>(this IEnumerable<T> source) => source.TryGetNonEnumeratedCount(out int count) ? count : source.Count();
        public static bool HasAny<T>(this IEnumerable<T> first, IEnumerable<T> second) => first.Intersect(second).Any();
        public static bool HasAll<T>(this IEnumerable<T> first, IEnumerable<T> second) => first.Intersect(second).GetCount() == second.GetCount();
    }
}
