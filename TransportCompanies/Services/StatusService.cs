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

        public bool CreateStatus(Status status)
        {
            if (_statusRepository.StatusExists(status.Id))
                throw new Exception("Status já cadastrado");

            return _statusRepository.CreateStatus(status);
        }

        public bool DeleteStatus(Status status)
        {
            if (!_statusRepository.StatusExists(status.Id))
                throw new Exception("Status não cadastrado");

            return _statusRepository.DeleteStatus(status);
        }

        public Status GetStatus(int id)
        {
            if (!_statusRepository.StatusExists(id))
                throw new Exception("Status não cadastrado");

            return _statusRepository.GetStatus(id);
        }

        public ICollection<Status> GetStatuses()
        {
            return _statusRepository.GetStatuses();
        }

        public bool StatusExists(int id)
        {
            return _statusRepository.StatusExists(id);
        }

        public bool UpdateStatus(int id,Status status)
        {
            if (!_statusRepository.StatusExists(status.Id))
                throw new Exception("Status não cadastrado");

            return _statusRepository.UpdateStatus(status);
        }
    }
}
