using System.Diagnostics.CodeAnalysis;

namespace LearnKana.Domain.Kana
{
    [method: SetsRequiredMembers]
    public readonly struct KanaSet(KanaCharacter character, KanaScript script)
    {
        public required KanaCharacter Character { get; init; } = character;
        public required KanaScript Script { get; init; } = script;
    }
}