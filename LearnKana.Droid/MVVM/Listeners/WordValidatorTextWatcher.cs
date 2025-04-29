using System.Collections.Generic;

namespace LearnKana.Droid.MVVM.Listeners
{
    public class WordValidatorTextWatcher(HashSet<string> items, Action<bool> callback) : TextWatcher
    {
        private readonly HashSet<string> m_Items = items;
        private readonly Action<bool> m_Callback = callback;

        public WordValidatorTextWatcher(IEnumerable<string> items, Action<bool> callback) : this(new HashSet<string>(items), callback)
        {
        }

        public override void AfterTextChanged(string? text)
        {
            bool valid = text != null && m_Items.Contains(text);
            m_Callback?.Invoke(valid);
        }
    }
}