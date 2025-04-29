using Android.App;

using AndroidX.Core.Content;

namespace LearnKana.Droid.Values.Resources
{
    public readonly struct StringResource(int id) : IResource
    {
        public int Id { get; } = id;
        public override string ToString() => ContextCompat.GetString(Application.Context, Id);

        public static implicit operator StringResource(int id) => new StringResource(id);
        public static implicit operator string(StringResource resource) => resource.ToString();
    }
}