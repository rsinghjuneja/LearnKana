using Android.Views;
using LearnKana.Domain.Kana;
using LearnKana.Droid.MVVM.ViewModels;
using LearnKana.Droid.MVVM.Views.Activities;
using LearnKana.Droid.MVVM.Views.Widgets;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Fragments
{
    public class KanaChartFragment : BaseFragment, View.IOnClickListener
    {
        public static KanaChartFragment CreateInstance() => new KanaChartFragment();

        private KanaRowView[]? m_KanaRows;

        private KanaChartViewModel? m_ViewModel;
        private KanaChartViewModel ViewModel =>
            m_ViewModel ??= ViewModelService.GetViewModel<KanaChartViewModel>(Activity);

        public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_chart, container, false).ThrowIfNull();
            return view;
        }

        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            m_KanaRows = new[]
            {
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_1),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_2),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_3),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_4),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_5),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_6),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_7),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_8),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_9),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_10),
                view.RequireViewById<KanaRowView>(Resource.Id.view_kana_row_11),
            };

            KanaScript script = ViewModel.KanaScript;
            m_KanaRows?.ForEachElement((i, row) => row.SetKanaRow(App.KanaService.GetRow((KanaRowKey)i, KanaType.Standard), script));
            UpdateKanaScript(script);
        }

        public override void OnStart()
        {
            base.OnStart();
            m_KanaRows?.ForEachElement(x => x.KanaCharacters.ForEachElement(x => x.SetOnClickListener(this)));
        }
        public override void OnStop()
        {
            base.OnStop();
            m_KanaRows?.ForEachElement(x => x.KanaCharacters.ForEachElement(x => x.SetOnClickListener(null)));
        }

        public override void OnClick(View? view)
        {
            if (view is IKanaCharacterView kanaCharacterView && kanaCharacterView.KanaCharacter.HasValue)
            {
                KanaScript script = ViewModel.KanaScript;
                KanaActivity.StartActivity(Context, kanaCharacterView.KanaCharacter.Value, script);
            }
        }

        public void UpdateKanaScript(KanaScript script) =>
            m_KanaRows?.ForEachElement((i, row) => row.UpdateKanaRow(script));
    }
}