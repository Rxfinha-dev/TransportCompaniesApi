using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;

namespace TransportCompanies.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<bool> CreateStatus(Status status)
        {
            if (await _statusRepository.StatusExists(status.Id))
                throw new Exception("Status já cadastrado");

            return await _statusRepository.CreateStatus(status);
        }

        public async Task<bool> DeleteStatus(Status status)
        {
            if (! await _statusRepository.StatusExists(status.Id))
                throw new Exception("Status não cadastrado");

            return await _statusRepository.DeleteStatus(status);
        }

        public async Task<Status> GetStatus(int id)
        {
            if (!await _statusRepository.StatusExists(id))
                throw new Exception("Status não cadastrado");

            return await _statusRepository.GetStatus(id);
        }

        public async Task<ICollection<Status>> GetStatuses()
        {
            return await _statusRepository.GetStatuses();
        }

        public async Task<bool> StatusExists(int id)
        {
            return await _statusRepository.StatusExists(id);
        }

        public async Task<bool> UpdateStatus(int id,Status status)
        {
            if (!await _statusRepository.StatusExists(status.Id))
                throw new Exception("Status não cadastrado");

            return await _statusRepository.UpdateStatus(status);
        }
    }
}
