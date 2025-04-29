using System;
using System.Collections.Generic;

using LearnKana.Domain.Kana;

namespace LearnKana.Droid.Utilities
{
    public static class Strings
    {
        public static string GetkanaScript(KanaScript type) => type switch
        {
            KanaScript.Hiragana => GetString(Resource.String.hiragana),
            KanaScript.Katakana => GetString(Resource.String.katakana),
            _ => throw new NotImplementedException(),
        };
        public static string GetkanaScriptLowerCase(KanaScript type) => type switch
        {
            KanaScript.Hiragana => GetString(Resource.String.hiragana_lower_case),
            KanaScript.Katakana => GetString(Resource.String.katakana_lower_case),
            _ => throw new NotImplementedException(),
        };
        public static string GetString(int resourceId)
        {
            string value = App.Context.Resources?.GetString(resourceId)
                ?? throw new KeyNotFoundException($"{resourceId}");
            return value;
        }
    }
}
