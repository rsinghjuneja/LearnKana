using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

using AndroidX.AppCompat.App;

using Google.Android.Material.AppBar;

namespace LearnKana.Droid.MVVM.Views.Toolbars
{
    public class ActivityToolbarView : AppBarLayout
    {
        private readonly TextView m_TextViewToolbarTitle;
        private readonly TextView m_TextViewToolbarSubtitle;

        public ActivityToolbarView(Context context) : this(context, null) { }
        public ActivityToolbarView(Context context, IAttributeSet? attrs) : base(context, attrs)
        {
            Inflate(context, Resource.Layout.toolbar_activity, this);
            Toolbar = RequireViewById<MaterialToolbar>(Resource.Id.material_toolbar);

            m_TextViewToolbarTitle = Toolbar.RequireViewById<TextView>(Resource.Id.textview_toolbar_title);
            m_TextViewToolbarSubtitle = Toolbar.RequireViewById<TextView>(Resource.Id.textview_toolbar_subtitle);
            m_TextViewToolbarSubtitle.SetVisible(ViewStates.Gone);
        }

        public MaterialToolbar Toolbar { get; }

        public void SetToolbarTitle(string title) => m_TextViewToolbarTitle.SetText(title);
        public void SetToolbarTitle(int resourceId) => m_TextViewToolbarTitle.SetText(resourceId);
        public void SetToolbarSubtitle(string subtitle) => m_TextViewToolbarSubtitle.SetText(subtitle).SetVisible(true);
        public void SetToolbarSubtitle(int resourceId)
        {
            m_TextViewToolbarSubtitle.SetText(resourceId);
            m_TextViewToolbarSubtitle.SetVisible(true);
        }

        public void SetSupportActionBar(AppCompatActivity activity, bool displayHomeAsUpEnabled = true)
        {
            activity.SetSupportActionBar(Toolbar);
            activity.SupportActionBar?.SetDisplayHomeAsUpEnabled(displayHomeAsUpEnabled);
        }
    }
}