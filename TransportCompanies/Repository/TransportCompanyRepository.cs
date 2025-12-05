using Microsoft.EntityFrameworkCore;
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

        public bool CreateTransportCompany(TransportCompany company)
        {
            _context.Add(company);
            return Save();
        }

        public bool DeleteTransportCompany(TransportCompany company)
        {
            _context.Remove(company);
            return Save();
        }

        public ICollection<TransportCompany> GetTransportCompanies()
        {
            return _context.TransportCompanies.OrderBy(t=>t.Id).ToList();
        }

        public TransportCompany GetTransportCompany(int id)
        {
            return _context.TransportCompanies.Where(t => t.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TransportCompanyExists(int id)
        {
            return _context.TransportCompanies.Any(t=>t.Id == id);
        }

        public bool UpdateTransportCompany(TransportCompany company)
        {
            _context.Update(company);
            return Save();
        }
    }
}
