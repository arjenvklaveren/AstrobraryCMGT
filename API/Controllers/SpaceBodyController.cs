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
    }
}
