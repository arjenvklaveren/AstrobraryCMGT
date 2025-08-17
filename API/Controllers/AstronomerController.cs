using API.Entities;
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
        public async Task<ActionResult<IReadOnlyList<Astronomer>>> GetAllAstronomersAsync()
        {
            return Ok(await astronomerService.GetAllAstronomersAsync());
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
        public async Task<ActionResult<Astronomer>> AddAstronomer(Astronomer astronomer)
        {
            return Ok(astronomer);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Astronomer>> UpdateAstronomer(Astronomer astronomer)
        {
            return Ok(astronomer);
        }

        [HttpPost("delete/{id}")]
        public async Task<ActionResult> RemoveAstronomer(int id)
        {
            return Ok();
        }
    }
}
