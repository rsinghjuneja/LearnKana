using System;

using Android.Content;

using AndroidX.Preference;

using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.Services
{
    public class Prefs
    {
        private const int DefaultIntValue = int.MinValue;
        private readonly ISharedPreferences m_PreferenceManager;

        public Prefs(Context context)
        {
            m_PreferenceManager = PreferenceManager.GetDefaultSharedPreferences(context)
                ?? throw new NullReferenceException(nameof(m_PreferenceManager));
        }

        public bool StoreBool(string key, bool value)
        {
            ISharedPreferencesEditor? editor = m_PreferenceManager.Edit();
            bool? result = editor?
                .PutBoolean(key, value)?
                .Commit();
            return result ?? false;
        }
        public bool GetBool(string key, bool? defaultValue = null)
        {
            bool value = m_PreferenceManager.GetBoolean(key, defaultValue ?? default);
            return value;
        }

        public bool StoreInt(string key, int value)
        {
            ISharedPreferencesEditor? editor = m_PreferenceManager.Edit();
            bool? result = editor?
                .PutInt(key, value)?
                .Commit();
            return result ?? false;
        }
        public int GetInt(string key, int? defaultValue = null)
        {
            int value = m_PreferenceManager.GetInt(key, defaultValue ?? DefaultIntValue);
            return value;
        }

        public bool StoreEnum<T>(string key, T @enum) where T : struct, Enum
        {
            int value = Convert.ToInt32(@enum);
            return StoreInt(key, value);
        }
        public T GetEnum<T>(string key, T? defaultValue = null) where T : struct, Enum
        {
            int value = GetInt(key);
            if (defaultValue.HasValue && value == DefaultIntValue)
                return defaultValue.Value;
            return value.ToEnum<T>();
        }
    }
}