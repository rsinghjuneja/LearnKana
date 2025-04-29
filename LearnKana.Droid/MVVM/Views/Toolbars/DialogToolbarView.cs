using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace LearnKana.Droid.MVVM.Views.Toolbars
{
    public class DialogToolbarView : LinearLayout
    {
        private readonly TextView m_TextView;
        private readonly View m_ButtonExit;

        public DialogToolbarView(Context? context) : this(context, null) { }
        public DialogToolbarView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            Orientation = Orientation.Vertical;
            Inflate(Context, _Microsoft.Android.Resource.Designer.ResourceConstant.Layout.toolbar_dialog, this);
            m_ButtonExit = RequireViewById<View>(_Microsoft.Android.Resource.Designer.ResourceConstant.Id.button_exit);
            m_TextView = RequireViewById<TextView>(_Microsoft.Android.Resource.Designer.ResourceConstant.Id.textview_title);
        }

        public event Action? Exit;

        public void SetTitle(string? title)
            => m_TextView.SetText(title);

        protected void InvokeExit()
        {
            Exit?.Invoke();
        }

        public void SetOnExitClickListener(IOnClickListener? listener)
            => m_ButtonExit.SetOnClickListener(listener);

        public bool DismissRequested(int id) => id == m_ButtonExit.Id;
        public bool DismissRequested(View? view) => view == m_ButtonExit;
    }
}