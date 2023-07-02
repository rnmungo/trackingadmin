using AutoMapper;
using BLL.TrackingAdmin.Extensions;
using Domain.TrackingAdmin.Entities;
using Domain.TrackingAdmin.Enums;
using Domain.TrackingAdmin.Models;

namespace BLL.TrackingAdmin.Mappers
{
    public sealed class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Location, LocationModel>()
                .ReverseMap();
            CreateMap<Distance, DistanceModel>()
                .ReverseMap();
            CreateMap<Truck, TruckModel>()
                .ReverseMap();
            CreateMap<RoadMap, RoadMapModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnum<RoadMapStatusEnum>()));
            CreateMap<Travel, TravelModel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnum<TravelStatusEnum>()));
        }
    }
}
