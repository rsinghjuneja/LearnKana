using Android.App;

using AndroidX.Lifecycle;

using LearnKana.Droid.MVVM.ViewModels;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    [Activity]
    public class QuizLauncherActivity : ViewModelActivity<QuizSettingViewModel>
    {
        protected override ViewModelProvider.IFactory GetViewModelFactory()
        {
            throw new NotImplementedException();
        }
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }
    }
}
