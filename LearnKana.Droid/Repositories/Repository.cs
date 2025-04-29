using System.Threading;
using System.Threading.Tasks;

using LearnKana.Droid.Persistence;

namespace LearnKana.Droid.Repositories
{
    public abstract class Repository(FileManager manager) : IRepository
    {
        private readonly FileManager m_Manager = manager;

        public async Task SaveChangesAsync(CancellationToken? token = default)
        {
            await m_Manager.SaveChangesAsync(token);
        }
    }
}
