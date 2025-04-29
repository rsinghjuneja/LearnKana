using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

using LearnKana.Droid.Values;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class CircularProgressView : ProgressView
    {
        private readonly TextView m_TextViewTitle;
        private readonly TextView m_TextViewSubtitle;
        public CircularProgressView(Context context) : this(context, null) { }
        public CircularProgressView(Context context, IAttributeSet? attrs) : base(context, attrs)
        {
            m_TextViewTitle = RequireViewById<TextView>(Resource.Id.textview_title).SetVisible(ViewStates.Gone);
            m_TextViewSubtitle = RequireViewById<TextView>(Resource.Id.textview_subtitle).SetVisible(ViewStates.Gone);
        }

        public override int LayoutResourceId => Resource.Layout.layout_circular_progress_view;

        public CircularProgressView SetTitle(AndroidText text)
        {
            m_TextViewTitle
                .SetText(text)
                .SetVisible(true);
            return this;
        }
        public CircularProgressView SetSubtitle(AndroidText text)
        {
            m_TextViewSubtitle
                .SetText(text)
                .SetVisible(true);
            return this;
        }
    }
}