using Android.App;
using Android.Content;

using AndroidX.ViewPager2.Widget;

using Google.Android.Material.Tabs;

using LearnKana.Domain.Kana;
using LearnKana.Droid.MVVM.Pager;
using LearnKana.Droid.MVVM.Tabs;
using LearnKana.Droid.MVVM.Views.Fragments;
using LearnKana.Droid.Utilities;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Activities
{
    [Activity]
    public class KanaActivity : BaseActivity
    {
        public static void StartActivity(Context? context, KanaCharacter character, KanaScript script)
        {
            ArgumentNullException.ThrowIfNull(context);
            Intent intent = CreateIntent<KanaActivity>(context);
            KanaBundle bundle = new KanaBundle(character, script);
            intent.PutExtra(Keys.Bundle, bundle);
            context.StartActivity(intent);
        }

        private TabLayout? m_TabLayout;
        private ViewPager2? m_ViewPager;
        private TabService? m_TabGroup;

        private KanaCharacter m_KanaCharacter;
        private KanaScript m_kanaScript;
        private KanaRow? m_KanaRow;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_kana);

            KanaSet kana = KanaBundle.FromBundle(Intent?.GetBundleExtra(Keys.Bundle), App.KanaService);

            m_KanaCharacter = kana.Character;
            m_kanaScript = kana.Script;

            m_KanaRow = App.KanaService.GetRow(m_KanaCharacter.KanaRow, KanaType.StandardTenTen);
            InitializeView();
        }

        private void InitializeView()
        {
            SetToolbarTitle(Resource.String.kana_study);
            SetToolbarSubtitle($"Row: ({m_KanaCharacter.KanaRow})");

            m_TabLayout = RequireViewById<TabLayout>(Resource.Id.tab_layout);
            m_ViewPager = RequireViewById<ViewPager2>(Resource.Id.view_pager);

            if (m_KanaRow == null)
                throw new InvalidOperationException();

            ViewPagerAdapter adapter = new ViewPagerAdapter(this);
            m_ViewPager.Adapter = adapter;
            m_TabGroup = new TabService();
            m_KanaRow.Characters.ForEachElement(x =>
            {
                adapter.AddFragment(() => KanaFragment.CreateInstance(x, m_kanaScript));
                m_TabGroup.AddTab(new Tab($"{x.KanaFromScript(m_kanaScript)} ({x.Romaji})", 0, x == m_KanaCharacter));
            });
            m_ViewPager.SetPageTransformer(new ZoomOutPageTransformer());
            m_ViewPager.OffscreenPageLimit = m_TabGroup.Count;
            m_TabGroup.Mediate(m_TabLayout, m_ViewPager, TabMode.Fixed);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            m_TabGroup?.Dispose();
        }
    }
}
