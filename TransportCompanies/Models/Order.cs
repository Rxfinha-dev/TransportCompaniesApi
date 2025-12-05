using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using TransportCompanies.DTO;

namespace TransportCompanies.Models
{
    public class Order
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
