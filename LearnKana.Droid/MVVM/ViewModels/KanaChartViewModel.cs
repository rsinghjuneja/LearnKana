using System.Threading.Tasks;

using AndroidX.Lifecycle;

using LearnKana.Domain.Kana;
using LearnKana.Droid.Repositories;

namespace LearnKana.Droid.MVVM.ViewModels
{
    public class KanaChartViewModel(ApplicationRepository repository, SavedStateHandle state) : AbstractViewModel(repository, state)
    {
        private readonly ApplicationRepository m_Repository = repository;

        public KanaScript KanaScript
        {
            get => m_Repository.Settings.KanaChartScript;
            set => m_Repository.Settings.KanaChartScript = value;
        }

        public async Task ToggleKanaScriptAsync()
        {
            switch (KanaScript)
            {
                case KanaScript.Hiragana:
                    KanaScript = KanaScript.Katakana;
                    break;
                case KanaScript.Katakana:
                    KanaScript = KanaScript.Hiragana;
                    break;
            }

            await m_Repository.SaveChangesAsync();
        }

        public class Factory(ApplicationRepository repository) : ViewModelFactory<KanaChartViewModel>
        {
            private readonly ApplicationRepository m_Repository = repository;
            protected override KanaChartViewModel CreateViewModel(SavedStateHandle state)
                => new KanaChartViewModel(m_Repository, state);
        }
    }
}