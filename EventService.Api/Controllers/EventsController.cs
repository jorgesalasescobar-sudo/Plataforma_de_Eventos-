using EventService.Application.DTOs;
using EventService.Application.Messages; // <-- aquí está tu contrato EventCreated
using EventService.Domain.Entities;
using EventService.Infrastructure.Persistence;
using MassTransit; // <-- importante
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly EventDbContext _context;

        public EventsController(EventDbContext context)
        {
            _context = context;
        }

        // POST: api/events
        [HttpPost]
        public async Task<IActionResult> CreateEvent(
            [FromBody] CreateEventDto dto,
            [FromServices] IPublishEndpoint publishEndpoint) // <-- se inyecta aquí
        {
            var newEvent = new EventService.Domain.Entities.Event
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Date = dto.Date,
                Location = dto.Location,
                Zones = dto.Zones.Select(z => new Zone
                {
                    Id = Guid.NewGuid(),
                    Name = z.Name,
                    Price = z.Price,
                    Capacity = z.Capacity
                }).ToList()
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            // Crear mensaje EventCreated
            var message = new EventCreated(
                Guid.NewGuid(),          // messageId
                newEvent.Id,             // eventId
                newEvent.Name,           // name
                DateTime.UtcNow,         // occurredAt
                Guid.NewGuid(),          // correlationId
                1                        // version
            );

            // Publicar en RabbitMQ
            await publishEndpoint.Publish(message);

            return Ok(new { newEvent.Id });
        }

        // GET: api/events
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events
                .Include(e => e.Zones)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Date,
                    e.Location,
                    Zones = e.Zones.Select(z => new
                    {
                        z.Id,
                        z.Name,
                        z.Price,
                        z.Capacity
                    })
                })
                .ToListAsync();

            return Ok(events);
        }
    }
}
