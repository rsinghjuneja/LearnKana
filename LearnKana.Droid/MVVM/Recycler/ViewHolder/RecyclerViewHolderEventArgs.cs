using Android.Views;

namespace LearnKana.Droid.MVVM.Recycler.ViewHolder
{
    public class RecyclerViewHolderEventArgs(View? view, int adapterPosition, RecyclerViewHolder viewHolder)
    {
        public static RecyclerViewHolderEventArgs From(RecyclerViewHolder viewHolder)
            => new RecyclerViewHolderEventArgs(viewHolder.ItemView, viewHolder.AbsoluteAdapterPosition, viewHolder);

        public View? View { get; } = view;
        public int AdapterPosition { get; } = adapterPosition;
        public RecyclerViewHolder ViewHolder { get; } = viewHolder;
    }
}