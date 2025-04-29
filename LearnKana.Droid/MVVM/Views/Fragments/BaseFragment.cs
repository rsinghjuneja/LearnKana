using Android.Views;

using LearnKana.Droid.Utilities;
using LearnKana.Provider;

namespace LearnKana.Droid.MVVM.Views.Fragments
{
    public abstract class BaseFragment : Fragment, View.IOnClickListener, IFragmentManagerProvider
    {
        public const string FragmentViewCreated = "fragment_view_created";

        protected static KanaService KanaService => App.KanaService;
        protected static KanaAudioPlayer KanaAudioPlayer => App.KanaAudioPlayer;

        private Bundle? m_Result;
        protected Bundle ResultData => m_Result ??= new Bundle();

        protected void OnFragmentViewCreated()
        {
            FragmentManager manager = GetFragmentManager();
            manager.SetFragmentResult(FragmentViewCreated, ResultData);
        }
        protected void SetResult(string key, Bundle bundle)
        {
            FragmentManager manager = GetFragmentManager();
            manager.SetFragmentResult(key, bundle);
        }
        public virtual FragmentManager GetFragmentManager()
        {
            ArgumentNullException.ThrowIfNull(Activity?.SupportFragmentManager);
            return Activity.SupportFragmentManager;
        }

        public virtual void OnClick(View? view) { }
    }
}