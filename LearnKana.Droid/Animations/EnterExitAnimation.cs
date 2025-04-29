using System.Diagnostics.CodeAnalysis;

namespace LearnKana.Droid.Animations
{
    [method: SetsRequiredMembers]
    public readonly struct EnterExitAnimation(int enter, int exit)
    {
        public required int Enter { get; init; } = enter;
        public required int Exit { get; init; } = exit;
    }
}
