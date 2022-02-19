using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DFMS.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> AsReadOnly<TKey, TValue>(this IDictionary<TKey, IList<TValue>> input)
        {
            return AsReadOnly(input.ToDictionary(x => x.Key, x => x.Value.ToList()));
        }

        public static IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> AsReadOnly<TKey, TValue>(this IDictionary<TKey, IEnumerable<TValue>> input)
        {
            return AsReadOnly(input.ToDictionary(x => x.Key, x => x.Value.ToList()));
        }

        public static IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> AsReadOnly<TKey, TValue>(this IDictionary<TKey, ICollection<TValue>> input)
        {
            return AsReadOnly(input.ToDictionary(x => x.Key, x => x.Value.ToList()));
        }

        public static IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> AsReadOnly<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> input)
        {
            return AsReadOnly(input.ToDictionary(x => x.Key, x => x.Value.ToList()));
        }

        public static IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> AsReadOnly<TKey, TValue>(this IDictionary<TKey, Collection<TValue>> input)
        {
            return AsReadOnly(input.ToDictionary(x => x.Key, x => x.Value.ToList()));
        }

        public static IReadOnlyDictionary<TKey, IReadOnlyCollection<TValue>> AsReadOnly<TKey, TValue>(this IDictionary<TKey, List<TValue>> input)
        {
            var readOnly = new Dictionary<TKey, IReadOnlyCollection<TValue>>();

            foreach (var entry in input)
            {
                readOnly.Add(entry.Key, new ReadOnlyCollection<TValue>(entry.Value.AsReadOnly()));
            }

            return readOnly;
        }
    }
}
