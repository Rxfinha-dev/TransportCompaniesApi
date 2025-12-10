using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TransportCompanies.Data;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Models;

namespace TransportCompanies.Repository
{
    public class TransportCompanyRepository : ITransportCompanyRepository
    {
        private readonly DataContext _context;
        public TransportCompanyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTransportCompany(TransportCompany company)
        {
           await _context.AddAsync(company);
            return await Save();
          
        }

        public async Task<bool> DeleteTransportCompany(TransportCompany company)
        {
            _context.Remove(company);
            return await Save();
        }

        public async Task<ICollection<TransportCompany>> GetTransportCompanies()
        {
            return await _context.TransportCompanies
                .OrderBy(t=>t.Id)
                    .AsNoTracking()
                        .ToListAsync();
        }

        public async Task<TransportCompany> GetTransportCompany(int id)
        {
            return await _context.TransportCompanies
                .Where(t => t.Id == id)
                    .AsNoTracking()
                         .FirstOrDefaultAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
             return await saved > 0 ? true : false;
        }

        public async Task<bool> TransportCompanyExists(int id)
        {
            return _context.TransportCompanies.Any(t=>t.Id == id);
        }

        public async Task<bool> UpdateTransportCompany(TransportCompany company)
        {
            _context.Update(company);
            return await Save();
        }
    }
}
