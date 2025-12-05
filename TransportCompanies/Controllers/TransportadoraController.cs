using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

namespace TransportCompanies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportadoraController : Controller
    {
        private readonly ITransportCompanyService _transportCompanyService;
        private readonly IMapper _mapper;
        public TransportadoraController(ITransportCompanyService transportCompanyService, IMapper mapper)
        {
            _transportCompanyService = transportCompanyService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportCompany>))]
        public IActionResult GetTransportCompanies()
        {
            var companies = _mapper.Map<List<TransportCompanyDto>>(_transportCompanyService.GetTransportCompanies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(companies);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TransportCompany))]
        [ProducesResponseType(400)]
        public IActionResult GetTransportCompany(int id)
        {
            if (!_transportCompanyService.TransportCompanyExists(id))
                return NotFound();

            var company = _mapper.Map<TransportCompanyDto>(_transportCompanyService.GetTransportCompany(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTransportCompany([FromBody]TransportCompanyDto company)
        {
            if (_transportCompanyService.TransportCompanyExists(company.Id))
            {
                ModelState.AddModelError("", "Transportadora já existe");
                return StatusCode(422, ModelState);
            }
                

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyMap = _mapper.Map<TransportCompany>(company);

            if(!_transportCompanyService.CreateTransportCompany(companyMap))
            {
                ModelState.AddModelError("","Algo deu errado ao criar a Transportadora");
                return StatusCode(500, ModelState);
            }

            return Ok("Transportadora criada com sucesso");

            
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTransportCompany(int id, [FromBody]TransportCompanyDto updatedCompany)
        {
            if(updatedCompany == null)
                return BadRequest(ModelState);
            if(id != updatedCompany.Id)
                return BadRequest(ModelState);
            if(!_transportCompanyService.TransportCompanyExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var companyMap = _mapper.Map<TransportCompany>(updatedCompany);

            if (!_transportCompanyService.UpdateTransportCompany(id, companyMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao Atualizar a transportadora");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTransportCompany(int id)
        {
            if (!_transportCompanyService.TransportCompanyExists(id))
                return NotFound();

            var companyToDelete = _transportCompanyService.GetTransportCompany(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_transportCompanyService.DeleteTransportCompany(companyToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
                return StatusCode(500, "Erro ao remover");
            }

            return NoContent();
        }
    }
}
