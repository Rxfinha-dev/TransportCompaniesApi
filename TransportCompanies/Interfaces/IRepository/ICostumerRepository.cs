using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface ICostumerRepository
    {
        Task<ICollection<Costumer>> GetCostumers();
        Task<Costumer> GetCostumer(int id);
        Task<Costumer> GetCostumer(string cpf);

        Task<bool> CreateCostumer(Costumer costumer);
        Task<bool> UpdateCostumer(Costumer costumer);
        Task<bool> DeleteCostumer(Costumer costumer);
        Task<bool> CostumerExists(int id);
        Task<bool> CostumerExists(string cpf);
        Task<bool> Save();

    }
}
