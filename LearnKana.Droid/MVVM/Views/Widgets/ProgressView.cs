using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

using LearnKana.Droid.Values;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class ProgressView : FrameLayout
    {
        protected readonly ProgressBar m_ProgressBar;
        protected readonly TextView m_TextViewLabel;

        public ProgressView(Context context) : this(context, null) { }
        public ProgressView(Context context, IAttributeSet? attrs) : base(context, attrs)
        {
            Inflate(context, LayoutResourceId, this);
            m_ProgressBar = RequireViewById<ProgressBar>(Resource.Id.progress_bar);
            m_TextViewLabel = RequireViewById<TextView>(Resource.Id.textview_label)
                .SetVisible(ViewStates.Gone);
        }

        public virtual int LayoutResourceId => Resource.Layout.layout_progress_view;

        public int Progress => m_ProgressBar.Progress;
        public int Max => m_ProgressBar.Max;

        public void SetMin(int min)
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(26))
                m_ProgressBar.Min = min;
        }
        public void SetMax(int max) => m_ProgressBar.Max = max;
        public void SetProgress(int min, int max, int progress, bool animate = true)
        {
            SetMin(min);
            SetMax(max);
            SetProgress(progress, animate);
        }
        public void SetProgress(int progress, bool animate = true)
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(24))
                m_ProgressBar.SetProgress(progress, animate);
            else
                m_ProgressBar.Progress = progress;
        }
        public void SetProgressLabel(int progress, int max) => SetProgressLabel($"{progress}/{max}");        
        public void SetProgressLabel(AndroidText text)
        {
            m_TextViewLabel
                .SetText(text)
                .SetVisible(true);
        }
    }
}