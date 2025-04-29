namespace LearnKana.Shared.Extensions
{
    public static class CollectionExtensions
    {
        public static void ForEachElement<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action.Invoke(item);
        }
        public static void ForEachElement<T>(this IEnumerable<T> collection, Action<int, T> action)
        {
            int count = 0;
            foreach (var item in collection)
            {
                action.Invoke(count, item);
                count++;
            }
        }
        public static void ForEachKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey> action)
        {
            foreach (var item in dictionary)
                action.Invoke(item.Key);
        }
        public static void ForEachValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TValue> action)
        {
            foreach (var item in dictionary)
                action.Invoke(item.Value);
        }
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Dictionary<TKey, TValue> other) where TKey : notnull where TValue : notnull
        {
            foreach (var kvp in other)
                dictionary.Add(kvp.Key, kvp.Value);
        }
    }
}