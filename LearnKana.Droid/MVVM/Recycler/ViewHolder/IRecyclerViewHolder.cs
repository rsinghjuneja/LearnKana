using System;

namespace LearnKana.Droid.MVVM.Recycler.ViewHolder
{
    public interface IRecyclerViewHolder
    {
        public bool ClickableItems { get; }

        public void SetOnClickListener(Action<RecyclerViewHolderEventArgs>? onClick);
        public void SetOnLongClickListener(Action<RecyclerViewHolderEventArgs>? onLongClick);
    }
}
