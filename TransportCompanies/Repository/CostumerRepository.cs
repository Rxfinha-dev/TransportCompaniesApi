using Microsoft.EntityFrameworkCore;
using TransportCompanies.Data;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Models;

namespace TransportCompanies.Repository
{
    public class CostumerRepository : ICostumerRepository
    {
        private readonly DataContext _context;
        public CostumerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CostumerExists(int id)
        {
            return _context.Costumers.Any(c => c.Id == id);
        }

        public async Task<bool> CostumerExists(string cpf)
        {
            return _context.Costumers.Any(c=>c.Cpf.ToUpper() == cpf.ToUpper());
        }

        public async Task<bool> CreateCostumer(Costumer costumer)
        {
           await _context.AddAsync(costumer);
            return await Save();

        }

        public async Task<bool> DeleteCostumer(Costumer costumer)
        {
            _context.Remove(costumer);
            return await Save();

        }

        public async Task<Costumer> GetCostumer(int id)
        {
            return await _context.Costumers
                .Where(c => c.Id == id)
                    .AsNoTracking()
                        .FirstOrDefaultAsync();
        }

        public async Task<Costumer> GetCostumer(string cpf)
        {
            return await _context.Costumers
                .Where(c => c.Cpf == cpf)
                    .AsNoTracking()
                        .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Costumer>> GetCostumers()
        {
           return await _context.Costumers
                .OrderBy(c=>c.Id)
                    .AsNoTracking()
                         .ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> UpdateCostumer(Costumer costumer)
        {
            _context.Update(costumer);
            return await Save();
        }
    }
}
