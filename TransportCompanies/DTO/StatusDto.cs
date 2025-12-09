using System.ComponentModel.DataAnnotations;

namespace TransportCompanies.DTO
{
    public class StatusDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Status é obrigatório")]
        [StringLength(20, MinimumLength = 3)]
        public string Description { get; set; }
        
    }
}
