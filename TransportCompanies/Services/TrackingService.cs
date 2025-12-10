using AutoMapper;
using TransportCompanies.DTO;
using TransportCompanies.Interfaces.IRepository;
using TransportCompanies.Interfaces.IServices;
using TransportCompanies.Models;


namespace TransportCompanies.Services
{
    public class TrackingService : ITrackingService
    {
        private readonly ITrackingRepository _trackingRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public TrackingService(ITrackingRepository trackingRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _trackingRepository = trackingRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task AddTrackingEventAsync(int orderId, CreateTrackingEventDto dto)
        {
            if (!await _orderRepository.OrderExists(orderId))
                throw new KeyNotFoundException("Order not found");


            var trackingEvent = new Tracking
            {
                OrderId = orderId,
                StatusId = dto.StatusId,
                Message = dto.Message,
                Location = dto.Location
            };


            await _trackingRepository.AddEventAsync(trackingEvent);
            await _trackingRepository.SaveAsync();
        }

        public async Task<TrackingDto?> GetLatestEventAsync(int orderId)
        {
            var trackingEvent = await _trackingRepository.GetLatestEventAsync(orderId);
            return trackingEvent == null ? null : _mapper.Map<TrackingDto>(trackingEvent);
        }

        public async Task<ICollection<TrackingDto>> GetTrackingHistoryAsync(int orderId)
        {
            var events = await _trackingRepository.GetEventsByOrderAsync(orderId);
            return events.Select(e => _mapper.Map<TrackingDto>(e)).ToList();
        }
    }
}
