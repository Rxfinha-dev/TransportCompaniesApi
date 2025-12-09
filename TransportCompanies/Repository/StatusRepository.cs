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

        public bool CreateStatus(Status status)
        {
            _context.Add(status);
                return Save();
        }


        public bool DeleteStatus(Status status)
        {
            _context.Remove(status);
            return Save();
        }

        public Status GetStatus(int id)
        {
            return _context.Statuses.Where(s => s.Id == id).FirstOrDefault();
        }

        public ICollection<Status> GetStatuses()
        {
            return _context.Statuses.OrderBy(s=>s.Id).AsNoTracking().ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool StatusExists(int id)
        {
            return _context.Statuses.Any(s => s.Id == id);
        }

        public bool UpdateStatus(Status status)
        {
            _context.Update(status);
            return Save();
        }
    }
}
