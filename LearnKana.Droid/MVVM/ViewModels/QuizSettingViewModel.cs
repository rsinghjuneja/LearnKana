using AndroidX.Lifecycle;

using LearnKana.Domain.Study;
using LearnKana.Droid.Repositories;

namespace LearnKana.Droid.MVVM.ViewModels
{
    public class QuizSettingViewModel(QuizRepository repository, SavedStateHandle handle) : AbstractViewModel(repository, handle)
    {
        private readonly QuizRepository m_Repository = repository;
        public QuizSettings Settings { get; } = repository.QuizSettings;

        public class Factory(QuizRepository repository) : ViewModelFactory<QuizSettingViewModel>()
        {
            private readonly QuizRepository m_Repository = repository;
            protected override QuizSettingViewModel CreateViewModel(SavedStateHandle state) 
                => new QuizSettingViewModel(m_Repository, state);
        }
    }
}