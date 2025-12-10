namespace TransportCompanies.Models
{
    public class Tracking
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        public string? Message { get; set; }
        public string? Location { get; set; }

        public DateTime CreatedAt { get; set; } =DateTime.UtcNow;
    }
}
