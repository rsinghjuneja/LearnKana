using System.Threading;
using System.Threading.Tasks;

using AndroidX.Lifecycle;

using LearnKana.Droid.Repositories;

namespace LearnKana.Droid.MVVM.ViewModels
{
    public abstract class AbstractViewModel(IRepository repository, SavedStateHandle state) : ViewModel, IViewModel
    {
        private readonly IRepository m_Repository = repository;
        protected SavedStateHandle m_State = state;

        public bool IsSaving { get; set; }

        private void SetSaving(bool value)
        {
            IsSaving = value;
            Debug.WriteLine(value ? $"{GetType().Name}: is saving..." : $"{GetType().Name}: save complete!");
        }

        public async Task SaveChangesAsync(CancellationToken? token = default)
        {
            SetSaving(true);
            await m_Repository.SaveChangesAsync(token);
            SetSaving(false);
        }

        public abstract class ViewModelFactory<T> : AbstractSavedStateViewModelFactory where T : ViewModel
        {
            protected override Java.Lang.Object Create(string key, Java.Lang.Class modelClass, SavedStateHandle state)
            {
                if (modelClass.IsAssignableFrom(Java.Lang.Class.FromType(typeof(T))))
                    return CreateViewModel(state);
                throw new InvalidOperationException();
            }
            protected abstract T CreateViewModel(SavedStateHandle state);
        }
    }
}