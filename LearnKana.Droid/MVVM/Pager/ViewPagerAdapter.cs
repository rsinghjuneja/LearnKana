using System;
using System.Collections.Generic;

using AndroidX.AppCompat.App;
using AndroidX.ViewPager2.Adapter;

namespace LearnKana.Droid.MVVM.Pager
{
    public class ViewPagerAdapter : FragmentStateAdapter
    {
        private readonly Dictionary<int, Func<Fragment>> m_Factory = [];
        private readonly Dictionary<int, Fragment> m_Fragments = [];

        public ViewPagerAdapter(AppCompatActivity activity) : base(activity)
        {
        }
        public ViewPagerAdapter(Fragment fragment) : base(fragment)
        {
        }

        public override int ItemCount => m_Factory.Count;

        public override Fragment CreateFragment(int position)
        {
            if (!m_Fragments.TryGetValue(position, out var fragment))
            {
                fragment = m_Factory[position].Invoke();
                m_Fragments.Add(position, fragment);
            }
            return fragment;
        }

        public ViewPagerAdapter AddFragment(Func<Fragment> factory)
        {
            m_Factory.Add(ItemCount, factory);
            return this;
        }
        public T GetFragment<T>(int position) where T : Fragment
        {
            return (T)m_Fragments[position];
        }
    }
}