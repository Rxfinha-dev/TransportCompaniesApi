using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface IStatusRepository
    {
        ICollection<Status> GetStatuses();
        Status GetStatus(int id);
        bool CreateStatus(Status status);
        bool UpdateStatus(Status status);
        bool DeleteStatus(Status status);
        bool StatusExists(int id);

        bool Save();
    }
}
