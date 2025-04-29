using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Android.Views;

using AndroidX.Fragment.App;

using LearnKana.Droid.Animations;
using LearnKana.Droid.MVVM.Views.Fragments;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.MVVM.Views
{
    // consider not caching fragments and accessing them directly from the fragment manager which exposes IList<Fragment>
    public class FragmentService<TKey>(IFragmentManagerProvider provider, View fragmentContainer) where TKey : notnull
    {
        public BaseFragment this[TKey index] => m_Fragments[index];

        private readonly Dictionary<TKey, Func<BaseFragment>> m_Factory = [];
        private readonly Dictionary<TKey, BaseFragment> m_Fragments = [];
        private readonly FragmentManager m_FragmentManager = provider.GetFragmentManager();

        public View FragmentContainer { get; } = fragmentContainer;
        public BaseFragment? Current { get; private set; }
        public EnterExitAnimation Animations { get; set; }
        public bool Async { get; set; }

        public FragmentService<TKey> AddFragmentFactory(TKey key, Func<BaseFragment> factory)
        {
            m_Factory.Add(key, factory);
            return this;
        }

        private NewFragment CreateFragment(TKey key)
        {
            if (!m_Fragments.TryGetValue(key, out BaseFragment? fragment))
            {
                fragment = m_Factory[key].Invoke();
                m_Fragments.Add(key, fragment);
                return new NewFragment(fragment, true);
            }
            return new NewFragment(fragment, false);
        }

        public T ShowFragment<T>(TKey key) where T : BaseFragment => (T)ShowFragment(key);
        public BaseFragment ShowFragment(TKey key)
        {
            FragmentTransaction transaction = m_FragmentManager.BeginTransaction();
            NewFragment newFragment = CreateFragment(key);
            BaseFragment fragment = newFragment.Fragment;

            if (newFragment.Created)
                transaction
                    .Add(FragmentContainer.Id, fragment, key.ToString());

            m_FragmentManager.Fragments.ForEachElement(x => transaction.Hide(x));

            transaction
                .Show(fragment)
                .SetCustomAnimations(Animations.Enter, Animations.Exit);

            TransactionCommit(transaction);

            Current = fragment;
            return fragment;
        }
        public T SetFragment<T>(TKey key) where T : BaseFragment => (T)SetFragment(key);
        public BaseFragment SetFragment(TKey key)
        {
            FragmentTransaction transaction = m_FragmentManager.BeginTransaction();
            NewFragment newFragment = CreateFragment(key);
            BaseFragment fragment = newFragment.Fragment;

            transaction
                .Replace(FragmentContainer.Id, fragment)
                .SetCustomAnimations(Animations.Enter, Animations.Exit);

            TransactionCommit(transaction);

            Current = fragment;
            return fragment;
        }

        public TFragment GetFragment<TFragment>(TKey key) where TFragment : BaseFragment
        {
            BaseFragment fragment = this[key];
            return (TFragment)fragment;
        }
        public TFragment GetCurrentFragment<TFragment>() where TFragment : BaseFragment
        {
            ArgumentNullException.ThrowIfNull(Current);
            BaseFragment fragment = Current;
            return (TFragment)fragment;
        }
        private void TransactionCommit(FragmentTransaction transaction)
        {
            if (Async)
                transaction.Commit();
            else
                transaction.CommitNow();
        }

        [method: SetsRequiredMembers]
        private readonly struct NewFragment(BaseFragment fragment, bool created)
        {
            public required readonly BaseFragment Fragment { get; init; } = fragment;
            public required readonly bool Created { get; init; } = created;
        }
    }
}