namespace TransportCompanies.DTO
{
    public class AddressDto
    {
        public string Cep { get; set; }

        public int Number { get; set; }
        public string? Rua { get; set; }

        public string? Bairro { get; set; }
        public string?  Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Complement { get; set; }
    }
}
