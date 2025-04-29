using System.Diagnostics;
using System.Timers;

using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class TimerProgressBar : ProgressBar
    {
        private TextView? m_TextViewLabel;

        private readonly Timer m_Timer;
        private readonly Stopwatch m_StopWatch;

        private Action? m_Callback;

        public TimerProgressBar(Context? context) : this(context, null) { }
        public TimerProgressBar(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            m_Timer = new Timer(100);
            m_StopWatch = new Stopwatch();
            Initialize();
        }

        public TimeSpan Duration { get; set; }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            m_Timer.Elapsed += Timer_Elapsed;
        }
        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            m_Timer.Elapsed -= Timer_Elapsed;
        }

        private void Initialize()
        {
            Visibility = ViewStates.Invisible;
        }

        public void SetTextLabel(TextView? textView) => m_TextViewLabel = textView;

        public void Start(Action? callback)
        {
            m_Callback = callback;
            Progress = 0;
            Max = (int)Duration.TotalMilliseconds;
            Visibility = ViewStates.Visible;

            m_StopWatch.Start();
            m_Timer.Start();
        }
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (m_StopWatch.ElapsedMilliseconds < Duration.TotalMilliseconds)
                Post(() => SetProgress((int)m_StopWatch.ElapsedMilliseconds));
            else
            {
                m_StopWatch.Reset();
                m_Timer.Stop();

                Post(() =>
                {
                    m_Callback?.Invoke();
                    m_TextViewLabel?.ClearText();
                    Visibility = ViewStates.Invisible;
                });
            }
        }

        public void SetDuration(TimeSpan duration) => Duration = duration;
        public void SetProgress(int progress)
        {
            if (OperatingSystem.IsAndroidVersionAtLeast(26))
                SetProgress(progress, true);
            else
                Progress = progress;

            m_TextViewLabel?.SetText($"{progress.ToPercent(Max)}/100");
        }
    }
}