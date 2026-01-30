using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Requests.Event;
using Service.Contracts.DTOs.Event;
using Service.Contracts.IService;
using Service.Contracts.Manager;
using AutoMapper;
using Shared.RequestFeatures.Parameters;
namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IServiceManager _service;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public EventController(IServiceManager service, IFileService fileService, IMapper mapper)
        {
            _service = service;
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery]EventParameter eventParameter)
        {
            var eventsList = await _service.Event.GetEventsAsync(eventParameter);
            return Ok(eventsList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var ev = await _service.Event.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromForm] CreateEventRequest eventRequest)
        {
            var attachments = new List<EventAttachmentDto>();

            if (eventRequest.AttachmentFiles != null)
            {
                foreach (var file in eventRequest.AttachmentFiles)
                {
                    using var stream = file.OpenReadStream();
                    var path = await _fileService.SaveFileAsync(stream, file.FileName, "Events");

                    attachments.Add(new EventAttachmentDto
                    {
                        OriginalFileName = file.FileName,
                        FilePath = path,
                        ContentType = file.ContentType,
                        FileSize = file.Length
                    });
                }
            }
            

            var eventDto = _mapper.Map<CreateEventDto>(eventRequest);
            eventDto.Attachments = attachments;

            var createdEvent = await _service.Event.CreateEventAsync(eventDto);

            return CreatedAtAction(
                nameof(GetEventById),
                new { id = createdEvent.EventId },
                createdEvent
            );
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _service.Event.DeleteEvent(id);
            return NoContent();
        }
    }
}
