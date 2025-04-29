using Google.Android.Material.Tabs;

namespace LearnKana.Droid.MVVM.Tabs
{
    public interface ITab
    {
        public bool Selected { get; }
        public void ConfigureTab(TabLayout.Tab newTab, int position);
    }
}
