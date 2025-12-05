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

        public bool CostumerExists(int id)
        {
            return _context.Costumers.Any(c => c.Id == id);
        }

        public bool CostumerExists(string cpf)
        {
            return _context.Costumers.Any(c=>c.Cpf.ToUpper() == cpf.ToUpper());
        }

        public bool CreateCostumer(Costumer costumer)
        {
            _context.Add(costumer);
            return Save();

        }

        public bool DeleteCostumer(Costumer costumer)
        {
            _context.Remove(costumer);
            return Save();

        }

        public Costumer GetCostumer(int id)
        {
            return _context.Costumers.Where(c => c.Id == id).FirstOrDefault();
        }

        public Costumer GetCostumer(string cpf)
        {
            return _context.Costumers.Where(c => c.Cpf == cpf).FirstOrDefault();
        }

        public ICollection<Costumer> GetCostumers()
        {
           return _context.Costumers.OrderBy(c=>c.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCostumer(Costumer costumer)
        {
            _context.Update(costumer);
            return Save();
        }
    }
}
