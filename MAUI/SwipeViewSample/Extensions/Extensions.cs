using Reactive.Bindings;

namespace SwipeViewSample.Extensions
{
    internal static class Extensions
    {
        public static ReactiveCollection<T> AddRange<T>(this ReactiveCollection<T> collection, IEnumerable<T> target)
        {
            target.ToList().ForEach(x => collection.Add(x));
            return collection;
        }
    }
}
