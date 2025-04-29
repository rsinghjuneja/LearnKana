using System.Threading;
using System.Threading.Tasks;

using LearnKana.Shared;

namespace LearnKana.Droid.Persistence
{
    public class FileManager(string directory, string filename, AppDatabase database)
    {
        private readonly string m_Directory = directory;
        private readonly string m_FileName = filename;

        public AppDatabase AppDatabase { get; } = database;

        public async Task SaveChangesAsync(CancellationToken? token = default)
        {
            await JsonDatabase.SaveFileAsync(AppDatabase, m_Directory, m_FileName, token);
        }

        public static FileManager Create(string directory, string filename, CancellationToken? token = default)
            => CreateAsync(directory, filename, token).GetAwaiter().GetResult();

        public static async Task<FileManager> CreateAsync(string directory, string filename, CancellationToken? token = default)
        {
            AppDatabase? database = await JsonDatabase.ReadFileAsync<AppDatabase>(directory, filename, token);
            return new FileManager(directory, filename, database ?? new AppDatabase());
        }
    }
}