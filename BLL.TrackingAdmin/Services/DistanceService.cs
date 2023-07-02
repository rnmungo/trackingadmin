﻿using AutoMapper;
using Google.OrTools.ConstraintSolver;
using Microsoft.EntityFrameworkCore;
using DAL.TrackingAdmin.Contracts;
using Domain.TrackingAdmin.Entities;
using Domain.TrackingAdmin.Models;
using BLL.TrackingAdmin.Contracts;
using Google.Protobuf.WellKnownTypes;

namespace BLL.TrackingAdmin.Services
{
    public sealed class DistanceService : IDistanceService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Random _random = new Random();

        public DistanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public List<Distance> GetBestRoute(Guid originId, params Guid[] locationIds)
        {
            List<LocationModel> locationModels = _unitOfWork.Locations
                .GetAll(tracking: false)
                .ToList();
            List<DistanceModel> distanceModels = _unitOfWork.Distances
                .GetAll(tracking: false)
                .Include(distance => distance.OriginLocation)
                .Include(distance => distance.DestinationLocation)
                .ToList();

            List<Guid> locations = new List<Guid>(locationIds);
            locations.Insert(0, originId);
            List<int> indexes = locations.Select(d => locationModels.FindIndex(c => c.Id == d)).ToList();

            // Matriz completa de distancias
            int n = locationModels.Count;
            long[][] fullDistanceMatrix = new long[n][];
            for (int i = 0; i < locationModels.Count; i++)
            {
                fullDistanceMatrix[i] = new long[locationModels.Count];
                for (int j = 0; j < locationModels.Count; j++)
                {
                    var distance = distanceModels.FirstOrDefault(d =>
                        (d.OriginLocationId == locationModels[i].Id && d.DestinationLocationId == locationModels[j].Id) ||
                            (d.DestinationLocationId == locationModels[i].Id && d.OriginLocationId == locationModels[j].Id));
                    if (distance != null)
                    {
                        long distanceInKm = (long)distance.DistanceInKm;
                        fullDistanceMatrix[i][j] = distanceInKm != 0 ? distanceInKm : long.MaxValue;
                    }
                    else
                    {
                        fullDistanceMatrix[i][j] = long.MaxValue;
                    }
                }
            }

            // Matriz reducida de distancias
            long[][] smallDistanceMatrix = new long[indexes.Count][];
            for (int i = 0; i < indexes.Count; i++)
            {
                smallDistanceMatrix[i] = new long[indexes.Count];
                for (int j = 0; j < indexes.Count; j++)
                {
                    smallDistanceMatrix[i][j] = fullDistanceMatrix[indexes[i]][indexes[j]];
                }
            }

            // Crea el problema del viajante de comercio.
            RoutingIndexManager manager = new RoutingIndexManager(smallDistanceMatrix.GetLength(0), 1, 0);
            RoutingModel routing = new RoutingModel(manager);

            int transitCallbackIndex = routing.RegisterTransitCallback((long i, long j) => smallDistanceMatrix[manager.IndexToNode(i)][manager.IndexToNode(j)]);

            // Define el costo de cada arco.
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            // Establece las opciones de búsqueda.
            RoutingSearchParameters searchParameters = operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;
            searchParameters.LocalSearchMetaheuristic = LocalSearchMetaheuristic.Types.Value.GuidedLocalSearch;
            searchParameters.TimeLimit = new Duration { Seconds = 1 };

            // Resuelve el problema.
            Assignment solution = routing.SolveWithParameters(searchParameters);

            // Crea la lista de distancias del mejor recorrido
            List<DistanceModel> bestRoute = new List<DistanceModel>();
            if (solution != null)
            {
                long index = routing.Start(0);
                while (!routing.IsEnd(index))
                {
                    long nextIndex = solution.Value(routing.NextVar(index));
                    LocationModel originLocation = locationModels[indexes[manager.IndexToNode((int)index)]];
                    LocationModel destinationLocation = locationModels[indexes[manager.IndexToNode((int)nextIndex)]];
                    DistanceModel distance = distanceModels.First(d => d.OriginLocationId == originLocation.Id && d.DestinationLocationId == destinationLocation.Id);
                    bestRoute.Add(distance);
                    index = nextIndex;
                }
            }

            // Mapea las distancias
            List<Distance> distances = _mapper.Map<List<Distance>>(bestRoute);
            return distances;
        }
    }
}
