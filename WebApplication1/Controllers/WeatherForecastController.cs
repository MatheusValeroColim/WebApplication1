using Microsoft.AspNetCore.Mvc;
using WebApplication1.Domain;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SucosController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public SucosController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Suco>>> Get()
        {
            var produtos = await _mongoDBService.GetAll();
            return produtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Suco>> Get(string id)
        {
            var produto = await _mongoDBService.GetById(id);

            if (produto == null)
            {
                return NotFound();
            }

            return produto;
        }

        [HttpPost]
        public async Task<ActionResult<Suco>> Post(Suco suco)
        {
            await _mongoDBService.Create(suco);
            return CreatedAtAction("Get", new { id = suco.Id }, suco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Suco suco)
        {
            if (id != suco.Id.ToString())
            {
                return BadRequest();
            }

            await _mongoDBService.Update(suco);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mongoDBService.Delete(id);
            return NoContent();
        }
    }
}
