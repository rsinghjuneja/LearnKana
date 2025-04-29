using Android.Views;

namespace LearnKana.Droid.MVVM.Recycler.ViewHolder
{
    public class EmptyViewHolder<T>(View view) : RecyclerViewHolder<T>(view)
    {
        public override void BindItemView(T item) { }
    }
}
