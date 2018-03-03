using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dashboard.Controllers.Resources;
using dashboard.Core;
using dashboard.Core.Models;
using dashboard.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace dashboard.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("/api/environments")]
    public class TeamEnvironmentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITeamEnvironmentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<TeamEnvironmentHub> _teamEnvironmentHub;

        public TeamEnvironmentsController(IHubContext<TeamEnvironmentHub> environmentHub, IMapper mapper, ITeamEnvironmentRepository repository, IUnitOfWork unitOfWork)
        {
            _teamEnvironmentHub = environmentHub;
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("team/{teamId}")]
        public async Task<IEnumerable<TeamEnvironmentResource>> GetTeamEnvironments(int teamId)
        {
            var teamEnvironEvironments = await _repository.GetTeamEvironments(teamId);

            return _mapper.Map<IEnumerable<TeamEnvironment>, IEnumerable<TeamEnvironmentResource>>(
                teamEnvironEvironments);
        }

        [HttpGet]
        public async Task<IEnumerable<TeamEnvironmentResource>> GetTeamEnvironments()
        {
            var teamEnvironEvironments = await _repository.GetTeamEvironments();

            return _mapper.Map<IEnumerable<TeamEnvironment>, IEnumerable<TeamEnvironmentResource>>(
                teamEnvironEvironments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamEnvironment(int id)
        {
            var teamEnvironment = await _repository.GetTeamEvironmentByIdWithRelations(id);
            if (teamEnvironment == null)
                return NotFound();

            var teamEnvironmentResource = _mapper.Map<TeamEnvironment, TeamEnvironmentResource>(teamEnvironment);

            return Ok(teamEnvironmentResource);
        }

        [HttpPost("/api/environments")]
        public async Task<IActionResult> CreateTeamEnvironment([FromBody] SaveTeamEnvironmentResource teamEnvironment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var environment = _mapper.Map<SaveTeamEnvironmentResource, TeamEnvironment>(teamEnvironment);

            _repository.Add(environment);
            await _unitOfWork.CompleteAsync();

            //Repository call
            environment = await _repository.GetTeamEvironmentByIdWithRelations(environment.Id);

            var result = _mapper.Map<TeamEnvironment, TeamEnvironmentResource>(environment);

            InvokeHub();

            return Ok(result);
        }


        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var environment = await _repository.GetTeamEvironmentById(id);
            if (environment == null)
            {
                return NotFound();
            }

            _repository.Remove(environment);
            await _unitOfWork.CompleteAsync();

            InvokeHub();

            return Ok(id);
        }

        private async void InvokeHub()
        {
            await _teamEnvironmentHub.Clients.All.InvokeAsync("UpdateTeamEnvironment", "Update Team Environment");
        }
    }
}