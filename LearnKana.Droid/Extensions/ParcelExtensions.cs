using System;
using System.Collections.Generic;

using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.Extensions
{
    public static class ParcelExtensions
    {
        public static Bundle ThrowIfKeyNotFound(this Bundle? bundle)
        {
            if (bundle == null)
                throw new KeyNotFoundException();
            return bundle;
        }
        public static T GetEnum<T>(this Bundle bundle, string key) where T : struct, Enum
        {
            int value = bundle.GetInt(key, int.MinValue);
            if (value == int.MinValue)
                throw new KeyNotFoundException($"Key: {key}");

            T @enum = value.ToEnum<T>();
            return @enum;
        }
        public static Bundle PutEnum<T>(this Bundle bundle, string key, T @enum) where T : struct, Enum
        {
            int value = Convert.ToInt32(@enum);
            bundle.PutInt(key, value);
            return bundle;
        }
    }
}