using Android.Text;

using Java.Lang;

namespace LearnKana.Droid.MVVM.Listeners
{
    public abstract class TextWatcher : Java.Lang.Object, ITextWatcher
    {
        public bool Enabled { get; set; } = true;

        public void SetEnabled(bool enabled) => Enabled = enabled;

        public virtual void BeforeTextChanged(ICharSequence? text, int start, int count, int after)
        {
            if (Enabled) { BeforeTextChanged(text?.ToString(), start, count, after); }
        }
        public virtual void OnTextChanged(ICharSequence? text, int start, int before, int count)
        {
            if (Enabled) { OnTextChanged(text?.ToString(), start, before, count); }
        }
        public virtual void AfterTextChanged(IEditable? text)
        {
            if (Enabled) { AfterTextChanged(text?.ToString()); }
        }

        public virtual void BeforeTextChanged(string? text, int start, int count, int after) { }
        public virtual void OnTextChanged(string? text, int start, int before, int count) { }
        public virtual void AfterTextChanged(string? text) { }
    }
}
