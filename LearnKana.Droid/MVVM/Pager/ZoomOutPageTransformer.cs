using System;

using Android.Views;

using AndroidX.ViewPager2.Widget;

namespace LearnKana.Droid.MVVM.Pager
{
    internal class ZoomOutPageTransformer : Java.Lang.Object, ViewPager2.IPageTransformer
    {
        private const float MIN_SCALE = 0.85f;
        private const float MIN_ALPHA = 0.5f;

        public void TransformPage(View view, float position)
        {
            int pageWidth = view.Width;
            int pageHeight = view.Height;

            if (position < -1)
            {
                view.Alpha = 0f;
            }
            else if (position <= 1)
            {
                float scaleFactor = Math.Max(MIN_SCALE, 1 - Math.Abs(position));
                float verticalMargin = pageHeight * (1 - scaleFactor) / 2;
                float horizontalMargin = pageWidth * (1 - scaleFactor) / 2;

                if (position < 0)
                {
                    view.TranslationX = horizontalMargin - verticalMargin / 2;
                }
                else
                {
                    view.TranslationX = -horizontalMargin + verticalMargin / 2;
                }

                view.ScaleX = scaleFactor;
                view.ScaleY = scaleFactor;

                view.Alpha = MIN_ALPHA + (scaleFactor - MIN_SCALE) / (1 - MIN_SCALE) * (1 - MIN_ALPHA);
            }
            else
            {
                view.Alpha = 0f;
            }
        }
    }
}