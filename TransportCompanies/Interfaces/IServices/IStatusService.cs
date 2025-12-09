using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface IStatusService
    {
        Task<ICollection<Status>> GetStatuses();
        Task<Status> GetStatus(int id);
        Task<bool> CreateStatus(Status status);
        Task<bool> UpdateStatus(int id, Status status);
        Task<bool> DeleteStatus( Status status);
        Task<bool> StatusExists(int id);
    }
}
