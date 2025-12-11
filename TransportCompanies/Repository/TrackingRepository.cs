using Microsoft.EntityFrameworkCore;
using TransportCompanies.Data;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Models;

namespace TransportCompanies.Repository
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly DataContext _context;
        public TrackingRepository(DataContext context)
        {
            _context = context;
        }

     

        public async Task AddEventAsync(Tracking tracking)
        {
            await _context.TrackingEvents.AddAsync(tracking);
        }

        public async Task<ICollection<Tracking>> GetEventsByOrderAsync(int orderId)
        {
           return await _context.TrackingEvents
                .Where(e=>e.OrderId == orderId)
                    .OrderBy(e=>e.CreatedAt)
                        .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<Tracking?> GetLatestEventAsync(int orderId)
        {
            return await _context.TrackingEvents
                .Where(e => e.OrderId == orderId)
                    .OrderByDescending(e => e.CreatedAt)
                        .AsNoTracking()
                            .FirstOrDefaultAsync();
        }

  
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
