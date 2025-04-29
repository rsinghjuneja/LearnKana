using System;

using LearnKana.Domain.Kana;
using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.Extensions
{
    public static class DomainExtensions
    {
        public static string ToFullString(this KanaCharacter character, KanaScript script) => script switch
        {
            KanaScript.Hiragana => $"{character.Hiragana} - {character.Romaji} ({Strings.GetkanaScriptLowerCase(script)})",
            KanaScript.Katakana => $"{character.Katakana} - {character.Romaji} ({Strings.GetkanaScriptLowerCase(script)})",
            _ => throw new NotImplementedException()
        };
        public static string ToShortString(this KanaCharacter character, KanaScript script) => script switch
        {
            KanaScript.Hiragana => $"{character.Hiragana} ({character.Romaji})",
            KanaScript.Katakana => $"{character.Katakana} ({character.Romaji})",
            _ => throw new NotImplementedException()
        };
    }
}
