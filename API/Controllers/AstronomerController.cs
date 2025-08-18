using API.Entities;
using API.Helpers.Types;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AstronomerController(IAstronomerService astronomerService, ISpaceBodyService spaceBodyService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Astronomer>>> GetAllAstronomersAsync([FromQuery] AstronomerFilterParams filterParams)
        {
            return Ok(await astronomerService.GetAllAstronomersAsync(filterParams));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SpaceBody>> GetAstronomerById(int id)
        {
            var astronomer = await astronomerService.GetAstronomerByIdAsync(id);
            if (astronomer != null) return Ok(astronomer);
            return NotFound("Astronomer not found");
        }

        [HttpGet("{id}/discoveries")]
        public async Task<ActionResult<SpaceBody>> GetSpaceBodiesOfAstronomerById(int id)
        {
            return Ok(await spaceBodyService.GetAllBodiesOfAstronomer(id));
        }

        [HttpPost("add")]
        public async Task<ActionResult<int>> AddAstronomer(Astronomer astronomer)
        {         
            return Ok(await astronomerService.AddAstronomerAsync(astronomer));
        }

        [HttpPut("update")]
        public async Task<ActionResult<Astronomer>> UpdateAstronomer(Astronomer astronomer)
        {
            return Ok(await astronomerService.AddAstronomerAsync(astronomer));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> RemoveAstronomer(int id)
        {
            await astronomerService.RemoveAstronomerAsync(id);
            return NoContent();
        }
    }
}
