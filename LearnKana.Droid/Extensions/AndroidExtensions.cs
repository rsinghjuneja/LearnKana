using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Widget;

using AndroidX.Core.Content;

namespace LearnKana.Droid.Extensions
{
    public static class AndroidExtensions
    {
        public static void ShowToast(this Context? context, string text, ToastLength length = ToastLength.Short)
        {
            Toast.MakeText(context, text, length)?.Show();
        }
        public static void ShowToast(this Context? context, int resourceId, ToastLength length = ToastLength.Short)
        {
            string? text = context?.GetString(resourceId);
            Toast.MakeText(context, text, length)?.Show();
        }
        public static void ShowToast(this Context? context, SpannableString text, ToastLength length = ToastLength.Short)
        {
            Toast.MakeText(context, text, length)?.Show();
        }
        public static Drawable? GetDrawableCompat(this Context? context, int resourceId)
        {
            Drawable? drawable = null;
            if (context != null)
                drawable = ContextCompat.GetDrawable(context, resourceId);
            return drawable;
        }
        public static T GetSystemService<T>(this Context context, string name) where T : Java.Lang.Object
        {
            T? service = context.GetSystemService(name) as T;
            return service.ThrowIfNull();
        }

        [DebuggerStepThrough]
        [return: NotNullIfNotNull(nameof(obj))]
        public static T ThrowIfNull<T>(this T? obj, string? message = "") where T : class
        {
            if (obj is null)
                throw new NullReferenceException(message ?? $"{nameof(obj)} was null");
            return obj;
        }
        public static Java.Lang.Class ToJavaClass(this Type type) => Java.Lang.Class.FromType(type);
    }
}