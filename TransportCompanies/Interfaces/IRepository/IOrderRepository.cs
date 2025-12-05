using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int id);


        bool CreateOrder(Order order);

        bool UpdateOrder(Order order);

        bool OrderExists(int id);

        bool DeleteOrder(Order order);

        bool Save();


    }
}
