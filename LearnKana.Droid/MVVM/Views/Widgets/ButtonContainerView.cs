using Android.Content;
using Android.Util;

using Google.Android.Material.Button;

using LearnKana.Droid.MVVM.Views.Factory;
using LearnKana.Droid.Values;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class ButtonContainerView(Context context, IAttributeSet? attrs) : BaseButtonContainerView<MaterialButton>(context, attrs)
    {
        public ButtonContainerView(Context context) : this(context, null) { }

        protected override MaterialButton CreateButton(int id, int? icon, string? text)
        {
            MaterialButton button = new MaterialButton(Context.ThrowIfNull())
            {
                Id = id,
                LayoutParameters = new LayoutParams(ViewFactory.WrapContent, ViewFactory.WrapContent) { Weight = 1, },
            }.SetMargin(Margin.Small);
            button.SetText(text);
            button.SetCompoundDrawablesRelativeWithIntrinsicBounds(icon ?? 0, 0, 0, 0);
            return button;
        }
    }
}