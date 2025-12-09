using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface ITransportCompanyService
    {
        Task<ICollection<TransportCompany>> GetTransportCompanies();

        Task<TransportCompany> GetTransportCompany(int id);

        Task<bool> CreateTransportCompany(TransportCompany company);

        Task<bool> UpdateTransportCompany(int id, TransportCompany company);

        Task<bool> DeleteTransportCompany(TransportCompany company);

        Task<bool> TransportCompanyExists(int id);
    }
}
