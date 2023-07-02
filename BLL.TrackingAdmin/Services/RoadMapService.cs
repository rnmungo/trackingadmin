using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DAL.TrackingAdmin.Contracts;
using BLL.TrackingAdmin.Contracts;
using BLL.TrackingAdmin.Exceptions;
using BLL.TrackingAdmin.Extensions;
using Domain.TrackingAdmin.Entities;
using Domain.TrackingAdmin.Enums;
using Domain.TrackingAdmin.Models;

namespace BLL.TrackingAdmin.Services
{
    public sealed class RoadMapService : IRoadMapService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RoadMapService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void Create(RoadMap entity)
        {
            ValidateTruck(entity.TruckId);

            List<TravelModel> travelModels = new List<TravelModel>();
            int orderNr = 1;
            foreach (var travel in entity.Travels)
            {
                TravelModel travelModel = _mapper.Map<TravelModel>(travel);
                travelModel.Status = TravelStatusEnum.Created.ToString();
                travelModel.OrderNumber = orderNr++;
                travelModels.Add(travelModel);
            }

            entity.Travels = new List<Travel>();
            RoadMapModel roadMapModel = _mapper.Map<RoadMapModel>(entity);
            roadMapModel.Status = RoadMapStatusEnum.Created.ToString();
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _unitOfWork.RoadMaps.Create(roadMapModel);
                    _unitOfWork.SaveChanges();
                    foreach (var travelModel in travelModels)
                    {
                        travelModel.RoadMapId = roadMapModel.Id;
                        _unitOfWork.Travels.Create(travelModel);
                    }
                    _unitOfWork.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Delete(Guid id)
        {
            var roadMapModel = _unitOfWork.RoadMaps.GetById(id);
            _unitOfWork.RoadMaps.Delete(roadMapModel);
            _unitOfWork.SaveChanges();
        }

        public List<RoadMap> GetAll()
        {
            var roadMapModels = _unitOfWork.RoadMaps.GetAll(tracking: false).ToList();
            List<RoadMap> roadMaps = _mapper.Map<List<RoadMap>>(roadMapModels);
            return roadMaps;
        }

        public RoadMap GetById(Guid id)
        {
            RoadMapModel roadMapModel = _unitOfWork.RoadMaps.GetByCondition(roadMap => roadMap.Id == id, tracking: false)
                .Include(roadMap => roadMap.Truck)
                .Include(roadMap => roadMap.Travels)
                    .ThenInclude(travel => travel.Distance)
                        .ThenInclude(distance => distance.OriginLocation)
                .Include(roadMap => roadMap.Travels)
                    .ThenInclude(travel => travel.Distance)
                        .ThenInclude(distance => distance.DestinationLocation)
                .FirstOrDefault()!;
            RoadMap roadMap = _mapper.Map<RoadMap>(roadMapModel);
            return roadMap;
        }

        public void MoveForward(Guid id)
        {
            RoadMapModel roadMapModel = _unitOfWork.RoadMaps.GetById(id);
            if (roadMapModel.Status == RoadMapStatusEnum.Finished.ToString())
            {
                throw new BusinessException(BusinessCodeEnum.CODE_001, "La ruta ya ha finalizado");
            }

            ValidateTruck(roadMapModel.TruckId, roadMapModel.Id);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    TravelModel inProcessTravelModel = _unitOfWork.Travels
                        .GetByCondition(travel => travel.RoadMapId == id && travel.Status == TravelStatusEnum.InProgress.ToString())
                        .FirstOrDefault();
                    if (inProcessTravelModel != null)
                    {
                        inProcessTravelModel.Status = TravelStatusEnum.Finished.ToString();
                        _unitOfWork.Travels.Update(inProcessTravelModel);
                        _unitOfWork.SaveChanges();
                        // Search if there is next travel
                        TravelModel nextTravelModel = _unitOfWork.Travels
                           .GetByCondition(travel => travel.RoadMapId == id && travel.OrderNumber == inProcessTravelModel.OrderNumber + 1)
                           .FirstOrDefault();
                        if (nextTravelModel != null)
                        {
                            nextTravelModel.Status = TravelStatusEnum.InProgress.ToString();
                            nextTravelModel.StartDate = DateTime.UtcNow;
                            _unitOfWork.Travels.Update(nextTravelModel);
                            _unitOfWork.SaveChanges();
                            transaction.Commit();
                        }
                        else
                        {
                            // If there is no next travel, the road map is finished
                            roadMapModel.Status = RoadMapStatusEnum.Finished.ToString();
                            _unitOfWork.RoadMaps.Update(roadMapModel);
                            _unitOfWork.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    else
                    {
                        TravelModel createdTravelModel = _unitOfWork.Travels
                            .GetByCondition(travel => travel.RoadMapId == id && travel.Status == TravelStatusEnum.Created.ToString())
                            .OrderBy(travel => travel.OrderNumber)
                            .FirstOrDefault();
                        if (createdTravelModel != null)
                        {
                            createdTravelModel.Status = TravelStatusEnum.InProgress.ToString();
                            createdTravelModel.StartDate = DateTime.UtcNow;
                            _unitOfWork.Travels.Update(createdTravelModel);
                            _unitOfWork.SaveChanges();

                            roadMapModel.Status = RoadMapStatusEnum.InProgress.ToString();
                            _unitOfWork.RoadMaps.Update(roadMapModel);
                            _unitOfWork.SaveChanges();
                            transaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Paged<RoadMap> Search(int size, int currentPage, string license, string status)
        {
            var query = _unitOfWork.RoadMaps.GetAll(tracking: false)
                .Include(roadMap => roadMap.Truck)
                .Include(roadMap => roadMap.Travels.OrderBy(travel => travel.OrderNumber))
                    .ThenInclude(travel => travel.Distance)
                        .ThenInclude(distance => distance.OriginLocation)
                .Include(roadMap => roadMap.Travels)
                    .ThenInclude(travel => travel.Distance)
                        .ThenInclude(distance => distance.DestinationLocation)
                .AsQueryable();
            if (!license.IsNullOrEmpty())
            {
                query = query.Where(roadMap => roadMap.Truck.LicensePlate == license);
            }
            if (!status.IsNullOrEmpty())
            {
                query = query.Where(roadMap => roadMap.Status == status);
            }

            var orderedQuery = query.OrderByDescending(roadMap => roadMap.CreatedAt).ThenBy(roadMap => roadMap.Number);
            List<RoadMapModel> pagedElements = orderedQuery.Skip((currentPage - 1) * size).Take(size).ToList();
            List<RoadMapModel> totalElements = orderedQuery.ToList();
            List<RoadMap> roadMaps = _mapper.Map<List<RoadMap>>(pagedElements);
            return new Paged<RoadMap>()
            {
                Results = roadMaps,
                CurrentPage = currentPage,
                SizeLimit = size,
                Total = totalElements.Count,
            };
        }

        public void Update(RoadMap entity)
        {
            throw new NotImplementedException();
        }

        private void ValidateTruck(Guid truckId, Guid roadMapId = new Guid())
        {
            var truckModel = _unitOfWork.Trucks.GetById(truckId);
            if (truckModel is null)
            {
                throw new BusinessException(BusinessCodeEnum.CODE_002, "El camión no existe");
            }

            string inProcessStatus = RoadMapStatusEnum.InProgress.ToString();
            var inDriveTruckQuery = _unitOfWork.RoadMaps
                .GetByCondition(roadMap => roadMap.Status.Equals(inProcessStatus));
            if (roadMapId != Guid.Empty)
            {
                inDriveTruckQuery = inDriveTruckQuery.Where(roadMap => roadMap.Id != roadMapId);
            }
            List<Guid> inDriveTruckIds = inDriveTruckQuery.Select(roadMap => roadMap.TruckId).ToList();
            if (inDriveTruckIds.Contains(truckId))
            {
                throw new BusinessException(BusinessCodeEnum.CODE_003, "El camión ya se encuentra en ruta");
            }
        }
    }
}
