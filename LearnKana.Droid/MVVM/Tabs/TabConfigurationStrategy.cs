using System;

using Google.Android.Material.Tabs;

namespace LearnKana.Droid.MVVM.Tabs
{
    public class TabConfigurationStrategy(Action<TabLayout.Tab, int> strategy) : Java.Lang.Object, TabLayoutMediator.ITabConfigurationStrategy
    {
        private readonly Action<TabLayout.Tab, int> m_Strategy = strategy;

        public void OnConfigureTab(TabLayout.Tab tab, int position)
        {
            m_Strategy.Invoke(tab, position);
        }
    }
}
