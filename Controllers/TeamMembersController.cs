using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dashboard.Controllers.Resources;
using dashboard.Core;
using dashboard.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dashboard.Controllers
{
    [Route("/api/members")]
    public class TeamMembersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITeamMemberRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamMembersController(IMapper mapper, ITeamMemberRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("/api/members")]
        public async Task<IEnumerable<TeamMemberResource>> GetTeamMembers()
        {
            var members = await _repository.GetMembers();
            return _mapper.Map<IEnumerable<TeamMember>, IEnumerable<TeamMemberResource>>(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamMember([FromRoute] int id)
        {
            var teamMember = await _repository.GetTeamMemberByIdWithRelations(id);

            if (teamMember == null)
                return NotFound();

            var teamMemberResource = _mapper.Map<TeamMember, TeamMemberResource>(teamMember);

            return Ok(teamMemberResource);
        }
        
        [HttpPost]
        public async Task<IActionResult> TeamMember([FromForm]TeamMember member)
        {
            _repository.Add(member);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        
        [HttpPut("{id}")]
        public void PutTeamMember(int id, [FromBody]string value)
        {
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var teamMember = await _repository.GetMemberById(id);

            if (teamMember == null)
                return NotFound();

            _repository.Remove(teamMember);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
