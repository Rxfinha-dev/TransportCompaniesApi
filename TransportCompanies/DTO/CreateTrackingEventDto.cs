namespace TransportCompanies.DTO
{
    public class CreateTrackingEventDto
    {
        public int StatusId { get; set; }
        public  string? Message { get; set; }
        public string? Location { get; set; }
    }
}
