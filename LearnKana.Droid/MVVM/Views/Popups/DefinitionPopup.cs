using Android.Content;
using Android.Views;
using Android.Widget;
using LearnKana.Droid.MVVM.Views.Toolbars;

namespace LearnKana.Droid.MVVM.Views.Popups
{
    public class DefinitionPopup(Context? context, string phrase, string definition) : BasePopup<DefinitionPopup>(context)
    {
        public static DefinitionPopup Show(Context? context, string phrase, string definition, View? anchor = null, int offsetX = 0, int offsetY = 0)
        {
            DefinitionPopup popup = new DefinitionPopup(context, phrase, definition);
            popup.ShowAsDropDown(anchor, offsetX, offsetY);
            return popup;
        }

        private readonly string m_Phrase = phrase;
        private readonly string m_Definition = definition;

        private DialogToolbarView? m_Toolbar;
        private TextView? m_TextViewDefinition;

        protected override View OnCreateView(LayoutInflater inflater)
        {
            View view = inflater.Inflate<View>(Resource.Layout.popup_definition, null);

            m_Toolbar = view.RequireViewById<DialogToolbarView>(Resource.Id.toolbar);
            m_TextViewDefinition = view.RequireViewById<TextView>(Resource.Id.textview_definition);

            m_Toolbar.SetTitle(m_Phrase);
            m_TextViewDefinition.SetText(m_Definition);

            m_Toolbar.SetOnExitClickListener(this);

            return view;
        }

        public override void OnClick(View? view)
        {
            if (m_Toolbar?.DismissRequested(view) ?? false)
                Dismiss();
        }

        public override void Dismiss()
        {
            m_Toolbar?.SetOnExitClickListener(null);
            base.Dismiss();
        }
    }
}
