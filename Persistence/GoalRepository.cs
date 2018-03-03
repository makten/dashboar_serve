using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dashboard.Core;
using dashboard.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Persistence
{
    public class GoalRepository : IGoalRepository
    {
        private readonly DashboardDbContext _context;

        public GoalRepository(DashboardDbContext _context)
        {
            this._context = _context;
        }


        public async Task<Goal> GetGoalById(int id)
        {
            return await _context.Goals.FindAsync(id);
        }

        public async Task<Goal> GetGoalByIdWithRelations(int id)
        {
            return await _context.Goals.Include(g => g.TeamMember).SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Goal>> GetGoals()
        {
            return await _context.Goals.Include(g => g.TeamMember).ToListAsync();
        }

        public async Task<IEnumerable<Goal>> GetGoals(int? teamMemberId)
        {
            return await _context.Goals.Include(g => g.TeamMember).Where(g => g.TeamMemberId == teamMemberId)
                .ToListAsync();
        }

        public void Add(Goal goal)
        {
            _context.Goals.Add(goal);
        }

        public void Remove(Goal goal)
        {
            _context.Goals.Remove(goal);
        }
    }
}