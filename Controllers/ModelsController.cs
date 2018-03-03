using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using dashboard.Controllers.Resources;
using dashboard.Core.Models;
using dashboard.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Controllers
{
    public class ModelsController : Controller
    {
        private readonly DashboardDbContext context;
        private readonly IMapper mapper;
        public ModelsController(DashboardDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("api/models")]
        public async Task<IEnumerable<ModelResource>> GetModels(){

            var models = await context.Models.ToListAsync();

            return mapper.Map<List<Model>, List<ModelResource>>(models);
        }
    }
}