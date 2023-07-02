using AutoMapper;
using BLL.TrackingAdmin.Extensions;
using Domain.TrackingAdmin.Entities;
using Domain.TrackingAdmin.Enums;
using TrackingAdmin.ViewModels;

namespace TrackingAdmin.Mappers
{
    public sealed class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<TruckResponseViewModel, Truck>()
                .ReverseMap();
            CreateMap<TruckCreateRequestViewModel, Truck>()
                .ReverseMap();
            CreateMap<DistanceResponseViewModel, Distance>()
                .ReverseMap();
            CreateMap<LocationResponseViewModel, Location>()
                .ReverseMap();
            CreateMap<TravelCreateRequestViewModel, Travel>()
                .ReverseMap();
            CreateMap<RoadMapCreateRequestViewModel, RoadMap>()
                .ReverseMap();
            CreateMap<LocationPagedResponseViewModel, Location>()
                .ReverseMap();
            CreateMap<DistancePagedResponseViewModel, Distance>()
                .ReverseMap();
            CreateMap<TravelPagedResponseViewModel, Travel>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnum<TravelStatusEnum>()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<RoadMapPagedResponseViewModel, RoadMap>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToEnum<RoadMapStatusEnum>()))
                .ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<TruckPagedResponseViewModel, Truck>()
                .ReverseMap();
        }
    }
}
