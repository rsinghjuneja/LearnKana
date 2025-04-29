using System.Diagnostics.CodeAnalysis;

using Android.Views;

namespace LearnKana.Droid.MVVM.Views.Factory
{
    [method: SetsRequiredMembers]
    public readonly struct LayoutParameters(int width, int height, GravityFlags gravity = GravityFlags.Center)
    {
        public const int MatchParent = ViewGroup.LayoutParams.MatchParent;
        public const int WrapContent = ViewGroup.LayoutParams.WrapContent;

        [method: SetsRequiredMembers]
        public LayoutParameters(LayoutParameter width, LayoutParameter height, GravityFlags gravity = GravityFlags.Center)
            : this((int)width, (int)height, gravity)
        {

        }

        public int Width { get; init; } = width;
        public int Height { get; init; } = height;
        public GravityFlags Gravity { get; init; } = gravity;
    }
}
