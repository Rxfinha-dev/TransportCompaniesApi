using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface ICostumerService
    {
        ICollection<Costumer> GetCostumers();
        Costumer GetCostumer(int id);
        Costumer GetCostumer(string cpf);

        bool CreateCostumer(Costumer costumer);
        bool UpdateCostumer(int id, Costumer costumer);
        bool DeleteCostumer(Costumer costumer);
        bool CostumerExists(int id);

       
    }
}
