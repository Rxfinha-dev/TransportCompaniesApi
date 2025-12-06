using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface IOrderService
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int id);

        Task<bool> IsCepValid(string cpf);
     


        Task<bool> CreateOrder(Order order);

        bool UpdateClientOrder(int id, Order order);
        bool UpdateOrderItens(int id, Order order);
  

        bool OrderExists(int id);

        bool DeleteOrder(Order order);
        bool UpdateStatus(int id, Order order);
        Task<bool> UpdateAddresses(int id, UpdateAdressDto addressToUpdate);

    }
}
