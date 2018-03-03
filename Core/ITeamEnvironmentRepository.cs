using dashboard.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dashboard.Core
{
    public interface ITeamEnvironmentRepository
    {
        Task<TeamEnvironment> GetTeamEvironmentById(int id);
        Task<TeamEnvironment> GetTeamEvironmentByIdWithRelations(int id);

        Task<IEnumerable<TeamEnvironment>> GetTeamEvironments(int teamId);
        Task<IEnumerable<TeamEnvironment>> GetTeamEvironments();

        void Add(TeamEnvironment environment);
        void Remove(TeamEnvironment environment);
    }
}
