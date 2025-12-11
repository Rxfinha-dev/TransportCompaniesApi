using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransportCompanies.Data;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

namespace TransportCompanies.Controllers
{
    [ApiController]
    [Route("api/orders/{orderId:int}/tracking")]
    public class TrackingController : Controller
    {
        private readonly ITrackingService _trackingService;
        private readonly DataContext _context;

        public TrackingController(ITrackingService trackingService, DataContext context)
        {
            _trackingService = trackingService;
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddEvent(
            int orderId,
            [FromBody] CreateTrackingEventDto dto
        )
        {
            if (dto == null)
                return BadRequest();

            await _trackingService.AddTrackingEventAsync(orderId, dto);
            return NoContent();
        }



        [HttpGet("quantity")]
        [ProducesResponseType(200, Type = typeof(int))]

        public async Task<IActionResult> CountEvents(int orderId, DateTime? Filter = null)
        {
            int quantity;
            if(Filter != null)
            {
                quantity = _context.TrackingEvents.Where(e => e.OrderId == orderId && e.CreatedAt <= Filter).Count();

            }
            else
            {
                 quantity = _context.TrackingEvents.Where(e => e.OrderId == orderId).Count();
            }

            return Ok(quantity);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Tracking>))]
        public async Task<ActionResult<ICollection<TrackingDto>>> GetHistory(int orderId)
        {
            var history = await _trackingService.GetTrackingHistoryAsync(orderId);
            return Ok(history);
        }

        [HttpGet("latest")]
        [ProducesResponseType(200, Type = typeof(Tracking))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TrackingDto>> GetLatest(int orderId)
        {
            var ev = await _trackingService.GetLatestEventAsync(orderId);
            if (ev == null)
                return NotFound();
            return Ok(ev);
        }
    }
}
