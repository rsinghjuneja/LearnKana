using Android.Views;
using Android.Widget;

using LearnKana.Droid.MVVM.Views.Toolbars;
using LearnKana.Droid.MVVM.Views.Widgets;
using LearnKana.Droid.Utilities;
using LearnKana.Droid.Values.Resources;

namespace LearnKana.Droid.MVVM.Views.Dialogs
{
    public class MessageDialog : BaseDialogFragment
    {
        public static void ShowDialog(IFragmentManagerProvider provider, Parameters parameters, Action? callback = null)
        {
            MessageDialog dialog = CreateInstance(parameters);
            dialog.Exit += OnExit;
            dialog.Show(provider);

            void OnExit()
            {
                dialog.Exit -= OnExit;
                dialog.DismissAllowingStateLoss();
                callback?.Invoke();
            }
        }

        public static MessageDialog CreateInstance(Parameters parameters)
        {
            Bundle bundle = parameters.ToBundle(new Bundle());
            MessageDialog dialog = new MessageDialog
            {
                Arguments = bundle
            };
            return dialog;
        }

        protected DialogToolbarView? m_Toolbar;
        protected TextView? m_TextViewMessage;
        protected ButtonContainerView? m_ButtonContainerView;

        protected IBundle? m_Parameters;

        protected virtual void GetParameters()
        {
            m_Parameters = CreateParameters<MessageDialog.Parameters>();
        }

        public override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState); 
            GetParameters();
        }

        public override View? OnCreateView(LayoutInflater inflater, ViewGroup? container, Bundle? savedInstanceState)
        {
            View view = inflater.Inflate<View>(Resource.Layout.dialog_message, container);
            return view;
        }

        public override void OnViewCreated(View view, Bundle? savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            if (m_Parameters is not Parameters parameters)
                throw new NotImplementedException();

            m_Toolbar = view.RequireViewById<DialogToolbarView>(Resource.Id.dialog_toolbar_view);
            m_TextViewMessage = view.RequireViewById<TextView>(Resource.Id.textview_message);
            m_ButtonContainerView = view.RequireViewById<ButtonContainerView>(Resource.Id.button_container_view);

            m_Toolbar.SetTitle(parameters?.Title);
            m_TextViewMessage?.SetText(parameters?.Message);
            m_ButtonContainerView.AddButton(Resource.Id.button_positive, parameters?.Icon, parameters?.ButtonPositive);
        }

        public override void OnStart()
        {
            base.OnStart();
            m_Toolbar?.SetOnExitClickListener(this);
            m_ButtonContainerView?.SetOnClickListener(this);
        }
        public override void OnStop()
        {
            base.OnStop();
            m_Toolbar?.SetOnExitClickListener(null);
            m_ButtonContainerView?.SetOnClickListener(null);
        }

        public override void OnClick(View? view)
        {
            switch (view?.Id)
            {
                case Resource.Id.button_positive:
                    InvokeOnExitEvent();
                    break;
                case Resource.Id.button_exit:
                    InvokeOnExitEvent();
                    break;
            }

            base.OnClick(view);
        }

        public class Parameters(int icon, string title, string message, string? buttonPositive = default) : DialogParameters, IBundle<Parameters>
        {
            public static Parameters FromBundle(Bundle? bundle)
            {
                ArgumentNullException.ThrowIfNull(bundle);
                int icon = bundle.GetInt(Keys.Icon, IBundle.DefaultIntValue);
                string title = bundle.GetString(Keys.Title, default).ThrowIfNull();
                string message = bundle.GetString(Keys.Message, default).ThrowIfNull();
                string buttonYes = bundle.GetString(Keys.ButtonPositive, new StringResource(Resource.String.button_ok)).ThrowIfNull();
                return new Parameters(icon, title, message, buttonYes);
            }

            public int Icon { get; init; } = icon;
            public string Title { get; init; } = title;
            public string Message { get; init; } = message;
            public string? ButtonPositive { get; init; } = buttonPositive;

            public override Bundle ToBundle(Bundle bundle)
            {
                bundle.PutInt(Keys.Icon, Icon);
                bundle.PutString(Keys.Title, Title);
                bundle.PutString(Keys.Message, Message);
                bundle.PutString(Keys.ButtonPositive, ButtonPositive);
                return bundle;
            }
        }
    }
}
