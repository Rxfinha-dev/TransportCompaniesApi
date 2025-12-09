using DocumentValidator;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

namespace TransportCompanies.Services
{
    public class CostumerService : ICostumerService
    {
        private readonly ICostumerRepository _costumerRepository;
        public CostumerService(ICostumerRepository costumerRepository)
        {
            _costumerRepository = costumerRepository;   
        }

        public async Task<bool> CostumerExistsAsync(int id)
        {
            return await _costumerRepository.CostumerExists(id);
        }

        public async Task<bool> CreateCostumerAsync(Costumer costumer)
        {
            if (await _costumerRepository.CostumerExists(costumer.Id))
                throw new Exception("cliente já existe");

            bool cpfValidation = CpfValidation.Validate(costumer.Cpf);

            if (!cpfValidation)
                throw new Exception("cpf inválido");

            return await _costumerRepository.CreateCostumer(costumer);           
        }

        public async Task<bool> DeleteCostumerAsync(Costumer costumer)
        {
            if (!await _costumerRepository.CostumerExists(costumer.Id))
                throw new Exception("cliente não existe");

            return await _costumerRepository.DeleteCostumer(costumer);
        }

        public async Task<Costumer> GetCostumerAsync(int id)
        {
            if (!await _costumerRepository.CostumerExists(id))
                throw new Exception("cliente não existe");

            return await _costumerRepository.GetCostumer(id);
        }

        public async Task<Costumer> GetCostumerAsync(string cpf)
        {
            if (!await _costumerRepository.CostumerExists(cpf))
                throw new Exception("cliente não existe");

            return await _costumerRepository.GetCostumer(cpf);
        }

     

        public async Task<ICollection<Costumer>> GetCostumersAsync()
        {
            return await _costumerRepository.GetCostumers();
        }

        public async Task<bool> UpdateCostumerAsync(int id,Costumer costumer)
        {
          
            if (!await _costumerRepository.CostumerExists(costumer.Id))
                    throw new Exception("Cliente não cadastrada");
            bool cpfValidation = CpfValidation.Validate(costumer.Cpf);
            if (!cpfValidation)
                throw new Exception("cpf inválido");
            
            return await _costumerRepository.UpdateCostumer(costumer);

        }
    }
}
