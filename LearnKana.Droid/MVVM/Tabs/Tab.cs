using Android.Text;

using Google.Android.Material.Tabs;

namespace LearnKana.Droid.MVVM.Tabs
{
    public class Tab(SpannableString title, int icon, bool selected) : ITab
    {
        public Tab(string title, int icon, bool selected) : this(new SpannableString(title), icon, selected) { }

        public SpannableString Title { get; } = title;
        public int Icon { get; } = icon;
        public bool Selected { get; } = selected;

        public void ConfigureTab(TabLayout.Tab newTab, int position)
        {
            newTab.SetText(Title);
            if (Icon != 0)
                newTab.SetIcon(Icon);
        }
    }
}
