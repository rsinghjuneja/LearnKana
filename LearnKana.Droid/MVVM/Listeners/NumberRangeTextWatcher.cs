using LearnKana.Droid.Values;

namespace LearnKana.Droid.MVVM.Listeners
{
    public class NumberRangeTextWatcher(Range<int> range, Action<bool, int> callback) : TextWatcher
    {
        private readonly Action<bool, int> m_Callback = callback;

        public Range<int> Range { get; } = range;

        public override void AfterTextChanged(string? text)
        {
            if (int.TryParse(text, out int value))
            {
                bool valid = Range.Contains(value);
                m_Callback?.Invoke(valid, value);
            }
            else
                m_Callback?.Invoke(true, Range.Start);
        }
    }
}
