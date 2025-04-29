using Android.Views;

namespace LearnKana.Droid.MVVM.Recycler.ViewHolder
{
    public interface IRecyclerViewHolderFactory
    {
        public RecyclerViewHolder CreateViewHolder(LayoutInflater inflater, ViewGroup parent, int viewType);
    }
}
