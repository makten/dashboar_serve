using dashboard.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dashboard.Core
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeams();
        Task<Team> GetTeamById(int id);
        Task<Team> GetTeamByIdWithRelations(int id);        

        void Add(Team item);
        void Remove(Team item);
    }
}
