using Android.Graphics;
using Android.Views;
using Android.Widget;

using LearnKana.Domain.Kana;
using LearnKana.Droid.MVVM.Views.Activities;
using LearnKana.Droid.MVVM.Views.Dialogs;
using LearnKana.Droid.MVVM.Views.Popups;
using LearnKana.Droid.Text;
using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Views.Fragments
{
    public class KanaFragment : BaseFragment, View.IOnClickListener
    {
        public static KanaFragment CreateInstance(KanaCharacter kana, KanaScript type)
        {
            Bundle bundle = new KanaBundle(kana, type).ToBundle();
            KanaFragment fragment = new KanaFragment
            {
                Arguments = bundle
            };
            return fragment;
        }

        private View? m_ButtonAudio;
        private TextView? m_TextViewKana;
        private TextView? m_TextViewContent;
        private TextView? m_TextViewTenTen;
        private Button? m_ButtonStrokes;

        private KanaCharacter m_KanaCharacter;
        private KanaScript m_kanaScript;

        public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate<View>(Resource.Layout.fragment_kana, container);
            return view;
        }

        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            m_ButtonAudio = view.RequireViewById<View>(Resource.Id.button_audio);
            m_TextViewKana = view.RequireViewById<TextView>(Resource.Id.textview_kana);
            m_TextViewContent = view.RequireViewById<TextView>(Resource.Id.textview_content);
            m_TextViewTenTen = view.RequireViewById<TextView>(Resource.Id.textview_tenten);
            m_ButtonStrokes = view.RequireViewById<Button>(Resource.Id.button_strokes);

            Arguments arguments = new Arguments(this);

            string romaji = KanaBundle.GetRomajiKey(arguments);
            m_kanaScript = KanaBundle.GetKanaScript(arguments);

            m_KanaCharacter = KanaService.KanaSyllabary[romaji];

            m_TextViewKana.SetText(m_KanaCharacter.KanaFromScript(m_kanaScript));
            m_TextViewContent.SetText(m_KanaCharacter.ToFullString(m_kanaScript));
            m_TextViewContent.SetTypeface(Resource.Font.noto_sans_jp_bold, TypefaceStyle.Bold);

            SpanBuilder builder = new SpanBuilder();

            if (KanaService.DakutenSyllabary.TryGetValue(romaji, out KanaCharacter dakuten))
            {
                builder
                    .SetBullet(" ")
                    .Append("This character has a ")
                    .SetClickable(Strings.GetString(Resource.String.dakuten), (x) =>
                        DefinitionPopup.Show(Context, Strings.GetString(Resource.String.dakuten), Strings.GetString(Resource.String.definition_dakuten), m_TextViewTenTen))
                    .Append(" counterpart: ")
                    .SetClickable(dakuten.ToShortString(m_kanaScript), (x) => KanaActivity.StartActivity(Context, dakuten, m_kanaScript))
                    .Append(".");
            }
            if (KanaService.HandakutenSyllabary.TryGetValue(romaji, out KanaCharacter handakuten))
            {
                builder
                    .NewLine()
                    .SetBullet(" ")
                    .Append("This character has a ")
                    .SetClickable(Strings.GetString(Resource.String.handakuten), (x) =>
                        DefinitionPopup.Show(Context, Strings.GetString(Resource.String.handakuten), Strings.GetString(Resource.String.definition_handakuten), m_TextViewTenTen))
                    .Append(" counterpart: ")
                    .SetClickable(handakuten.ToShortString(m_kanaScript), (x) => KanaActivity.StartActivity(Context, handakuten, m_kanaScript))
                    .Append(".");
            }

            if (builder.Length > 0)
            {
                m_TextViewTenTen.SetSpan(builder);
            }
        }

        public override void OnStart()
        {
            base.OnStart();
            m_ButtonAudio?.SetOnClickListener(this);
            m_ButtonStrokes?.SetOnClickListener(this);
        }

        public override void OnStop()
        {
            base.OnStop();
            m_ButtonAudio?.SetOnClickListener(null);
            m_ButtonStrokes?.SetOnClickListener(null);
        }

        public override void OnClick(View? view)
        {
            switch (view?.Id)
            {
                case Resource.Id.button_audio:
                    KanaAudioPlayer.PlayAudio(m_KanaCharacter);
                    break;
                case Resource.Id.button_strokes:
                    KanaStrokeDialog.ShowDialog(Activity, new KanaBundle(m_KanaCharacter, m_kanaScript));
                    break;
                default:
                    break;
            }
        }
    }
}