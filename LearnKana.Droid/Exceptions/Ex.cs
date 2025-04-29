using System;

namespace LearnKana.Droid.Exceptions
{
    public static class Ex
    {
        private const string Empty = "";

        public static void ThrowIfFalse<T>(bool condition, string? message = Empty, string? paramName = Empty) where T : ArgumentException
        {
            if (!condition)
                throw Activator.CreateInstance(typeof(T), message, paramName) as T ?? new ArgumentException(message, paramName);
        }
    }
}
