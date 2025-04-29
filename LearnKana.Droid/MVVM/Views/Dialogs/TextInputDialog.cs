using System;

using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

using Google.Android.Material.TextField;

using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Views.Dialogs
{
    public class TextInputDialog : BaseBottomSheetDialogFragment, TextView.IOnEditorActionListener
    {
        public static TextInputDialog ShowDialog(TextInputDialog dialog, IFragmentManagerProvider? provider, Action<string> callback)
        {
            ArgumentNullException.ThrowIfNull(provider);

            dialog.Input += OnInput;
            void OnInput(string input)
            {
                dialog.Input -= OnInput;
                callback?.Invoke(input);
                dialog.DismissAllowingStateLoss();
            }

            dialog.Show(provider.GetFragmentManager(), nameof(TextInputDialog));
            return dialog;
        }
        public static TextInputDialog ShowDialog(IFragmentManagerProvider? provider, Parameters parameters, Action<string> callback)
        {
            TextInputDialog dialog = CreateInstance(parameters);
            ShowDialog(dialog, provider, callback);
            return dialog;
        }
        public static TextInputDialog CreateInstance(Parameters parameters)
        {
            Bundle bundle = parameters.ToBundle(new Bundle());
            TextInputDialog dialog = new TextInputDialog { Arguments = bundle };
            return dialog;
        }

        private TextInputLayout? m_TextInputLayout;
        private TextInputEditText? m_TextInputEditText;
        private View? m_ButtonOk;

        private Parameters? m_Parameters;

        public event Action<string>? Input;

        public override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            m_Parameters = Parameters.FromBundle(Arguments);
        }
        public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate<View>(Resource.Layout.layout_text_input, container);
            return view;
        }
        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            m_TextInputLayout = view.RequireViewById<TextInputLayout>(Resource.Id.textinput_layout);
            m_TextInputEditText = view.RequireViewById<TextInputEditText>(Resource.Id.textinput_edittext);
            m_ButtonOk = view.RequireViewById<View>(Resource.Id.button_ok);

            ArgumentNullException.ThrowIfNull(m_Parameters);

            m_TextInputLayout.Hint = m_Parameters.Hint;
            if (m_Parameters.Max != IBundle.DefaultIntValue)
            {
                m_TextInputLayout.CounterEnabled = true;
                m_TextInputLayout.CounterMaxLength = m_Parameters.Max;
            }
            m_TextInputEditText.InputType = InputTypes.ClassText;
            if (m_Parameters.Input?.Length <= m_TextInputLayout.CounterMaxLength)
                m_TextInputEditText.SetText(m_Parameters.Input);

            m_TextInputEditText.ImeOptions = ImeAction.Done;
            m_TextInputEditText.RequestFocus();
            m_TextInputEditText.SelectAll();
        }
        public override Android.App.Dialog OnCreateDialog(Bundle? savedInstanceState)
        {
            Android.App.Dialog dialog = base.OnCreateDialog(savedInstanceState);
            if (dialog.Window != null)
            {
                dialog.Window.SetGravity(GravityFlags.Bottom);
                dialog.Window.SetSoftInputMode(SoftInput.StateAlwaysVisible);
                dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                dialog.Window.SetFlags(WindowManagerFlags.BlurBehind, WindowManagerFlags.BlurBehind);
            }
            return dialog;
        }

        public override void OnStart()
        {
            base.OnStart();
            m_ButtonOk?.SetOnClickListener(this);
            m_TextInputEditText?.SetOnEditorActionListener(this);
            if (m_TextInputEditText != null)
                m_TextInputEditText.TextChanged += EditText_TextChanged;
        }
        public override void OnStop()
        {
            base.OnStop();
            m_ButtonOk?.SetOnClickListener(null);
            m_TextInputEditText?.SetOnEditorActionListener(null);
            if (m_TextInputEditText != null)
                m_TextInputEditText.TextChanged -= EditText_TextChanged;
        }

        private void EditText_TextChanged(object? sender, TextChangedEventArgs e)
        {
            m_ButtonOk?.SetEnabled(m_TextInputEditText?.Text?.Length <= m_TextInputLayout?.CounterMaxLength);
        }
        public bool OnEditorAction(TextView? textview, [GeneratedEnum] ImeAction actionId, KeyEvent? e)
        {
            if (actionId == ImeAction.Done)
            {
                if (m_TextInputEditText?.Text?.Length <= m_TextInputLayout?.CounterMaxLength)
                {
                    InvokeInputEvent();
                    return true;
                }
            }
            return false;
        }
        public override void OnClick(View? view)
        {
            base.OnClick(view);
            InvokeInputEvent();
        }

        private void InvokeInputEvent()
        {
            Input?.Invoke(m_TextInputEditText?.Text ?? string.Empty);
            DismissAllowingStateLoss();
        }

        public class Parameters(string? input, string hint, int max = -1) : DialogParameters, IBundle<Parameters>
        {
            public string? Input { get; init; } = input;
            public string Hint { get; init; } = hint;
            public int Max { get; init; } = max;

            public static Parameters FromBundle(Bundle? bundle)
            {
                ArgumentNullException.ThrowIfNull(bundle);
                string? input = bundle.GetString(Keys.Text, default);
                string hint = bundle.GetString(Keys.Hint) ?? string.Empty;
                int max = bundle.GetInt(Keys.Max, IBundle.DefaultIntValue);
                return new Parameters(input, hint, max);
            }

            public override Bundle ToBundle(Bundle bundle)
            {
                bundle.PutString(Keys.Text, Input);
                bundle.PutString(Keys.Hint, Hint);
                bundle.PutInt(Keys.Max, Max);
                return bundle;
            }
        }
    }
}