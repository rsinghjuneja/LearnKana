using System;

using AndroidX.Fragment.App;

namespace LearnKana.Droid.MVVM.Listeners
{
    public class FragmentOnAttachListener<T>(Action<FragmentManager, Fragment> callback) : Java.Lang.Object, IFragmentOnAttachListener where T : Fragment
    {
        private readonly Action<FragmentManager, Fragment> m_Callback = callback;

        public void OnAttachFragment(FragmentManager manager, Fragment fragment)
        {
            m_Callback?.Invoke(manager, fragment);
        }
    }
}
