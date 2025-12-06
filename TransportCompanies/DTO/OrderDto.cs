using TransportCompanies.Models;

namespace TransportCompanies.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }     

        public ICollection<ItemDto> orderedItens { get; set; }
        public int StatusId { get; set; }
        public int CostumerId { get; set; }
        public int TransportCompanyId { get; set; }

        public AddressDto origin { get; set; }

        public AddressDto destination { get; set; }

        public bool IsDispatched { get; set; }
       
    }
}
