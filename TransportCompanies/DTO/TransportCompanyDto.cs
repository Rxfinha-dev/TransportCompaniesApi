using System.ComponentModel.DataAnnotations;

namespace TransportCompanies.DTO
{
    public class TransportCompanyDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome da Transportadora obrigatório")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

    }
}
