using System;

using Android.Views;
using Android.Widget;

using AndroidX.Fragment.App;

using Google.Android.Material.Dialog;

using LearnKana.Domain.Kana;
using LearnKana.Droid.MVVM.Views.Widgets;
using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Views.Dialogs
{
    public class KanaStrokeDialog : BaseAlertDialogFragment
    {
        public static KanaStrokeDialog ShowDialog(FragmentActivity? activity, KanaBundle bundle)
        {
            ArgumentNullException.ThrowIfNull(activity);
            KanaStrokeDialog dialog = CreateInstance(bundle);
            dialog.Show(activity.SupportFragmentManager, nameof(KanaStrokeDialog));
            return dialog;
        }

        public static KanaStrokeDialog CreateInstance(KanaBundle bundle)
        {
            KanaStrokeDialog dialog = new KanaStrokeDialog
            {
                Arguments = bundle.ToBundle()
            };
            return dialog;
        }

        private TextView? m_TextViewContent;
        private GifView? m_GifView;

        protected override void OnCreateDialog(MaterialAlertDialogBuilder builder)
        {
            View view = LayoutInflater.Inflate<View>(Resource.Layout.dialog_kana_strokes, null);

            m_TextViewContent = view.RequireViewById<TextView>(Resource.Id.textview_content);
            m_GifView = view.RequireViewById<GifView>(Resource.Id.gif_view);

            Arguments arguments = new Arguments(Arguments);
            string romaji = KanaBundle.GetRomajiKey(arguments);
            KanaCharacter kana = App.KanaService.KanaSyllabary[romaji];
            KanaScript script = KanaBundle.GetKanaScript(arguments);

            m_TextViewContent.SetText(kana.ToFullString(script));
            m_TextViewContent.SetTypeface(Resource.Font.noto_sans_jp_bold, Android.Graphics.TypefaceStyle.Bold);
            m_GifView.SetGif(Path.Combine("GIFs", Strings.GetkanaScript(script), $"{romaji}.gif"));

            builder.SetView(view);
            builder.SetTitle(Resource.String.stroke_order);
            builder.SetIcon(Resource.Drawable.ic_strokes);
        }

        public override void OnStart()
        {
            m_GifView?.StartGif();
            base.OnStart();
        }

        public override void OnStop()
        {
            m_GifView?.StopGif();
            base.OnStop();
        }

        protected override void OnExit()
        {
            m_GifView?.Destroy();
            base.OnExit();
        }
    }
}