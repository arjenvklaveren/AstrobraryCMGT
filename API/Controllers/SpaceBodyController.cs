using API.DTO_s;
using API.Entities;
using API.Helpers.Extensions;
using API.Helpers.Types;
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
        public async Task<ActionResult<IReadOnlyList<SpaceBodyDTO>>> GetAllBodiesAsync([FromQuery] SpaceBodyFilterParams filterParams)
        {
            return Ok(await spaceBodyService.GetAllBodiesAsync(filterParams));
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
        
        [HttpPost("add")]
        public async Task<ActionResult<int>> AddSpaceBody(SpaceBodyDTO spaceBody)
        {
            return Ok(await spaceBodyService.AddSpaceBodyAsync(spaceBody));
        }

        [HttpPut("update")]
        public async Task<ActionResult<SpaceBodyDTO>> UpdateSpaceBody(SpaceBodyDTO spaceBody)
        {
            return Ok(await spaceBodyService.UpdateSpaceBodyAsync(spaceBody));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> RemoveSpaceBody(int id)
        {
            await spaceBodyService.RemoveSpaceBodyAsync(id);
            return NoContent();
        }
    }
}
