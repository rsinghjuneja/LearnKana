using System.Collections.Generic;

using Android.Views;

using AndroidX.ViewPager2.Widget;

using Google.Android.Material.Tabs;

using LearnKana.Droid.Utilities;

namespace LearnKana.Droid.MVVM.Tabs
{
    public class TabService : DisposableObject
    {
        private readonly List<ITab> m_Tabs = [];
        private TabLayoutMediator? m_Mediator;
        private int m_Selected;

        public ITab this[int index]
        {
            get => m_Tabs[index];
            set => m_Tabs[index] = value;
        }

        public TabService()
        {

        }

        public int Count => m_Tabs.Count;
        public bool HideSingleTab { get; set; } = true;

        public TabService AddTab(ITab tab)
        {
            m_Tabs.Add(tab);
            return this;
        }

        public TabService Mediate(TabLayout tabLayout, ViewPager2 viewPager, TabMode mode, TabConfigurationStrategy? strategy = null)
        {
            m_Mediator = new TabLayoutMediator(tabLayout, viewPager, strategy ?? new TabConfigurationStrategy(Configure));
            m_Mediator.Attach();
            SetTabLayoutMode(tabLayout, mode);
            viewPager.SetCurrentItem(m_Selected, false);
            return this;
        }

        public TabService Configure(TabLayout tabLayout, TabMode mode)
        {
            for (int i = 0; i < m_Tabs.Count; i++)
            {
                ITab tab = m_Tabs[i];
                TabLayout.Tab newTab = tabLayout.NewTab();
                Configure(newTab, i);
                tabLayout.AddTab(newTab, i, tab.Selected);
            }

            SetTabLayoutMode(tabLayout, mode);
            return this;
        }

        private void SetTabLayoutMode(TabLayout tabLayout, TabMode mode)
        {
            if (HideSingleTab && m_Tabs.Count <= 1)
            {
                tabLayout.SetVisible(ViewStates.Gone);
                return;
            }

            if (mode == TabMode.Fixed)
                tabLayout.TabGravity = TabLayout.GravityFill;
            else
                tabLayout.TabGravity = TabLayout.GravityCenter;

            tabLayout.TabMode = (int)mode;
        }

        private void Configure(TabLayout.Tab newTab, int position)
        {
            ITab tab = m_Tabs[position];
            tab.ConfigureTab(newTab, position);
            if (tab.Selected)
                m_Selected = position;
        }

        protected override void OnDispose()
        {
            m_Mediator?.Detach();
        }
    }
}