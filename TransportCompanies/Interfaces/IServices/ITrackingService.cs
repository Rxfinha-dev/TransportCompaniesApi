using TransportCompanies.DTO;

namespace TransportCompanies.Interfaces.IServices
{
    public interface ITrackingService
    {
        Task AddTrackingEventAsync(int orderId, CreateTrackingEventDto dto);
        Task<ICollection<TrackingDto>> GetTrackingHistoryAsync(int orderId);
        Task<TrackingDto?> GetLatestEventAsync(int orderId);
    }
}
