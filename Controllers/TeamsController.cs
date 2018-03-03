using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dashboard.Controllers.Resources;
using dashboard.Core.Models;
using dashboard.Core;
using Microsoft.AspNetCore.Authorization;
using System.Collections.ObjectModel;
using dashboard.Controllers;

namespace dashboard.Controllers
{
    [Route("/api/teams")]
    public class TeamsController : Controller
    {
        public readonly IMapper Mapper;
        private readonly ITeamRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamsController(IMapper mapper, ITeamRepository repository, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<TeamResource>> GetTeams()
        {
            var teams = await _repository.GetTeams();
            return Mapper.Map<IEnumerable<Team>, IEnumerable<TeamResource>>(teams);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam([FromRoute] int id)
        {
            var team = await _repository.GetTeamByIdWithRelations(id);

            if (team == null)
                return NotFound();

            var teamResource = Mapper.Map<Team, TeamResource>(team);

            return Ok(teamResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, [FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teamsssss = await _repository.GetTeamById(id);

            if (teamsssss == null)
                return NotFound();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostTeam([FromForm] Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Add(team);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _repository.GetTeamById(id);
            if (team == null)
                return NotFound();

            _repository.Remove(team);
            await _unitOfWork.CompleteAsync();

            return Ok(team);
        }
    }
}