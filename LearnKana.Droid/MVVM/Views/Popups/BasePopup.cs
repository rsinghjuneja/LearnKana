using System;

using Android.Content;
using Android.Views;
using Android.Widget;

namespace LearnKana.Droid.MVVM.Views.Popups
{
    public abstract class BasePopup : PopupWindow, View.IOnClickListener
    {
        protected BasePopup(Context? context) : base(context)
        {
            SetBackgroundDrawable(context.GetDrawableCompat(Resource.Drawable.shape_rounded_corners));
            OutsideTouchable = true;
            if (OperatingSystem.IsAndroidVersionAtLeast(23))
            {
                OverlapAnchor = true;
            }

            LayoutInflater inflater = App.GetLayoutInflater(context);
            ContentView = OnCreateView(inflater);
        }

        protected abstract View OnCreateView(LayoutInflater inflater);

        public virtual void OnClick(View? view)
        {
        }
    }

    public abstract class BasePopup<T> : BasePopup where T : BasePopup
    {
        public static T Show(T popup, View? anchor = null, int offsetX = 0, int offsetY = 0)
        {
            popup.ShowAsDropDown(anchor, offsetX, offsetY);
            return popup;
        }

        protected BasePopup(Context? context) : base(context)
        {

        }
    }
}