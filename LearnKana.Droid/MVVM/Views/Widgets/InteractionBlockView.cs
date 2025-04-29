using Android.Content;
using Android.Util;
using Android.Views;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class InteractionBlockView : View, View.IOnClickListener
    {
        public InteractionBlockView(Context? context) : base(context)
        {
            this.SetVisible(ViewStates.Gone);
        }

        public InteractionBlockView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            this.SetVisible(ViewStates.Gone);
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            SetOnClickListener(this);
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            SetOnClickListener(null);
        }

        public void BlockInteraction(bool value)
        {
            this.SetVisible(value ? ViewStates.Visible : ViewStates.Gone);
        }

        public void OnClick(View? view)
        {
            Debug.WriteLine($"Absorbed Click");
        }
    }
}