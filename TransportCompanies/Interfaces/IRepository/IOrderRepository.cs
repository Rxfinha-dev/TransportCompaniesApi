using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrderToUpdate(int id, bool tracking = false);

        Order GetOrderById(int id);


        bool CreateOrder(Order order);

        bool UpdateOrder(Order order);

        bool OrderExists(int id);

        bool DeleteOrder(Order order);

        bool Save();


    }
}
