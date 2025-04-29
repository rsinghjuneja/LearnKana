using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

using Google.Android.Material.Card;

namespace LearnKana.Droid.MVVM.Views.Containers
{
    public class MaterialTitleContainer : MaterialCardView
    {
        private readonly TextView m_TextViewTitle;
        private readonly ViewGroup m_ChildContainer;

        public MaterialTitleContainer(Context? context) : this(context, null) { }
        public MaterialTitleContainer(Context? context, IAttributeSet? attrs) : base(context, attrs)
        {
            Inflate(context, Resource.Layout.layout_title_container, this);
            m_ChildContainer = RequireViewById<ViewGroup>(Resource.Id.child_container);
            m_TextViewTitle = RequireViewById<TextView>(Resource.Id.textview_title);
        }

        public void SetTitle(string title) => m_TextViewTitle.SetText(title);
        public void SetTitle(int resourceId) => m_TextViewTitle.SetText(resourceId);

        public override void AddView(View? child, int index, ViewGroup.LayoutParams? @params)
        {
            if (ChildCount > 0 && GetChildAt(0) == m_ChildContainer)
                m_ChildContainer.AddView(child, index, @params);
            else
                base.AddView(child, index, @params);
        }
    }
}