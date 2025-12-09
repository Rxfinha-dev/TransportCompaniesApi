using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface ICostumerService
    {
        Task<ICollection<Costumer>> GetCostumersAsync();
        Task<Costumer> GetCostumerAsync(int id);
        Task<Costumer> GetCostumerAsync(string cpf);

        Task<bool> CreateCostumerAsync(Costumer costumer);
        Task<bool> UpdateCostumerAsync(int id, Costumer costumer);
        Task<bool> DeleteCostumerAsync(Costumer costumer);
        Task<bool> CostumerExistsAsync(int id);

       
    }
}
