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
        public IActionResult GetCostumers()
        {
            var costumers = _mapper.Map<List<CostumerDto>>(_costumerService.GetCostumers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(costumers);
        }

        [HttpGet("{id}")]       
        [ProducesResponseType(200, Type = typeof(Costumer))]
        [ProducesResponseType(400)]
        public IActionResult GetCostumer(int id)
        {
            if (!_costumerService.CostumerExists(id))
                return NotFound();

            var costumer = _mapper.Map<CostumerDto>(_costumerService.GetCostumer(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(costumer);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCostumer([FromBody]CostumerDto createCostumer)
        {
            if (createCostumer == null)
                return BadRequest(ModelState);

            var costumer = _costumerService.GetCostumers().Where(r => r.Cpf.Trim().ToUpper() == createCostumer.Cpf.TrimEnd().ToUpper()).FirstOrDefault();


            if (costumer != null)
            {
                ModelState.AddModelError("", "Cliente já existe já existe");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var costumerMap = _mapper.Map<Costumer>(createCostumer);

            if (!_costumerService.CreateCostumer(costumerMap))
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
        public IActionResult UpdateCostumer(int id, [FromBody] CostumerDto updatedCostumer)
        {
            if (updatedCostumer == null)
                return BadRequest(ModelState);

            if (id != updatedCostumer.Id)
                return BadRequest(ModelState);

            if (!_costumerService.CostumerExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var costumerMap = _mapper.Map<Costumer>(updatedCostumer);

            if (!_costumerService.UpdateCostumer(costumerMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao atualizar o cliente ");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCostumer(int id)
        {
            if (!_costumerService.CostumerExists(id))
                return NotFound();

            var costumerToDelete = _costumerService.GetCostumer(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_costumerService.DeleteCostumer(costumerToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
            }

            return NoContent();

        }
    }
}
