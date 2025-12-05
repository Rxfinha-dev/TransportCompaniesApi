using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface IStatusService
    {
        ICollection<Status> GetStatuses();
        Status GetStatus(int id);
        bool CreateStatus(Status status);
        bool UpdateStatus(int id, Status status);
        bool DeleteStatus( Status status);
        bool StatusExists(int id);
    }
}
