using Contract.Interfaces.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace InvenTechEventManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IRepositoryManager _repo;

        public EventsController(IRepositoryManager repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var eventsList = await _repo.Event.GetAllAsync();
            return Ok(eventsList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ev = await _repo.Event.GetByIdAsync(id);
            if (ev == null) return NotFound();

            return Ok(ev);
        }
    }
}
