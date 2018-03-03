using System.Collections.Generic;
using System.Threading.Tasks;
using dashboard.Core;
using dashboard.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Persistence
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly DashboardDbContext _context;

        public TeamMemberRepository(DashboardDbContext context)
        {
            _context = context;
        }

        public async Task<TeamMember> GetMemberById(int id)
        {
            return await _context.TeamMembers.FindAsync(id);
        }

        public async Task<TeamMember> GetTeamMemberByIdWithRelations(int id)
        {
            return await _context.TeamMembers
                .Include(tm => tm.Goals)
                .Include(tm => tm.Team)
                .SingleOrDefaultAsync(tm => tm.Id == id);
        }

        public async Task<IEnumerable<TeamMember>> GetMembers()
        {
            return await _context.TeamMembers
                .Include(tm => tm.Goals)
                .Include(tm => tm.Team)
                .ToListAsync();
        }

        public void Add(TeamMember member)
        {
            _context.TeamMembers.Add(member);
        }

        public void Remove(TeamMember member)
        {
            _context.TeamMembers.Remove(member);
        }
    }
}
