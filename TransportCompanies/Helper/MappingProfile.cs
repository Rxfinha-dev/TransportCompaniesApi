using AutoMapper;
using TransportCompanies.DTO;
using TransportCompanies.Models;

namespace TransportCompanies.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<Costumer, CostumerDto>();
            CreateMap<CostumerDto, Costumer>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Status, StatusDto>();
            CreateMap<StatusDto, Status>();
            CreateMap<TransportCompany, TransportCompanyDto>();
            CreateMap<TransportCompanyDto, TransportCompany>();
            CreateMap<Tracking, TrackingDto>();
            CreateMap<CreateTrackingEventDto, Tracking>();
        }
    }
}
