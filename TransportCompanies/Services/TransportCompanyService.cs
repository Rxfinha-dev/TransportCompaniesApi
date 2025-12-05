using Microsoft.AspNetCore.Http.HttpResults;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TransportCompanies.Services
{
    public class TransportCompanyService : ITransportCompanyService
    {
        private readonly ITransportCompanyRepository _transportCompanyRepository;
        public TransportCompanyService(ITransportCompanyRepository transportCompanyRepository)
        {
            _transportCompanyRepository = transportCompanyRepository;
        }
        public bool CreateTransportCompany(TransportCompany company)
        {
            if (_transportCompanyRepository.TransportCompanyExists(company.Id))
                throw new Exception("Transportadora já cadastrada");

            return _transportCompanyRepository.CreateTransportCompany(company);
        }

        public bool DeleteTransportCompany(TransportCompany company)
        {
            if (!_transportCompanyRepository.TransportCompanyExists(company.Id))
                throw new Exception("Transportadora não cadastrada");

            return _transportCompanyRepository.DeleteTransportCompany(company);

        }

        public ICollection<TransportCompany> GetTransportCompanies()
        {
            return _transportCompanyRepository.GetTransportCompanies();
        }

        public TransportCompany GetTransportCompany(int id)
        {
            if (!_transportCompanyRepository.TransportCompanyExists(id))
                throw new Exception("Transportadora não cadastrada");

            return _transportCompanyRepository.GetTransportCompany(id);
        }

        public bool TransportCompanyExists(int id)
        {
            return _transportCompanyRepository.TransportCompanyExists(id);  
        }

        public bool UpdateTransportCompany(int id, TransportCompany company)
        {
            if (!_transportCompanyRepository.TransportCompanyExists(company.Id))
                throw new Exception("Transportadora não cadastrada");

            return _transportCompanyRepository.UpdateTransportCompany(company);
        }
    }
}
