using System;

using Android.Content;
using Android.Views;

namespace LearnKana.Droid.MVVM.Views.Dialogs
{
    public abstract class BaseDialogFragment : DialogFragment, View.IOnClickListener
    {
        public event Action? Exit;

        public override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetStyle(StyleNoTitle, Resource.Style.DialogStyle);
        }

        protected T CreateParameters<T>() where T : IBundle<T>
        {
            return T.FromBundle(Arguments);
        }

        /// <summary>
        /// Uses type name as the tag.
        /// </summary>
        /// <param name="provider"></param>
        public void Show(IFragmentManagerProvider provider)
        {
            Show(provider.GetFragmentManager(), GetType().Name);
        }

        protected void InvokeOnExitEvent()
        {
            Exit?.Invoke();
        }
        protected virtual void OnExit()
        {
            InvokeOnExitEvent();
        }
        public override void OnDismiss(IDialogInterface dialog)
        {
            OnExit();
            base.OnDismiss(dialog);
        }
        public virtual void OnClick(View? view)
        {
            if (view?.Id == Resource.Id.button_exit)
                OnExit();
        }
    }
}