using LearnKana.Droid.Persistence;

namespace LearnKana.Droid.Repositories
{
    public class ApplicationRepository(FileManager manager) : Repository(manager)
    {
        public Settings Settings { get; } = manager.AppDatabase.Settings;
    }
}
