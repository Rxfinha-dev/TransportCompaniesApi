namespace TransportCompanies.DTO
{
    public class TrackingDto
    {
        public int StatusId { get; set; }
        public string Message { get; set; }
        public  string? Location { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
