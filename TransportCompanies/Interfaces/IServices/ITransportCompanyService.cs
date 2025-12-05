using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IServices
{
    public interface ITransportCompanyService
    {
        ICollection<TransportCompany> GetTransportCompanies();

        TransportCompany GetTransportCompany(int id);

        bool CreateTransportCompany(TransportCompany company);

        bool UpdateTransportCompany(int id, TransportCompany company);

        bool DeleteTransportCompany(TransportCompany company);

        bool TransportCompanyExists(int id);
    }
}
