using Microsoft.EntityFrameworkCore;
using TransportCompanies.Data;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Models;

namespace TransportCompanies.Repository
{
    public class OrderRepositorycs : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepositorycs(DataContext context)
        {
            _context = context; 
        }

        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
              
        }

        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
        }

        public Order GetOrder(int id)
        {
            return _context.Orders.Include(s=>s.Status)
                .Include(c=>c.Costumer).
                Include(t=>t.TransportCompany).
                Where(o=>o.Id == id).FirstOrDefault();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.OrderBy(o=>o.Id)
                .Include(s => s.Status)
                .Include(c => c.Costumer).
                Include(t => t.TransportCompany).ToList();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrder(Order order)
        {
             _context.Update(order);
            return Save();


        }
    }
}
