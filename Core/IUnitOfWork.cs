using System.Threading.Tasks;

namespace dashboard.Core
{

    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}