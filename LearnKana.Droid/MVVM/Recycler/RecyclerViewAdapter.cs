using System;
using System.Collections.Generic;

using Android.Views;

using AndroidX.RecyclerView.Widget;

using LearnKana.Droid.MVVM.Recycler.ViewHolder;

namespace LearnKana.Droid.MVVM.Recycler
{
    public class RecyclerViewAdapter<T>(IRecyclerViewHolderFactory factory) : RecyclerViewAdapter(factory)
    {
        public T this[int index] => Items[index];
        public List<T> Items { get; } = [];
        public override int ItemCount => Items.Count;

        public event Action<T, RecyclerViewHolderEventArgs>? Click;
        public event Action<T, RecyclerViewHolderEventArgs>? LongClick;

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            LayoutInflater inflater = LayoutInflater.From(parent.Context) ?? throw new NullContextException();
            RecyclerViewHolder viewHolder = Factory.CreateViewHolder(inflater, parent, viewType);
            if (viewHolder.ClickableItems)
            {
                viewHolder.SetOnClickListener(args => Click?.Invoke(this[viewHolder.AbsoluteAdapterPosition], args));
                viewHolder.SetOnLongClickListener(args => LongClick?.Invoke(this[viewHolder.AbsoluteAdapterPosition], args));
            }
            return viewHolder;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            T item = this[position];
            RecyclerViewHolder<T> viewHolder = (RecyclerViewHolder<T>)holder;
            viewHolder.BindItemView(item);
        }

        public void SetItems(IEnumerable<T> items)
        {
            Items.Clear();
            Items.AddRange(items);
            NotifyDataSetChanged();
        }

        public void SetItems(IEnumerable<T> items, DiffUtilCallback<T> callback)
        {
            DiffUtil.DiffResult result = DiffUtil.CalculateDiff(callback);
            Items.Clear();
            Items.AddRange(items);
            result.DispatchUpdatesTo(this);
        }

        public override int GetItemViewType(int position)
        {
            return base.GetItemViewType(position);
        }
    }

    public abstract class RecyclerViewAdapter(IRecyclerViewHolderFactory factory) : RecyclerView.Adapter
    {
        public IRecyclerViewHolderFactory Factory { get; } = factory;
    }
}