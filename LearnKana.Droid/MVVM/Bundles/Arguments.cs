using System;
using System.Collections.Generic;

namespace LearnKana.Droid.MVVM.Bundles
{
    public class Arguments
    {
        private readonly Bundle m_Bundle;

        public Arguments(Fragment fragment)
        {
            m_Bundle = fragment.RequireArguments();
        }
        public Arguments(Bundle? bundle)
        {
            ArgumentNullException.ThrowIfNull(bundle);
            m_Bundle = bundle;
        }

        public string GetString(string key, string? defaultValue = default)
        {
            string? value = m_Bundle.GetString(key, defaultValue);
            return value ?? throw new KeyNotFoundException(key);
        }
        public int GetInt(string key, int? defaultValue = default)
        {
            int value = m_Bundle.GetInt(key, defaultValue ?? int.MinValue);
            return value;
        }
        public T GetEnum<T>(string key) where T : struct, Enum
        {
            T value = m_Bundle.GetEnum<T>(key);
            return value;
        }
    }
}