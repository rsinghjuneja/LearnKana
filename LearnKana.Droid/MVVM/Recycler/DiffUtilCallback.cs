using System;
using System.Collections.Generic;

using AndroidX.RecyclerView.Widget;

namespace LearnKana.Droid.MVVM.Recycler
{
    public class DiffUtilCallback<T>(List<T> newList, List<T> oldList,
        Func<T, T, bool> compareItemInstance, Func<T, T, bool>? compareItemContent = null) : DiffUtil.Callback
    {
        public override int NewListSize => m_NewList.Count;
        public override int OldListSize => m_OldList.Count;

        private readonly List<T> m_NewList = new List<T>(newList);
        private readonly List<T> m_OldList = new List<T>(oldList);

        private readonly Func<T, T, bool> m_CompareItemInstance = compareItemInstance;
        private readonly Func<T, T, bool> m_CompareItemContent = compareItemContent ?? compareItemInstance;

        public override bool AreItemsTheSame(int oldItemPosition, int newItemPosition)
        {
            T oldItem = m_OldList[oldItemPosition];
            T newItem = m_NewList[newItemPosition];

            return m_CompareItemInstance?.Invoke(oldItem, newItem) ?? false;
        }

        public override bool AreContentsTheSame(int oldItemPosition, int newItemPosition)
        {
            T oldItem = m_OldList[oldItemPosition];
            T newItem = m_NewList[newItemPosition];

            return m_CompareItemContent?.Invoke(oldItem, newItem) ?? false;
        }
    }
}
