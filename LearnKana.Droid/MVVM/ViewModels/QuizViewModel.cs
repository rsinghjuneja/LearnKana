using AndroidX.Lifecycle;

using LearnKana.Domain.Study;
using LearnKana.Droid.Repositories;

namespace LearnKana.Droid.MVVM.ViewModels
{
    public class QuizViewModel(QuizRepository repository, SavedStateHandle state) : AbstractViewModel(repository, state)
    {
        private readonly QuizRepository m_Repository = repository;
        public Quiz? Quiz
        {
            get => m_Repository.Quiz;
            set => m_Repository.Quiz = value;
        }

        public class Factory(QuizRepository repository) : ViewModelFactory<QuizViewModel>()
        {
            private readonly QuizRepository m_Repository = repository;
            protected override QuizViewModel CreateViewModel(SavedStateHandle state) => new QuizViewModel(m_Repository, state);
        }
    }
}