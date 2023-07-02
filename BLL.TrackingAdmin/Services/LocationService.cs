using AutoMapper;
using DAL.TrackingAdmin.Contracts;
using Domain.TrackingAdmin.Entities;
using BLL.TrackingAdmin.Contracts;

namespace BLL.TrackingAdmin.Services
{
    public sealed class LocationService : ILocationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void Create(Location entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Location> GetAll()
        {
            var locationModels = _unitOfWork.Locations.GetAll(tracking: false).ToList();
            List<Location> locations = _mapper.Map<List<Location>>(locationModels);
            return locations;
        }

        public Location GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Location entity)
        {
            throw new NotImplementedException();
        }
    }
}
