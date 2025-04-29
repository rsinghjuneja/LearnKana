using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Widget;

using Google.Android.Material.Card;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class ItemCardView : MaterialCardView
    {
        private readonly TextView m_TextViewSubTitle;
        private readonly TextView m_TextViewContent;
        private readonly ImageView m_ImageView;

        public ItemCardView(Context? context) : base(context)
        {
            Inflate(context, Resource.Layout.layout_item_card, this);
            m_TextViewSubTitle = RequireViewById<TextView>(Resource.Id.textview_subtitle);
            m_TextViewContent = RequireViewById<TextView>(Resource.Id.textview_content);
            m_ImageView = RequireViewById<ImageView>(Resource.Id.imageview_icon);
        }

        public ItemCardView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            Inflate(context, Resource.Layout.layout_item_card, this);
            m_TextViewSubTitle = RequireViewById<TextView>(Resource.Id.textview_subtitle);
            m_TextViewContent = RequireViewById<TextView>(Resource.Id.textview_content);
            m_ImageView = RequireViewById<ImageView>(Resource.Id.imageview_icon);
        }

        public void SetOptionId(string id)
        {
            m_TextViewSubTitle.SetText(id);
        }
        public void SetContent(string content)
        {
            m_TextViewContent.SetText(content);
        }
        public void SetContentTextSize(int textSize)
        {
            m_TextViewContent.SetTextSize(ComplexUnitType.Sp, textSize);
        }
        public void SetIcon(int icon)
        {
            m_ImageView.SetImageResource(icon);
        }
        public void SetIconTint(Color color)
        {
            m_ImageView.SetColorFilter(color);
        }

        public void Highlight(Color color)
        {
            StrokeWidth = 1;
            SetStrokeColor(ColorStateList.ValueOf(color));
        }

        public void RemoveHighlight()
        {
            StrokeWidth = 0;
            SetStrokeColor(ColorStateList.ValueOf(Color.Transparent));
        }
    }
}
