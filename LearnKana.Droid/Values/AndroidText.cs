using System.Diagnostics.CodeAnalysis;

using Android.Text;

using LearnKana.Droid.Values.Resources;

namespace LearnKana.Droid.Values
{
    public readonly struct AndroidText
    {
        [SetsRequiredMembers]
        public AndroidText(string value) => Value = new Java.Lang.String(value);
        [SetsRequiredMembers]
        public AndroidText(int resourceId) : this(new Java.Lang.String(new StringResource(resourceId))) { }
        [SetsRequiredMembers]
        public AndroidText(Java.Lang.ICharSequence value) => Value = value;

        public required Java.Lang.ICharSequence Value { get; init; }

        public bool IsFormatted([NotNullWhen(true)] out ISpanned? spanned)
        {
            spanned = Value as ISpanned;
            return spanned != null;
        }
        public override string ToString() => Value.ToString();

        public static implicit operator AndroidText(string text) => new AndroidText(text);
        public static implicit operator AndroidText(SpannableString sequence) => new AndroidText(sequence);
        public static implicit operator AndroidText(int resourceId) => new AndroidText(resourceId);
    }
}