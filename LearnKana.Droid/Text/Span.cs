using System.Collections.Generic;

using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;

using LearnKana.Droid.Values;

namespace LearnKana.Droid.Text
{
    public class Span(SpannableString sequence)
    {
        private readonly SpannableString m_Text = sequence;
        private readonly List<Java.Lang.Object> m_Spans = [];

        public Span(string text) : this(new SpannableString(text))
        {
            
        }

        public Span SetTextSize(float size, ComplexUnitType unit = ComplexUnitType.Sp)
        {
            float value = IComplexUnit.Convert(size, unit);
            return AddSpan(new AbsoluteSizeSpan((int)value, false));
        }
        public Span SetStyle(TypefaceStyle style) => AddSpan(SpanFactory.Style(style));
        public Span SetBackgroundColor(Color color) => AddSpan(SpanFactory.BackgroundColor(color));
        public Span SetForegroundColor(Color color) => AddSpan(SpanFactory.ForegroundColor(color));
        public Span SetUnderline() => AddSpan(SpanFactory.Underline());
        public Span SetBullet(int gap = 2) => AddSpan(SpanFactory.Bullet(gap));
        public Span SetUrl(Uri uri) => AddSpan(SpanFactory.Url(uri));
        public Span SetClickable(ClickableSpan span) => AddSpan(span);
        public Span SetClickable(Action<SpannableString> action) => AddSpan(SpanFactory.Clickable(m_Text, action));
        public Span SetClickable<T>(T spannable, Action<T> action) where T : ISpannable => AddSpan(SpanFactory.Clickable(spannable, action));

        public void Apply(SpanBuilder builder)
        {
            for (int i = 0; i < m_Spans.Count; i++)
            {
                Java.Lang.Object span = m_Spans[i];
                builder.Append(m_Text, span);
            }
        }

        private Span AddSpan(Java.Lang.Object span)
        {
            m_Spans.Add(span);
            return this;
        }
    }
}
