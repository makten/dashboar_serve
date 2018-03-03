namespace dashboard.Persistence
{
    public abstract class DbRepository
    {
        private readonly DashboardDbContext _context;

        public DbRepository(DashboardDbContext _context)
        {
            this._context = _context;
        }
    }
}
