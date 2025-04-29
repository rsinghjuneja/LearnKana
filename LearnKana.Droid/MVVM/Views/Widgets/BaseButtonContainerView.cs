using System.Collections.Generic;

using Android.Content;
using Android.Util;
using Android.Widget;

using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public abstract class BaseButtonContainerView<T> : LinearLayout where T : Button
    {
        protected readonly List<T> m_Buttons = [];
        public BaseButtonContainerView(Context context) : this(context, null) { }
        public BaseButtonContainerView(Context context, IAttributeSet? attrs) : base(context, attrs)
        {
            Orientation = Orientation.Horizontal;
        }

        public void AddButton(int id, int? icon, string? text)
        {
            T button = CreateButton(id, icon, text);
            m_Buttons.Add(button);
            AddView(button);
        }
        public void InsertButton(int index, int id, int? icon, string? text)
        {
            T button = CreateButton(id, icon, text);
            m_Buttons.Insert(index, button);
            AddView(button, index);
        }

        protected abstract T CreateButton(int id, int? icon, string? text);
        public override void SetOnClickListener(IOnClickListener? listener) => m_Buttons.ForEachElement(x => x.SetOnClickListener(listener));
    }


}
