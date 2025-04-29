using System;

using Android.Views;

using AndroidX.RecyclerView.Widget;

namespace LearnKana.Droid.MVVM.Recycler.ViewHolder
{
    public abstract class RecyclerViewHolder<T>(View view) : RecyclerViewHolder(view)
    {
        public abstract void BindItemView(T item);
    }

    public abstract class RecyclerViewHolder(View view) : RecyclerView.ViewHolder(view), IRecyclerViewHolder, View.IOnClickListener, View.IOnLongClickListener
    {
        private Action<RecyclerViewHolderEventArgs>? m_Click;
        private Action<RecyclerViewHolderEventArgs>? m_LongClick;

        public bool ClickableItems { get; set; }

        public void SetOnClickListener(Action<RecyclerViewHolderEventArgs>? onClick)
        {
            m_Click = onClick;
            ItemView.SetOnClickListener(m_Click != null ? this : null);
        }
        public void SetOnLongClickListener(Action<RecyclerViewHolderEventArgs>? onLongClick)
        {
            m_LongClick = onLongClick;
            ItemView.SetOnLongClickListener(m_LongClick != null ? this : null);
        }

        public void OnClick(View? view) => m_Click?.Invoke(RecyclerViewHolderEventArgs.From(this));
        public bool OnLongClick(View? view)
        {
            if (m_LongClick == null)
                return false;

            m_LongClick.Invoke(RecyclerViewHolderEventArgs.From(this));
            return true;
        }
    }
}