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
            return _context.Orders.Select(o => new Order
            {

                Id = o.Id,
                orderedItens = o.orderedItens,
                Origin = o.Origin,
                Destination = o.Destination,

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


            }).FirstOrDefault();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.Select(o => new Order
            {

                Id = o.Id,
                orderedItens = o.orderedItens,
                Origin = o.Origin,
                Destination = o.Destination,

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


            }).OrderBy(o => o.Id).ToList();
               
               
               
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
