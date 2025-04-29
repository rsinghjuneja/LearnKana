using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;

using LearnKana.Droid.Values;

namespace LearnKana.Droid.Text
{
    public class SpanFactory
    {
        public static AbsoluteSizeSpan TextSize(float size, ComplexUnitType unit = ComplexUnitType.Sp)
        {
            float value = IComplexUnit.Convert(size, unit);
            return new AbsoluteSizeSpan((int)value, false);
        }
        public static StyleSpan Style(TypefaceStyle style) => new StyleSpan(style);
        public static BackgroundColorSpan BackgroundColor(Color color) => new BackgroundColorSpan(color);
        public static ForegroundColorSpan ForegroundColor(Color color) => new ForegroundColorSpan(color);
        public static UnderlineSpan Underline() => new UnderlineSpan();
        public static BulletSpan Bullet(int gap = 2) => new BulletSpan(gap);
        public static URLSpan Url(Uri uri) => new URLSpan(uri.AbsoluteUri);
        public static ClickableSpan Clickable(string text, Action<string> action) => new ClickSpan(text, action);
        public static ClickableSpan Clickable<T>(T spannable, Action<T> action) where T : Java.Lang.ICharSequence => new ClickSpan<T>(spannable, action);
    }
}
