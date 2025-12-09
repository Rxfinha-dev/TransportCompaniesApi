using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

namespace TransportCompanies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportCompanyController : Controller
    {
        private readonly ITransportCompanyService _transportCompanyService;
        private readonly IMapper _mapper;
        public TransportCompanyController(ITransportCompanyService transportCompanyService, IMapper mapper)
        {
            _transportCompanyService = transportCompanyService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TransportCompany>))]
        public async Task<IActionResult> GetTransportCompaniesAsync()
        {
            var companies = _mapper.Map<List<TransportCompanyDto>>(await _transportCompanyService.GetTransportCompanies());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(companies);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TransportCompany))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTransportCompanyAsync(int id)
        {
            if (! await _transportCompanyService.TransportCompanyExists(id))
                return NotFound();

            var company = _mapper.Map<TransportCompanyDto>(await _transportCompanyService.GetTransportCompany(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(company);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateTransportCompanyAsync([FromBody]TransportCompanyDto company)
        {
            if (await _transportCompanyService.TransportCompanyExists(company.Id))
            {
                ModelState.AddModelError("", "Transportadora já existe");
                return StatusCode(422, ModelState);
            }
                

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyMap = _mapper.Map<TransportCompany>(company);

            if(!await _transportCompanyService.CreateTransportCompany(companyMap))
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
        public async Task<IActionResult> UpdateTransportCompanyAsync(int id, [FromBody]TransportCompanyDto updatedCompany)
        {
            if(updatedCompany == null)
                return BadRequest(ModelState);
            if(id != updatedCompany.Id)
                return BadRequest(ModelState);
            if(!await _transportCompanyService.TransportCompanyExists(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var companyMap = _mapper.Map<TransportCompany>(updatedCompany);

            if (!await _transportCompanyService.UpdateTransportCompany(id, companyMap))
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
        public async Task<IActionResult> DeleteTransportCompanyAsync(int id)
        {
            if (!await _transportCompanyService.TransportCompanyExists(id))
                return NotFound();

            var companyToDelete = await _transportCompanyService.GetTransportCompany(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _transportCompanyService.DeleteTransportCompany(companyToDelete))
            {
                ModelState.AddModelError("", "Algo deu errado ao deletar");
                return StatusCode(500, "Erro ao remover");
            }

            return NoContent();
        }
    }
}
