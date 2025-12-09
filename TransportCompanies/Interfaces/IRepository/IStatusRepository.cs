using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface IStatusRepository
    {
        Task<ICollection<Status>> GetStatuses();
        Task<Status> GetStatus(int id);
        Task<bool> CreateStatus(Status status);
        Task<bool> UpdateStatus(Status status);
        Task<bool> DeleteStatus(Status status);
        Task<bool> StatusExists(int id);

        Task<bool> Save();
    }
}
