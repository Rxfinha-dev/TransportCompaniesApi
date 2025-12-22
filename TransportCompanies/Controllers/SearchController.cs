using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using TransportCompanies.Data;
using TransportCompanies.DTO;

namespace TransportCompanies.Controllers
{
    [ApiController]
    [Route("api/search")]
    public sealed class SearchController : ControllerBase
    {
        private readonly DataContext _context;
        public SearchController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        [ProducesResponseType<SearchDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] string? filter = null)                                          
        {
            filter = filter?.ToLower().Trim();

            var searchResult = new SearchDto();

            searchResult.Orders = await _context.Orders
                .Where(o => o.Costumer.Name.ToLower().Contains(filter!) ||
                            o.TransportCompany.Name.ToLower().Contains(filter!) ||
                            o.Id.ToString().Contains(filter!))
                .Select(o => new SearchItemDto
                {
                    Id = o.Id,
                    Name = o.Costumer.Name + " - " + o.TransportCompany.Name
                })
                .ToListAsync();

            searchResult.Customers = await _context.Costumers
                .Where(c => c.Name.ToLower().Contains(filter!)) 
                .Select(c => new SearchItemDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            searchResult.Companies = await _context.TransportCompanies
                .Where(tc => tc.Name.ToLower().Contains(filter!))
                .Select(tc => new SearchItemDto
                {
                    Id = tc.Id,
                    Name = tc.Name
                })
                .ToListAsync(); 

            return Ok(searchResult);


        }
    }
}
