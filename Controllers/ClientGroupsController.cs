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
    [Route("/api/clientgroups")]
    public class ClientGroupsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IClientGroupRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientGroupsController(IMapper mapper, IClientGroupRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("/api/clientgroups")]
        public async Task<IEnumerable<ClientGroupResource>> GetClientGroups()
        {
            var clientgroups = await _repository.GetClientGroups();
            return _mapper.Map<IEnumerable<ClientGroup>, IEnumerable<ClientGroupResource>>(clientgroups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientGroup([FromRoute] int id)
        {
            var clientGroup = await _repository.GetClientGroupByIdWithRelations(id);

            if (clientGroup == null)
                return NotFound();

            var clientGroupResource = _mapper.Map<ClientGroup, ClientGroupResource>(clientGroup);

            return Ok(clientGroupResource);
        }
        
        [HttpPost]
        public async Task<IActionResult> TeamMember([FromBody]ClientGroup clientGroup)
        {
            _repository.Add(clientGroup);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
        
        [HttpPut("{id}")]
        public void PutClientGroup(int id, [FromBody]string value)
        {
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientGroup(int id)
        {
            var clientGroup = await _repository.GetClientGroupByIdWithRelations(id);

            if (clientGroup == null)
                return NotFound();

            _repository.Remove(clientGroup);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
