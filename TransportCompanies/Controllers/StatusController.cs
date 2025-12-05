using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

namespace TransportCompanies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;
        public StatusController(IStatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Status>))]
        public IActionResult GetStatuses()
        {
            var statuses = _mapper.Map<List<StatusDto>>(_statusService.GetStatuses());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(statuses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Status))]
        [ProducesResponseType(400)]
        public IActionResult GetRota(int id)
        {
            if (!_statusService.StatusExists(id))
                return NotFound();

            var rota = _mapper.Map<StatusDto>(_statusService.GetStatus(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rota);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRota([FromBody] StatusDto statusCreate)
        {
            if (statusCreate == null)
                return BadRequest(ModelState);

            if (_statusService.StatusExists(statusCreate.Id))
            {
                ModelState.AddModelError("", "Status já existe");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var statusMap = _mapper.Map<Status>(statusCreate);

            if (!_statusService.CreateStatus(statusMap))
            {
                ModelState.AddModelError("", "Alguma coisa deu errado ao salvar");
                return StatusCode(500, ModelState);
            }

            return Ok("Criado com sucesso");

        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRota(int id, [FromBody] StatusDto updatedStatus)
        {
            if (updatedStatus == null)
                return BadRequest(ModelState);

            if (id != updatedStatus.Id)
                return BadRequest(ModelState);

            if (!_statusService.StatusExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var statusMap = _mapper.Map<Status>(updatedStatus);

            if (!_statusService.UpdateStatus(id, statusMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao atualizar o status");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRota(int id)
        {
            if (!_statusService.StatusExists(id))
                return NotFound();

            var rotaToDelete = _statusService.GetStatus(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_statusService.DeleteStatus(rotaToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();

        }
    }
}
