using System.Collections.Generic;
using System.Threading.Tasks;
using TransportCompanies.Models;


namespace TransportCompanies.Interfaces.IRepository
{
    public interface ITrackingRepository
    {
        Task AddEventAsync(Tracking ev);
        Task<ICollection<Tracking>> GetEventsByOrderAsync(int orderId);
        Task<Tracking?> GetLatestEventAsync(int orderId);
        Task<bool> SaveAsync();
    }
}