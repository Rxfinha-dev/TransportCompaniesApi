using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface ITransportCompanyRepository
    {
        Task<ICollection<TransportCompany>> GetTransportCompanies();

        Task<TransportCompany> GetTransportCompany(int id);

        Task<bool> CreateTransportCompany(TransportCompany company);

        Task<bool> UpdateTransportCompany(TransportCompany company);

        Task<bool> DeleteTransportCompany(TransportCompany company);
        Task<bool> TransportCompanyExists(int id);

        Task<bool> Save();
    }
}
