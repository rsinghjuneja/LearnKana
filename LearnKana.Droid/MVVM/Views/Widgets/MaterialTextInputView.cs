using Android.Content;
using Android.Util;
using Android.Views;

using Google.Android.Material.Button;
using Google.Android.Material.Card;
using Google.Android.Material.TextField;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class MaterialTextInputView : MaterialCardView
    {
        private readonly MaterialButton m_ButtonOk;

        public MaterialTextInputView(Context? context) : this(context, null) { }
        public MaterialTextInputView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            Inflate(context, Resource.Layout.layout_text_input, this);
            TextInputLayout = RequireViewById<TextInputLayout>(Resource.Id.textinput_layout);
            TextInputEditText = RequireViewById<TextInputEditText>(Resource.Id.textinput_edittext);
            m_ButtonOk = RequireViewById<MaterialButton>(Resource.Id.button_ok);
        }

        public TextInputLayout TextInputLayout { get; }
        public TextInputEditText TextInputEditText { get; }

        public void SetButtonText(string? text) => m_ButtonOk.SetText(text);
        public void SetButtonText(int resourceId) => m_ButtonOk.SetText(resourceId);
        public void SetButtonEnabled(bool enabled) => m_ButtonOk.SetEnabled(enabled);

        public void SetButtonOnClickListener(View.IOnClickListener? listener) => m_ButtonOk.SetOnClickListener(listener);
    }
}
