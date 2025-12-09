using Microsoft.EntityFrameworkCore;
using TransportCompanies.Data;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Models;

namespace TransportCompanies.Repository
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DataContext _context;
        public StatusRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateStatus(Status status)
        {
            _context.Add(status);
                return await Save();
        }


        public async Task<bool> DeleteStatus(Status status)
        {
            _context.Remove(status);
            return await Save();
        }

        public async Task<Status> GetStatus(int id)
        {
            return _context.Statuses.Where(s => s.Id == id).FirstOrDefault();
        }

        public async Task<ICollection<Status>> GetStatuses()
        {
            return _context.Statuses.OrderBy(s=>s.Id).AsNoTracking().ToList();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<bool> StatusExists(int id)
        {
            return _context.Statuses.Any(s => s.Id == id);
        }

        public async Task<bool> UpdateStatus(Status status)
        {
            _context.Update(status);
            return await Save();
        }
    }
}
