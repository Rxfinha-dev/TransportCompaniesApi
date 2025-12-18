using Microsoft.EntityFrameworkCore.Metadata;
using TransportCompanies.Models;

namespace TransportCompanies.DTO
{
    public class SearchDto
    {
        public ICollection<SearchItemDto>? Orders { get; set; }
        public  ICollection<SearchItemDto>? Customers { get; set; }
        public ICollection<SearchItemDto>? Companies { get; set; }

    }
}
