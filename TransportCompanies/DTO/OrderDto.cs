using System.ComponentModel.DataAnnotations;
using TransportCompanies.Models;

namespace TransportCompanies.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }     

        public ICollection<ItemDto> orderedItens { get; set; }

        [Required(ErrorMessage = "Status obrigatório")]
        public int StatusId { get; set; }


        [Required(ErrorMessage = "Cliente obrigatório")]
        public int CostumerId { get; set; }

        [Required(ErrorMessage = "Transportadora obrigatória")]
        public int TransportCompanyId { get; set; }

        public AddressDto origin { get; set; }

        public AddressDto destination { get; set; }

        public bool IsDispatched { get; set; }
       
    }
}
