using Microsoft.EntityFrameworkCore;
using TransportCompanies.Data;
using TransportCompanies.DTO;
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

        public async Task<bool> CreateOrder(Order order)
        {
            _context.AddAsync(order);
            return await Save();
              
        }

        public async Task<bool> DeleteOrder(Order order)
        {
            _context.Remove(order);
            return await Save();
        }

        public async Task<Order> GetOrderToUpdate(int id, bool tracking = false)
        {
            var order = _context.Orders
                .Include(o => o.Costumer)
                    .Include(o => o.TransportCompany)
                        .Include(o => o.Status)
                            .AsQueryable();
            
            if (!tracking)  
            {
                order = order.AsNoTracking();
            }
            
            return await order.FirstOrDefaultAsync(o=>o.Id == id);
            
        }

        public async Task<ICollection<Order>> GetOrders()
        {
            return await _context.Orders.Select(o => new Order
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
                    Name = o.TransportCompany.Name
                }


            }).OrderBy(o => o.Id)
                .AsNoTracking()
                    .ToListAsync();
               
        }

        public async Task<bool> OrderExists(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> UpdateOrder(Order order)
        {
             _context.Update(order);
            return await Save();

        }

        public async Task<Order> GetOrderById(int id)
        {
            return _context.Orders.Where(o => o.Id == id)
                 .Select(o => new Order
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
                         Id = o.statusID,
                         Description = o.Status.Description
                     },

                     Costumer = new Costumer
                     {
                         Id = o.costumerId,
                         Name = o.Costumer.Name,
                         Cpf = o.Costumer.Cpf
                     },
                     TransportCompany = new TransportCompany
                     {
                         Id = o.transportCompanyId,
                         Name = o.TransportCompany.Name
                     }
                 })
                 .AsNoTracking()
                .FirstOrDefault();
        }
    }
}
