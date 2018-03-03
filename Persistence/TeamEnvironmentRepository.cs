using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Core;
using dashboard.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Persistence
{
    public class TeamEnvironmentRepository : ITeamEnvironmentRepository
    {
        private readonly DashboardDbContext _context;

        public TeamEnvironmentRepository(DashboardDbContext context)
        {
            _context = context;
        }

        public Task<TeamEnvironment> GetTeamEvironmentById(int id)
        {
            return _context.TeamEnvironments.FindAsync(id);
        }

        public Task<TeamEnvironment> GetTeamEvironmentByIdWithRelations(int id)
        {
            return _context.TeamEnvironments
                .Include(t => t.ClientGroup)
                .SingleOrDefaultAsync(te => te.Id == id);
        }

        public async Task<IEnumerable<TeamEnvironment>> GetTeamEvironments(int teamId)
        {
            return await _context.TeamEnvironments
                .Include(t => t.ClientGroup)
                .Where(te => te.Id == teamId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeamEnvironment>> GetTeamEvironments()
        {
            return await _context.TeamEnvironments
                .Include(t => t.ClientGroup)
                .ToListAsync();
        }

        public void Add(TeamEnvironment environment)
        {
            _context.Add(environment);
        }

        public void Remove(TeamEnvironment environment)
        {
            _context.Remove(environment);
        }
    }
}
