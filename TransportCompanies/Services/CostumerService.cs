using DocumentValidator;
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

        public bool CostumerExists(int id)
        {
            return _costumerRepository.CostumerExists(id);
        }

        public bool CreateCostumer(Costumer costumer)
        {
            if (_costumerRepository.CostumerExists(costumer.Id))
                throw new Exception("cliente já existe");

            bool cpfValidation = CpfValidation.Validate(costumer.Cpf);

            if (!cpfValidation)
                throw new Exception("cpf inválido");

            return _costumerRepository.CreateCostumer(costumer);           
        }

        public bool DeleteCostumer(Costumer costumer)
        {
            if (!_costumerRepository.CostumerExists(costumer.Id))
                throw new Exception("cliente não existe");

            return _costumerRepository.DeleteCostumer(costumer);
        }

        public Costumer GetCostumer(int id)
        {
            if (!_costumerRepository.CostumerExists(id))
                throw new Exception("cliente não existe");

            return _costumerRepository.GetCostumer(id);
        }

        public Costumer GetCostumer(string cpf)
        {
            if (!_costumerRepository.CostumerExists(cpf))
                throw new Exception("cliente não existe");

            return _costumerRepository.GetCostumer(cpf);
        }

     

        public ICollection<Costumer> GetCostumers()
        {
            return _costumerRepository.GetCostumers();
        }

        public bool UpdateCostumer(Costumer costumer)
        {
            if (!_costumerRepository.CostumerExists(costumer.Id))
                throw new Exception("cliente não existe");

            bool cpfValidation = CpfValidation.Validate(costumer.Cpf);

            if(!cpfValidation)
                throw new Exception("cpf inválido");

            return _costumerRepository.UpdateCostumer(costumer);

        }
    }
}
