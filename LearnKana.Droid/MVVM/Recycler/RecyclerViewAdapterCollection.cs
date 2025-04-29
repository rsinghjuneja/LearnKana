using System.Collections.Generic;

namespace LearnKana.Droid.MVVM.Recycler
{
    public class RecyclerViewAdapterCollection<T>(params RecyclerViewAdapter<T>[] adapters)
    {
        public List<RecyclerViewAdapter<T>> Adapters { get; } = new List<RecyclerViewAdapter<T>>(adapters);
        public RecyclerViewAdapter<T> CurrentAdapter { get; private set; } = adapters[0];

        public void AddAdapter(RecyclerViewAdapter<T> adapter)
        {
            Adapters.Add(adapter);
        }
        public bool RemoveAdapter(RecyclerViewAdapter<T> adapter)
        {
            bool result = Adapters.Remove(adapter);
            return result;
        }
        public void SetCurrentAdapter(int index)
        {
            CurrentAdapter = Adapters[index];
        }
    }
}