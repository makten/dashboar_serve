using dashboard.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dashboard.Core
{
    public interface IClientGroupRepository
    {
        Task<ClientGroup> GetClientGroupById(int id);
        Task<ClientGroup> GetClientGroupByIdWithRelations(int id);
        Task<IEnumerable<ClientGroup>> GetClientGroups();

        void Add(ClientGroup clientGroup);
        void Remove(ClientGroup clientGroup);

    }
}
