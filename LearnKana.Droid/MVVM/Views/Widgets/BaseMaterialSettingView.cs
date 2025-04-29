using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

using Google.Android.Material.Card;
using Google.Android.Material.Divider;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public abstract class BaseMaterialSettingView<T> : MaterialCardView, View.IOnClickListener where T : BaseMaterialSettingView<T>
    {
        protected readonly ImageView m_ImageViewIcon;
        protected readonly TextView m_TextViewTitle;
        protected readonly TextView m_TextViewSubtitle;
        protected readonly MaterialDivider m_Divider;
        protected readonly FrameLayout m_ChildContainer;

        public BaseMaterialSettingView(Context? context) : this(context, null) { }
        public BaseMaterialSettingView(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            Inflate(context, Resource.Layout.layout_setting_view, this);
            m_ImageViewIcon = RequireViewById<ImageView>(Resource.Id.imageview_icon);
            m_TextViewTitle = RequireViewById<TextView>(Resource.Id.textview_title);
            m_TextViewSubtitle = RequireViewById<TextView>(Resource.Id.textview_subtitle);
            m_Divider = RequireViewById<MaterialDivider>(Resource.Id.title_divider);
            m_ChildContainer = RequireViewById<FrameLayout>(Resource.Id.child_container);          
        }
        
        public void AddViewToChildContainer(View view) => m_ChildContainer.AddView(view);
        protected ViewGroup.LayoutParams GenerateChildContainerLayoutParams(int width, int height) => new FrameLayout.LayoutParams(width, height);
        public abstract void OnClick(View? view);

        public T SetIcon(int icon)
        {
            m_ImageViewIcon.SetImageResource(icon);
            return (T)this;
        }
        public T SetTitle(string? title)
        {
            m_TextViewTitle.SetText(title);
            return (T)this;
        }
        public T SetSubtitle(string? subtitle)
        {
            m_TextViewSubtitle.SetText(subtitle);
            UpdateView();
            return (T)this;
        }
        protected virtual void UpdateView()
        {
            if (string.IsNullOrWhiteSpace(m_TextViewSubtitle.Text))
            {
                m_Divider.SetVisible(ViewStates.Gone);
                m_TextViewSubtitle.SetVisible(ViewStates.Gone);
            }
            else
            {
                m_Divider.SetVisible(ViewStates.Visible);
                m_TextViewSubtitle.SetVisible(ViewStates.Visible);
            }
        }
    }
}