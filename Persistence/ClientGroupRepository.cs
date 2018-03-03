using System.Collections.Generic;
using System.Threading.Tasks;
using dashboard.Core;
using dashboard.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Persistence
{
    public class ClientGroupRepository : IClientGroupRepository
    {
        private readonly DashboardDbContext _context;

        public ClientGroupRepository(DashboardDbContext context)
        {
            _context = context;
        }


        public async Task<ClientGroup> GetClientGroupById(int id)
        {
            return await _context.ClientGroups.FindAsync(id);
        }

        public async Task<ClientGroup> GetClientGroupByIdWithRelations(int id)
        {
            return await _context.ClientGroups
                .SingleOrDefaultAsync( cg => cg.Id == id);
        }

        public async Task<IEnumerable<ClientGroup>> GetClientGroups()
        {
            return await _context.ClientGroups.ToListAsync();
        }

        public void Add(ClientGroup clientGroup)
        {
            _context.Add(clientGroup);
        }

        public void Remove(ClientGroup clientGroup)
        {
            _context.Remove(clientGroup);
        }
    }
}
