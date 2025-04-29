using Android.App;
using Android.Content;
using Android.Views;

using AndroidX.Activity.Result;
using AndroidX.Activity.Result.Contract;
using AndroidX.AppCompat.App;
using AndroidX.Fragment.App;
using LearnKana.Droid.MVVM.Views.Toolbars;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    public abstract class BaseActivity : AppCompatActivity, IActivityResultCallback, IFragmentManagerProvider, IFragmentResultListener
    {
        FragmentManager IFragmentManagerProvider.GetFragmentManager() => SupportFragmentManager;

        protected ActivityToolbarView? Toolbar { get; private set; }

        public static void StartActivity<T>(Context? context, ActivityFlags flags = 0) where T : Activity
        {
            StartActivity(context, typeof(T), flags);
        }
        public static void StartActivity(Context? context, Type type, ActivityFlags flags = 0)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            Intent intent = CreateIntent(context, type, flags);
            context.StartActivity(intent);
        }
        public static Intent CreateIntent<T>(Context? context, ActivityFlags flags = 0) where T : Activity
        {
            return CreateIntent(context, typeof(T), flags);
        }
        public static Intent CreateIntent(Context? context, Type type, ActivityFlags flags = 0)
        {
            ArgumentNullException.ThrowIfNull(context,nameof(context));
            Intent intent = new Intent(context, type);
            intent.SetFlags(flags);
            return intent;
        }

        public void StartActivity<T>(ActivityFlags flags = 0) where T : Activity
        {
            StartActivity<T>(this, flags);
        }
        public Intent CreateIntent<T>(ActivityFlags flags = 0) where T : Activity
        {
            return CreateIntent<T>(this, flags);
        }

        public override void SetContentView(int layoutResID)
        {
            base.SetContentView(layoutResID);

            Toolbar = RequireViewById<ActivityToolbarView>(Resource.Id.activity_toolbar);
            Toolbar.SetSupportActionBar(this, displayHomeAsUpEnabled: this is not MainActivity);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackArrowPressed();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected ActivityResultLauncher RegisterForActivityResult()
        {
            ActivityResultLauncher launcher = RegisterForActivityResult(new ActivityResultContracts.StartActivityForResult(), this);
            return launcher;
        }
        protected ActivityResultLauncher RegisterForActivityResult(ActivityResultContract contract)
        {
            ActivityResultLauncher launcher = RegisterForActivityResult(contract, this);
            return launcher;
        }

        public void SetToolbarTitle(int resourceId) => Toolbar?.SetToolbarTitle(resourceId);
        public void SetToolbarTitle(string title) => Toolbar?.SetToolbarTitle(title);
        public void SetToolbarSubtitle(int resourceId) => Toolbar?.SetToolbarSubtitle(resourceId);
        public void SetToolbarSubtitle(string subtitle) => Toolbar?.SetToolbarSubtitle(subtitle);
        protected virtual void OnBackArrowPressed()
        {
            OnNavigateBack();
        }
        public override void OnBackPressed()
        {
            OnNavigateBack();
        }
        protected virtual void OnNavigateBack()
        {
            OnBackPressedDispatcher.OnBackPressed();
        }

        public void OnActivityResult(Java.Lang.Object? result)
        {
            if (result is ActivityResult activityResult)
                OnActivityResult(activityResult.Data, (Result)activityResult.ResultCode);
        }

        protected virtual void OnActivityResult(Intent? intent, Result result) { }
        public virtual void OnFragmentResult(string key, Bundle result) { }
    }
}