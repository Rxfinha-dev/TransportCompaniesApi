using System.ComponentModel.DataAnnotations;

namespace TransportCompanies.DTO
{
    public class CostumerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage =  "CPF é obrigatório")]
        [StringLength(11,MinimumLength = 11)]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos")]
        public string Cpf { get; set; }
    }
}
