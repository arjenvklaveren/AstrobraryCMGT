using API.Entities;
using API.Interfaces;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpaceBodyController(ISpaceBodyService spaceBodyService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<SpaceBody>>> GetAllBodiesAsync()
        {
            return Ok(await spaceBodyService.GetAllBodiesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceBody>> GetBodyById(int id)
        {
            var spaceBody = await spaceBodyService.GetBodyByIdAsync(id);
            if (spaceBody != null) return Ok(spaceBody);
            return NotFound("Spacebody not found");
        }
    }
}
