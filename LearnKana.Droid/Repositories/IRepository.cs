using System.Threading;
using System.Threading.Tasks;

namespace LearnKana.Droid.Repositories
{
    public interface IRepository
    {
        public Task SaveChangesAsync(CancellationToken? token = default);
    }
}
