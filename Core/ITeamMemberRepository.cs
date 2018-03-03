using dashboard.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dashboard.Core
{
    public interface ITeamMemberRepository
    {
        Task<TeamMember> GetMemberById(int id);
        Task<TeamMember> GetTeamMemberByIdWithRelations(int id);

        Task<IEnumerable<TeamMember>> GetMembers();

        void Add(TeamMember member);
        void Remove(TeamMember member);
    }
}
