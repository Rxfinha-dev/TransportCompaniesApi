using System.ComponentModel.DataAnnotations;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace TransportCompanies.DTO
{
    
    public class ItemDto
    {

        [Required(ErrorMessage = "Amount é obrigatório")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Quantity é obrigatório")]
        public int Quantity { get; set; }
    }
}
