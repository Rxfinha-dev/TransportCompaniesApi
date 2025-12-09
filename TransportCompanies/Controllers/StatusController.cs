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
        public async Task<IActionResult> GetStatusesAsync()
        {
            var statuses = _mapper.Map<List<StatusDto>>(await _statusService.GetStatuses());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(statuses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Status))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetRotaAsync(int id)
        {
            if (!await _statusService.StatusExists(id))
                return NotFound();

            var rota = _mapper.Map<StatusDto>(await _statusService.GetStatus(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rota);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateRotaAsync([FromBody] StatusDto statusCreate)
        {
            if (statusCreate == null)
                return BadRequest(ModelState);

            if (await _statusService.StatusExists(statusCreate.Id))
            {
                ModelState.AddModelError("", "Status já existe");
                return StatusCode(422, ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var statusMap = _mapper.Map<Status>(statusCreate);

            if (!await _statusService.CreateStatus(statusMap))
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
        public async Task<IActionResult> UpdateRotaAsync(int id, [FromBody] StatusDto updatedStatus)
        {
            if (updatedStatus == null)
                return BadRequest(ModelState);

            if (id != updatedStatus.Id)
                return BadRequest(ModelState);

            if (!await _statusService.StatusExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var statusMap = _mapper.Map<Status>(updatedStatus);

            if (!await _statusService.UpdateStatus(id, statusMap))
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
        public async Task<IActionResult> DeleteRotaAsync(int id)
        {
            if (!await _statusService.StatusExists(id))
                return NotFound();

            var rotaToDelete = await _statusService.GetStatus(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _statusService.DeleteStatus(rotaToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();

        }
    }
}
