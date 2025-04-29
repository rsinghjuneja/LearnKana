using LearnKana.Domain.Study;

namespace LearnKana.Droid.Persistence
{
    public class AppDatabase
    {
        public Settings Settings { get; set; } = new Settings();
        public QuizSettings QuizSettings { get; set; } = new QuizSettings();
    }
}
