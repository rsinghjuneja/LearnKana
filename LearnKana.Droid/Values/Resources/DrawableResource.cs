using Android.App;
using Android.Graphics.Drawables;

using AndroidX.Core.Content;

namespace LearnKana.Droid.Values.Resources
{
    public readonly struct DrawableResource(int id) : IResource
    {
        public int Id { get; } = id;
        public Drawable? ToDrawable() => ContextCompat.GetDrawable(Application.Context, Id);

        public static implicit operator DrawableResource(int id) => new DrawableResource(id);
        public static implicit operator Drawable?(DrawableResource resource) => resource.ToDrawable();

    }
}
