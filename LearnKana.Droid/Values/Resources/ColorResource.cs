using Android.App;
using Android.Graphics;

using AndroidX.Core.Content;

namespace LearnKana.Droid.Values.Resources
{
    public readonly struct ColorResource(int id) : IResource
    {
        public int Id { get; } = id;
        public Color ToColor() => new Color(ContextCompat.GetColor(Application.Context, Id));

        public static implicit operator ColorResource(int id) => new ColorResource(id);
        public static implicit operator Color(ColorResource resource) => resource.ToColor();
    }
}
