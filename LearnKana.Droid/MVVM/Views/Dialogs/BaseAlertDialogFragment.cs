using Google.Android.Material.Dialog;

namespace LearnKana.Droid.MVVM.Views.Dialogs
{
    public abstract class BaseAlertDialogFragment : BaseDialogFragment
    {
        public override Android.App.Dialog OnCreateDialog(Bundle? savedInstanceState)
        {
            MaterialAlertDialogBuilder builder = new MaterialAlertDialogBuilder(RequireContext());
            OnCreateDialog(builder);
            return builder.Create();
        }
        protected abstract void OnCreateDialog(MaterialAlertDialogBuilder builder);
    }
}
