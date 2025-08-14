using API.DTO_s;
using API.Entities;
using API.Helpers.Extensions;
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
        public async Task<ActionResult<IReadOnlyList<SpaceBodyDTO>>> GetAllBodiesAsync()
        {
            return Ok(await spaceBodyService.GetAllBodiesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceBodyDTO>> GetBodyById(int id)
        {
            var spaceBody = await spaceBodyService.GetBodyByIdAsync(id);
            if (spaceBody != null) return Ok(spaceBody);
            return NotFound("Spacebody not found");
        }

        [HttpGet("{id}/hierarchy")]
        public async Task<ActionResult<SpaceBodyDTO>> GetBodyHierarchyById(int id, bool full = true)
        {
            var spaceBody = full
                ? await spaceBodyService.GetRootSpaceBodyByIdAsync(id)
                : await spaceBodyService.GetBodyByIdAsync(id);

            if (spaceBody == null) return NotFound("Spacebody not found");

            SpaceBodyDTO? spaceBodyDTO = await spaceBodyService.GetSpaceBodyHierarchyAsync(spaceBody);
            
            return Ok(spaceBodyDTO);
        }
    }
}
