using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;


namespace LearnKana.Droid.Text
{
    public class SpanBuilder(SpannableStringBuilder builder)
    {
        private readonly SpannableStringBuilder m_Builder = builder;

        public SpanBuilder() : this(new SpannableStringBuilder()) { }

        public int Length => m_Builder.Length();
        public SpannableString Build() => SpannableString.ValueOf(m_Builder).ThrowIfNull("Tried to build an empty span.");

        public SpanBuilder NewLine(int lines = 1)
        {
            if (Length == 0)
                return this;

            for (int i = 0; i < lines; i++)
            {
                m_Builder.Append(Environment.NewLine);
            }
            return this;
        }

        public SpanBuilder Append(string text)
        {
            m_Builder.Append(text);
            return this;
        }
        public SpanBuilder Append(Java.Lang.ICharSequence spannable)
        {
            m_Builder.Append(spannable);
            return this;
        }
        public SpanBuilder Append(Java.Lang.ICharSequence? spannable, Java.Lang.Object span) => Append(m_Builder.Length(), spannable ?? new SpannableString(""), span);
        public SpanBuilder Append(string text, Java.Lang.Object span) => Append(m_Builder.Length(), new Java.Lang.String(text), span);
        public SpanBuilder Append(Span span)
        {
            span.Apply(this);
            return this;
        }
        private SpanBuilder Append(int index, Java.Lang.ICharSequence sequence, Java.Lang.Object span)
        {
            m_Builder.Append(sequence);
            m_Builder.SetSpan(span, index, index + sequence.Length(), 0);
            return this;
        }

        public SpanBuilder SetTextSize(string text, float size, ComplexUnitType unit = ComplexUnitType.Sp) => Append(text, SpanFactory.TextSize(size, unit));
        public SpanBuilder SetStyle(string text, TypefaceStyle style) => Append(text, SpanFactory.Style(style));
        public SpanBuilder SetBackgroundColor(string text, Color color) => Append(text, SpanFactory.BackgroundColor(color));
        public SpanBuilder SetForegroundColor(string text, Color color) => Append(text, SpanFactory.ForegroundColor(color));
        public SpanBuilder SetUnderline(string text) => Append(text, SpanFactory.Underline());
        public SpanBuilder SetBullet(string text, int gap = 2) => Append(text, SpanFactory.Bullet(gap));
        public SpanBuilder SetUrl(string text, Uri uri) => Append(text, SpanFactory.Url(uri));
        public SpanBuilder SetClickable(string text, ClickableSpan span) => Append(text, span);
        public SpanBuilder SetClickable(string text, Action<string> action) => Append(text, SpanFactory.Clickable(text, action));

        public SpanBuilder SetTextSize<T>(T? text, float size, ComplexUnitType unit = ComplexUnitType.Sp) where T : Java.Lang.ICharSequence => Append(text, SpanFactory.TextSize(size, unit));
        public SpanBuilder SetStyle<T>(T? text, TypefaceStyle style) where T : Java.Lang.ICharSequence => Append(text, SpanFactory.Style(style));
        public SpanBuilder SetBackgroundColor<T>(T? text, Color color) where T : Java.Lang.ICharSequence => Append(text, SpanFactory.BackgroundColor(color));
        public SpanBuilder SetForegroundColor<T>(T? text, Color color) where T : Java.Lang.ICharSequence => Append(text, SpanFactory.ForegroundColor(color));
        public SpanBuilder SetUnderline<T>(T? text) where T : Java.Lang.ICharSequence => Append(text, SpanFactory.Underline());
        public SpanBuilder SetBullet<T>(T? text, int gap = 2) where T : Java.Lang.ICharSequence => Append(text, SpanFactory.Bullet(gap));
        public SpanBuilder SetUrl<T>(T? text, Uri uri) where T : Java.Lang.ICharSequence => Append(text, SpanFactory.Url(uri));
        public SpanBuilder SetClickable<T>(T? text, ClickableSpan span) where T : Java.Lang.ICharSequence => Append(text, span);
        public SpanBuilder SetClickable<T>(T text, Action<T> action) where T : Java.Lang.ICharSequence => Append(text, SpanFactory.Clickable(text, action));
    }
}