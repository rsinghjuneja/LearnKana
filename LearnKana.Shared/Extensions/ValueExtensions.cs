using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace LearnKana.Shared.Extensions
{
    public static class ValueExtensions
    {
        public static T ToPercent<T>(this T amount, T total) where T : INumber<T>
            => amount * T.CreateChecked(100) / total;
        public static string ToPercentString<T>(this T amount, T total) where T : INumber<T>
        {
            T percent = ToPercent<T>(amount, total);
            return $"{percent}%";
        }
        public static T ToEnum<T>(this int value) where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), value))
                throw new InvalidOperationException($"The value ({value}) is not defined in the enum ({typeof(T).Name})");
            T @enum = (T)Enum.ToObject(typeof(T), value);
            return @enum;
        }
        public static string ToFullNameString<T>(this T @enum) where T : struct, Enum
        {
            string type = typeof(T).Name;
            string value = @enum.ToString();
            return $"{type}.{value}";
        }
        public static bool TryGetValue<T>(this T? nullable, [NotNullWhen(true)] out T value) where T : struct
        {
            value = nullable.GetValueOrDefault();
            return nullable.HasValue;
        }
    }
}