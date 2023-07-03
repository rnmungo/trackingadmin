using AutoMapper;
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

            long[][] distanceMatrix = GetDistanceMatrix(distanceModels, locationModels, indexes);

            // Create the traveling salesman problem
            RoutingIndexManager manager = new RoutingIndexManager(distanceMatrix.GetLength(0), 1, 0);
            RoutingModel routing = new RoutingModel(manager);

            int transitCallbackIndex = routing.RegisterTransitCallback((long i, long j) => distanceMatrix[manager.IndexToNode(i)][manager.IndexToNode(j)]);

            // Define the cost of each arc
            routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

            // Set the search parameters
            RoutingSearchParameters searchParameters = operations_research_constraint_solver.DefaultRoutingSearchParameters();
            searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;
            searchParameters.LocalSearchMetaheuristic = LocalSearchMetaheuristic.Types.Value.GuidedLocalSearch;
            searchParameters.TimeLimit = new Duration { Seconds = 1 };

            // Resolve the problem
            Assignment solution = routing.SolveWithParameters(searchParameters);

            // Create the list of distances of the best course
            List<Distance> bestRoute = GetBestRouteBySolution(locationModels, distanceModels, indexes, manager, routing, solution);
            return bestRoute;
        }

        private List<Distance> GetBestRouteBySolution(List<LocationModel> locationModels, List<DistanceModel> distanceModels, List<int> indexes, RoutingIndexManager manager, RoutingModel routing, Assignment solution)
        {
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
            List<Distance> distances = _mapper.Map<List<Distance>>(bestRoute);
            return distances;
        }

        private static long[][] GetDistanceMatrix(List<DistanceModel> distanceModels, List<LocationModel> locationModels, List<int> locationIndexes)
        {
            long[][] fullDistanceMatrix = GetFullDistanceMatrix(distanceModels, locationModels);
            long[][] smallDistanceMatrix = new long[locationIndexes.Count][];
            for (int i = 0; i < locationIndexes.Count; i++)
            {
                smallDistanceMatrix[i] = new long[locationIndexes.Count];
                for (int j = 0; j < locationIndexes.Count; j++)
                {
                    smallDistanceMatrix[i][j] = fullDistanceMatrix[locationIndexes[i]][locationIndexes[j]];
                }
            }
            return smallDistanceMatrix;
        }

        private static long[][] GetFullDistanceMatrix(List<DistanceModel> distanceModels, List<LocationModel> locationModels)
        {
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
            return fullDistanceMatrix;
        }
    }
}
