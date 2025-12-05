using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface IOrderService
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int id);

        Task<bool> IsOriginCepValid(string cpf);
       Task<bool> IsDestinationCepValid(string cpf);


        bool CreateOrder(Order order);

        bool UpdateClientOrder(int id, Order order);
        bool UpdateOrderItens(int id, Order order);
  

        bool OrderExists(int id);

        bool DeleteOrder(Order order);
        bool UpdateStatus(int id, Order order);
        bool UpdateAddresses(int id, AddressDto origin, AddressDto destination);

    }
}
