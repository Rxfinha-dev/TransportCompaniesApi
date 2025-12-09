using System.ComponentModel.DataAnnotations;

namespace TransportCompanies.DTO
{
    public class UpdateAdressDto
    {
        [Required(ErrorMessage = "Origem obrigatória")]
        public AddressDto Origin { get; set; }

        [Required(ErrorMessage = "Destino obrigatório")]
        public AddressDto Destination { get; set; }
    }
}
