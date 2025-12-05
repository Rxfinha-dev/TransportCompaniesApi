using TransportCompanies.Models;

namespace TransportCompanies.Interfaces.IRepository
{
    public interface ITransportCompanyRepository
    {
        ICollection<TransportCompany> GetTransportCompanies();

        TransportCompany GetTransportCompany(int id);

        bool CreateTransportCompany(TransportCompany company);

        bool UpdateTransportCompany(TransportCompany company);

        bool DeleteTransportCompany(TransportCompany company);
        bool TransportCompanyExists(int id);

        bool Save();
    }
}
