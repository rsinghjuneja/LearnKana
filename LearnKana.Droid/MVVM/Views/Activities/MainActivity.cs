using Android.App;
using Android.Views;

using AndroidX.Lifecycle;
using LearnKana.Domain.Kana;
using LearnKana.Droid.MVVM.ViewModels;
using LearnKana.Droid.MVVM.Views;
using LearnKana.Droid.MVVM.Views.Fragments;
using LearnKana.Droid.Text;
using LearnKana.Droid.Values.Resources;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    [Activity(Label = "@string/app_name")]
    public class MainActivity : ViewModelActivity<KanaChartViewModel>
    {
        private const int MenuItemTextSize = 12;
        private FragmentService<int>? m_FragmentService;

        protected override ViewModelProvider.IFactory GetViewModelFactory()
            => new KanaChartViewModel.Factory(App.ApplicationRepository);
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(_Microsoft.Android.Resource.Designer.ResourceConstant.Layout.activity_main);
            SetToolbarTitle(_Microsoft.Android.Resource.Designer.ResourceConstant.String.app_name);

            InitializeFragment();
        }
        public override bool OnCreateOptionsMenu(IMenu? menu)
        {
            MenuInflater.Inflate(_Microsoft.Android.Resource.Designer.ResourceConstant.Menu.menu_main, menu);
            for (int i = 0; i < menu?.Size(); i++)
            {
                IMenuItem? item = menu?.GetItem(i);
                if (item?.ItemId != _Microsoft.Android.Resource.Designer.ResourceConstant.Id.action_script)
                    item?.SetTitle(new SpanBuilder().SetTextSize(item.TitleFormatted, MenuItemTextSize).Build());
                else
                    UpdateKanaMenuItem(item);
            }

            return menu != null;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case _Microsoft.Android.Resource.Designer.ResourceConstant.Id.action_script:
                    if (ViewModel.IsSaving)
                        return false;
                    ToggleKanaScript();
                    UpdateKanaMenuItem(item);
                    return true;
                case _Microsoft.Android.Resource.Designer.ResourceConstant.Id.action_quiz:
                    KanaScript script = ViewModel.KanaScript;
                    QuizActivity.StartActivity(this, script);
                    return true;
                case _Microsoft.Android.Resource.Designer.ResourceConstant.Id.action_settings:
                    StartActivity<QuizSettingsActivity>();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void InitializeFragment()
        {
            m_FragmentService = new FragmentService<int>(this, RequireViewById<View>(_Microsoft.Android.Resource.Designer.ResourceConstant.Id.fragment_container_view))
                .AddFragmentFactory(0, KanaChartFragment.CreateInstance);
            m_FragmentService.SetFragment(0);
        }

        private async void ToggleKanaScript()
        {
            if (ViewModel.IsSaving)
                return;

            await ViewModel.ToggleKanaScriptAsync();

            ArgumentNullException.ThrowIfNull(m_FragmentService);
            m_FragmentService.GetCurrentFragment<KanaChartFragment>()
                .UpdateKanaScript(ViewModel.KanaScript);
        }
        private void UpdateKanaMenuItem(IMenuItem? item)
        {
            KanaScript script = ViewModel.KanaScript;
            switch (script)
            {
                case KanaScript.Hiragana:
                    item?.SetTitle(new SpanBuilder().SetTextSize(new StringResource(_Microsoft.Android.Resource.Designer.ResourceConstant.String.hiragana), MenuItemTextSize).Build());
                    break;
                case KanaScript.Katakana:
                    item?.SetTitle(new SpanBuilder().SetTextSize(new StringResource(_Microsoft.Android.Resource.Designer.ResourceConstant.String.katakana), MenuItemTextSize).Build());
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}