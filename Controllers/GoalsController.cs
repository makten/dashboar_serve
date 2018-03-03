using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dashboard.Controllers.Resources;
using dashboard.Core;
using dashboard.Core.Models;
using dashboard.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;


namespace dashboard.Controllers
{
    [Route("/api/goals")]
    public class GoalsController : Controller
    {
        private readonly IHubContext<GoalsHub> _goalsHub;
        private readonly IMapper _mapper;
        private readonly IGoalRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GoalsController(IHubContext<GoalsHub> goalsHub, IMapper mapper, IGoalRepository repository, IUnitOfWork unitOfWork)
        {
            _goalsHub = goalsHub;
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("/api/goals/{teamMemberId}")]
        public async Task<IEnumerable<GoalResource>> Get(int? teamMemberId)
        {
            IEnumerable<Goal> goals = new List<Goal>();
            if (teamMemberId != null)
            {
                goals = await _repository.GetGoals(teamMemberId);
            }
            else
            {
                goals = await _repository.GetGoals();
            }

            return _mapper.Map<IEnumerable<Goal>, IEnumerable<GoalResource>>(goals);
        }


        [HttpPost("/api/goals")]
        public async Task<IActionResult> CreateGoal([FromBody] SaveGoalResource goalResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var goal = _mapper.Map<SaveGoalResource, Goal>(goalResource);

            _repository.Add(goal);
            await _unitOfWork.CompleteAsync();

            //Repository call
            goal = await _repository.GetGoalByIdWithRelations(goal.Id);

            var result = _mapper.Map<Goal, GoalResource>(goal);

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
            var goal = await _repository.GetGoalById(id);
            if (goal == null)
            {
                return NotFound();
            }

            _repository.Remove(goal);
            await _unitOfWork.CompleteAsync();

            InvokeHub();

            return Ok(id);
        }

        private async void InvokeHub()
        {
            await _goalsHub.Clients.All.InvokeAsync("UpdateGoals", "Update Goals");
        }
    }


}
