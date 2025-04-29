using System;

using Android.Views;

namespace LearnKana.Droid.MVVM.Listeners
{
    public class OnClickListener(Action<View> callback) : Java.Lang.Object, View.IOnClickListener
    {
        private readonly Action<View> m_Callback = callback;

        public void OnClick(View? view)
        {
            if (view != null)
                m_Callback?.Invoke(view);
        }
    }
    public class OnClickListener<T>(Action<T> callback) : Java.Lang.Object, View.IOnClickListener where T : View
    {
        private readonly Action<T> m_Callback = callback;

        public void OnClick(View? view)
        {
            if (view is T casted)
                m_Callback?.Invoke(casted);
        }
    }
}