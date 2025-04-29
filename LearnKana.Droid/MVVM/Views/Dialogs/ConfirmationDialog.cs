using Android.Views;

using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Views.Dialogs
{
    public class ConfirmationDialog : MessageDialog
    {
        public static void ShowDialog(IFragmentManagerProvider provider, Parameters parameters, Action<DialogResult> callback)
        {
            ConfirmationDialog dialog = CreateInstance(parameters);
            dialog.Result += OnResult;
            dialog.Exit += OnExit;

            void OnResult(DialogResult result)
            {
                dialog.InvokeOnExitEvent();
                callback?.Invoke(result);
            }
            void OnExit()
            {
                dialog.Result -= OnResult;
                dialog.Exit -= OnExit;
                dialog.DismissAllowingStateLoss();
            }

            dialog.Show(provider);
        }

        public static ConfirmationDialog CreateInstance(Parameters parameters)
        {
            Bundle bundle = parameters.ToBundle(new Bundle());
            ConfirmationDialog dialog = new ConfirmationDialog
            {
                Arguments = bundle
            };
            return dialog;
        }

        public event Action<DialogResult>? Result;

        protected override void GetParameters()
        {
            m_Parameters = CreateParameters<ConfirmationDialog.Parameters>();
        }

        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (m_Parameters is not Parameters parameters)
                throw new NotImplementedException();

            m_ButtonContainerView?.AddButton(Resource.Id.button_negative, parameters?.Icon, parameters?.ButtonNegative);
        }

        public override void OnClick(View? view)
        {
            switch (view?.Id)
            {
                case Resource.Id.button_positive:
                    Result?.Invoke(DialogResult.Yes);
                    break;
                case Resource.Id.button_negative:
                    Result?.Invoke(DialogResult.No);
                    break;
                default:
                    base.OnClick(view);
                    break;
            }

        }

        public new class Parameters(int icon, string title, string message, string buttonPositive, string buttonNegative) : MessageDialog.Parameters(icon, title, message, buttonPositive), IBundle<Parameters>
        {
            public new static Parameters FromBundle(Bundle? bundle)
            {
                ArgumentNullException.ThrowIfNull(bundle);
                int icon = bundle.GetInt(Keys.Icon, IBundle.DefaultIntValue);
                string title = bundle.GetString(Keys.Title, default).ThrowIfNull();
                string message = bundle.GetString(Keys.Message, default).ThrowIfNull();
                string buttonYes = bundle.GetString(Keys.ButtonPositive, default).ThrowIfNull();
                string buttonNo = bundle.GetString(Keys.ButtonNegative, default).ThrowIfNull();
                return new Parameters(icon, title, message, buttonYes, buttonNo);
            }

            public string ButtonNegative { get; init; } = buttonNegative;

            public override Bundle ToBundle(Bundle bundle)
            {
                base.ToBundle(bundle);
                bundle.PutString(Keys.ButtonNegative, ButtonNegative);
                return bundle;
            }
        }
    }
}