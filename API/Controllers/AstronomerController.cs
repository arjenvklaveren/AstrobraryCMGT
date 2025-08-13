using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AstronomerController(IAstronomerService astronomerService) : ControllerBase
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
    }
}
