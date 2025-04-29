using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Widget;
using LearnKana.Droid.MVVM.Views.Factory;
using LearnKana.Droid.Values;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views.Widgets
{
    public class OptionView : LinearLayout
    {
        private readonly List<ItemCardView> m_ItemCardViews;

        public OptionView(Context context) : this(context, null) { }
        public OptionView(Context context, IAttributeSet? attrs) : base(context, attrs)
        {
            Orientation = Orientation.Vertical;
            m_ItemCardViews = [];
        }

        public void AddOptionView(int id)
        {
            ItemCardView view = new ItemCardView(Context)
            {
                Id = id,
                LayoutParameters = ViewFactory.LayoutParameterFactory.Create<LinearLayout>(new LayoutParameters(LayoutParameter.MatchParent, LayoutParameter.WrapContent)),
                Clickable = true,
            }.SetMargin(Margin.Small);
            AddView(view);
            m_ItemCardViews.Add(view);
        }
        public void SetOption(int index, Option option)
        {
            ItemCardView view = m_ItemCardViews[index];
            view.SetOptionId(option.OptionId);
            view.SetContent(option.Content);
            view.SetIcon(option.Icon);
        }
        public void SetItemCardBackgroundColor(Color color) => m_ItemCardViews.ForEach(x => x.SetCardBackgroundColor(color));
        public void Reset()
        {
            m_ItemCardViews.ForEach(x =>
            {
                x.RemoveHighlight();
            });
        }
        public override void SetOnClickListener(IOnClickListener? listener)
        {
            m_ItemCardViews.ForEachElement(x => x.SetOnClickListener(listener));
        }

        [method: SetsRequiredMembers]
        public readonly struct Option(string id, string content, int icon = 0)
        {
            public required string OptionId { get; init; } = id;
            public required string Content { get; init; } = content;
            public required int Icon { get; init; } = icon;
        }
    }
}
