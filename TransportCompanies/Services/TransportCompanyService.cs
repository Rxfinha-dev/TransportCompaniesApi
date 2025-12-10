using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        
        
        public async Task<bool> CreateTransportCompany(TransportCompany company)
        {
            if (await _transportCompanyRepository.TransportCompanyExists(company.Id))
                throw new Exception("Transportadora já cadastrada");


                return await _transportCompanyRepository.CreateTransportCompany(company);               
          
        }

        public async Task<bool> DeleteTransportCompany(TransportCompany company)
        {
            if (! await _transportCompanyRepository.TransportCompanyExists(company.Id))
                throw new Exception("Transportadora não cadastrada");

            return await _transportCompanyRepository.DeleteTransportCompany(company);

        }

        public async Task<ICollection<TransportCompany>> GetTransportCompanies()
        {
            return await _transportCompanyRepository.GetTransportCompanies();
        }

        public async Task<TransportCompany> GetTransportCompany(int id)
        {
            if (!await _transportCompanyRepository.TransportCompanyExists(id))
                throw new Exception("Transportadora não cadastrada");

            return await _transportCompanyRepository.GetTransportCompany(id);
        }

        public async Task<bool> TransportCompanyExists(int id)
        {
            return await _transportCompanyRepository.TransportCompanyExists(id);  
        }

        public async Task<bool> UpdateTransportCompany(int id, TransportCompany company)
        {
            if (!await _transportCompanyRepository.TransportCompanyExists(company.Id))
                throw new Exception("Transportadora não cadastrada");

            return await _transportCompanyRepository.UpdateTransportCompany(company);
        }
    }
}
