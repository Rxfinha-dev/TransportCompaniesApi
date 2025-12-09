using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface IOrderService
    {
        Task<ICollection<Order>> GetOrdersAsync();
        Task<Order> GetOrderAsync(int id);

        Task<bool> IsCepValid(string cpf);
     


        Task<bool> CreateOrder(Order order);

        Task<bool> UpdateClientOrderAsync(int id, Order order);
        Task<bool> UpdateOrderItensAsync(int id, Order order);
  

        Task<bool> OrderExistsAsync(int id);

        Task<bool> DeleteOrderAsync(int id);
        Task<bool> UpdateStatusAsync(int id, Order order);
        Task<bool> UpdateAddresses(int id, UpdateAdressDto addressToUpdate);

    }
}
