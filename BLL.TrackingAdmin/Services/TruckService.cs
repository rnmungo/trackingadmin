using AutoMapper;
using DAL.TrackingAdmin.Contracts;
using Domain.TrackingAdmin.Entities;
using Domain.TrackingAdmin.Models;
using BLL.TrackingAdmin.Contracts;
using Domain.TrackingAdmin.Enums;

namespace BLL.TrackingAdmin.Services
{
    public sealed class TruckService : ITruckService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TruckService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void Create(Truck entity)
        {
            TruckModel truckModel = _mapper.Map<TruckModel>(entity);
            _unitOfWork.Trucks.Create(truckModel);
            _unitOfWork.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var truckModel = _unitOfWork.Trucks.GetById(id);
            _unitOfWork.Trucks.Delete(truckModel);
            _unitOfWork.SaveChanges();
        }

        public List<Truck> GetAll()
        {
            var truckModels = _unitOfWork.Trucks.GetAll(tracking: false).ToList();
            List<Truck> trucks = _mapper.Map<List<Truck>>(truckModels);
            return trucks;
        }

        public List<Truck> GetAllowedTrucks()
        {
            string inProcessStatus = RoadMapStatusEnum.InProgress.ToString();
            var inDriveTruckIds = _unitOfWork.RoadMaps
                .GetByCondition(roadMap => roadMap.Status.Equals(inProcessStatus))
                .Select(roadMap => roadMap.TruckId).ToList();
            List<TruckModel> truckModels = _unitOfWork.Trucks
                .GetByCondition(truck => !inDriveTruckIds.Contains(truck.Id))
                .ToList();
            List<Truck> trucks = _mapper.Map<List<Truck>>(truckModels);
            return trucks;
        }

        public Truck GetById(Guid id)
        {
            var truckModel = _unitOfWork.Trucks.GetById(id);
            Truck truck = _mapper.Map<Truck>(truckModel);
            return truck;
        }

        public void Update(Truck entity)
        {
            var truckModel = _unitOfWork.Trucks.GetById(entity.Id);
            _mapper.Map(entity, truckModel);
            _unitOfWork.Trucks.Update(truckModel);
            _unitOfWork.SaveChanges();
        }
    }
}
