using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Core;
using dashboard.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Persistence
{
    public class TeamRepository : ITeamRepository
    {
        public readonly DashboardDbContext _context;

        public TeamRepository(DashboardDbContext context)
        {
            _context = context;
        }


        public async Task<Team> GetTeamById(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task<Team> GetTeamByIdWithRelations(int id)
        {
            return await _context.Teams
                .Include(t => t.Members)
                    .ThenInclude(tm => tm.Goals)
                .Include(t => t.UpcomingEvents)
                .Include(t => t.TeamEnvironments)
                    .ThenInclude(te => te.ClientGroup)
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _context.Teams
                .Include(t => t.Members)
                    .ThenInclude(tm => tm.Goals)
                .Include(t => t.TeamEnvironments)
                    .ThenInclude(te => te.ClientGroup)
                .ToListAsync();
        }

        public void Add(Team team)
        {
            _context.Add(team);
        }

        public void Remove(Team team)
        {
            _context.Remove(team);
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}