using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface IOrderRepository
    {
        Task<ICollection<Order>> GetOrders();
        Task<Order> GetOrderToUpdate(int id, bool tracking = false);

        Task<Order> GetOrderById(int id);


        Task<bool> CreateOrder(Order order);

        Task<bool>   UpdateOrder(Order order);

        Task<bool> OrderExists(int id);

        Task<bool> DeleteOrder(Order order);

        Task<bool> Save();


    }
}
