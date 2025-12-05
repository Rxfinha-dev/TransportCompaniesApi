using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface ICostumerRepository
    {
        ICollection<Costumer> GetCostumers();
        Costumer GetCostumer(int id);
        Costumer GetCostumer(string cpf);

        bool CreateCostumer(Costumer costumer);
        bool UpdateCostumer(Costumer costumer);
        bool DeleteCostumer(Costumer costumer);
        bool CostumerExists(int id);
        bool CostumerExists(string cpf);
        bool Save();

    }
}
