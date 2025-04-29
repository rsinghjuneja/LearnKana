using System.Reflection;

namespace LearnKana.Shared
{
    public class TypeFactory
    {
        private static readonly Dictionary<Type, ConstructorInfo> m_Cache = [];
        public static T Create<T>(object[] parameters) where T : notnull
        {
            if (!m_Cache.TryGetValue(typeof(T), out ConstructorInfo? constructor))
                constructor = typeof(T).GetConstructor(parameters.Select(x => x.GetType()).ToArray());

            if (constructor != null)
                return (T)constructor.Invoke(parameters);
            throw new ArgumentException("Constructor for type not found", typeof(T).GetType().Name);
        }
    }
}