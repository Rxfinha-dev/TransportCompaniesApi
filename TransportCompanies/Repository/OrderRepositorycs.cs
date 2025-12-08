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

        public Order GetOrder(int id, bool tracking = false)
        {
            var order = _context.Orders.Include(o => o.Costumer)
                .Include(o => o.Status)
                .Include(o => o.TransportCompany)
                .Where(o => o.Id == id);


            if (!tracking)
            {
                order = order.AsNoTracking();
            }
                
            
            return order.FirstOrDefault();

          
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.Select(o => new Order
            {

                Id = o.Id,
                orderedItens = o.orderedItens,
                Origin = o.Origin,
                Destination = o.Destination,
                statusID = o.statusID,
                costumerId = o.costumerId,
                transportCompanyId = o.TransportCompany.Id,
                IsDispatched = o.IsDispatched,

                Status = new Status
                {
                    Description = o.Status.Description
                },

                Costumer = new Costumer
                {
                    Name = o.Costumer.Name,
                    Cpf = o.Costumer.Cpf
                },
                TransportCompany = new TransportCompany
                {
                    Name = o.Costumer.Name
                }


            }).OrderBy(o => o.Id).AsNoTracking().ToList();
               
               
               
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
