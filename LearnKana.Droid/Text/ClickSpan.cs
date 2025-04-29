using Android.Text;
using Android.Text.Style;
using Android.Views;

namespace LearnKana.Droid.Text
{
    public class ClickSpan(string text, Action<string> action) : ClickableSpan
    {
        private readonly Action<string> m_Action = action;
        private readonly string m_Text = text;
        public override void OnClick(View view) => m_Action?.Invoke(m_Text);
    }
    public class ClickSpan<T>(T spannable, Action<T> action) : ClickableSpan where T : Java.Lang.ICharSequence
    {
        private readonly Action<T> m_Action = action;
        private readonly T m_Text = spannable;
        public override void OnClick(View view) => m_Action?.Invoke(m_Text);
    }
}