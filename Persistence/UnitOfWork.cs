using System.Threading.Tasks;
using dashboard.Core;

namespace dashboard.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DashboardDbContext context;

        public UnitOfWork(DashboardDbContext context)
        {
            this.context = context;

        }


        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}