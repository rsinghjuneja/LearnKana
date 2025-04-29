using System;

using Android.Content;
using Android.Views;

using Google.Android.Material.BottomSheet;

namespace LearnKana.Droid.MVVM.Views.Dialogs
{
    public class BaseBottomSheetDialogFragment : BottomSheetDialogFragment, View.IOnClickListener
    {
        public event Action? Exit;

        public override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetStyle(StyleNoTitle, Resource.Style.ModalBottomSheetDialogStyle);
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
