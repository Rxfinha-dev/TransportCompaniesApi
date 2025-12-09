using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

namespace TransportCompanies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostumerController : Controller
    {
        private readonly ICostumerService _costumerService;
        private readonly IMapper _mapper;

        public CostumerController(ICostumerService costumerService, IMapper mapper)
        {
            _costumerService = costumerService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Costumer>))]
        public async Task<IActionResult> GetCostumersAsync()
        {
            var costumers = _mapper.Map<List<CostumerDto>>(
                await _costumerService.GetCostumersAsync()
            );

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(costumers);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Costumer))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCostumerAsync(int id)
        {
            if (!await _costumerService.CostumerExistsAsync(id))
                return NotFound();

            var costumer = _mapper.Map<CostumerDto>(await _costumerService.GetCostumerAsync(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(costumer);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCostumer([FromBody] CostumerDto createCostumer)
        {
            if (createCostumer == null)
                return BadRequest(ModelState);

            string Normalize(string cpf) => cpf.Replace(".", "").Replace("-", "").Trim();

            var costumers = await _costumerService.GetCostumersAsync();

            var normalized = Normalize(createCostumer.Cpf);

            var costumer = costumers.FirstOrDefault(r => Normalize(r.Cpf) == normalized);

            if (costumer != null)
            {
                ModelState.AddModelError("", "Cliente já existe já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var costumerMap = _mapper.Map<Costumer>(createCostumer);

            if (!await _costumerService.CreateCostumerAsync(costumerMap))
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
        public async Task<IActionResult> UpdateCostumerAsync(int id, [FromBody] CostumerDto updatedCostumer)
        {
            if (updatedCostumer == null)
                return BadRequest(ModelState);
            if (id != updatedCostumer.Id)
                return BadRequest("id não coincide");
            if (!await _costumerService.CostumerExistsAsync(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var costumerMap = _mapper.Map<Costumer>(updatedCostumer);

            if (!await _costumerService.UpdateCostumerAsync(id, costumerMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao Atualizar o cliente");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCostumerAsync(int id)
        {
            if (!await _costumerService.CostumerExistsAsync(id))
                return NotFound();

            var costumerToDelete = await _costumerService.GetCostumerAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _costumerService.DeleteCostumerAsync(costumerToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();
        }
    }
}
