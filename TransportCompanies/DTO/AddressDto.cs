using System.ComponentModel.DataAnnotations;

namespace TransportCompanies.DTO
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3)]
        public string Cep { get; set; }

        public int Number { get; set; }
        public string? Rua { get; set; }

        public string? Bairro { get; set; }
        public string?  Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Complement { get; set; }
    }
}
