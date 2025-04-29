using Android.App;
using Android.Content;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    [Activity(MainLauncher = true)]
    public class SplashActivity : BaseActivity
    {
        protected override async void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            await App.OnCreateAsync();

            StartActivity<MainActivity>(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            Finish();
        }
    }
}