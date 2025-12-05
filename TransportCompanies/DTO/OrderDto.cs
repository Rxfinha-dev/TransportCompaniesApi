using TransportCompanies.Models;

namespace TransportCompanies.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public ICollection<ItemDto> orderedItens { get; set; }
        public Costumer Costumer { get; set; }

        public AddressDto Origin { get; set; }

        public AddressDto Destination { get; set; }

        public TransportCompany TransportCompany { get; set; }
    }
}
