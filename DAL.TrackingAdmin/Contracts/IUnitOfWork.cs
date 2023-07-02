using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Domain.TrackingAdmin.Models;

namespace DAL.TrackingAdmin.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        IRepository<LocationModel> Locations { get; }
        IRepository<DistanceModel> Distances { get; }
        IRepository<TruckModel> Trucks { get; }
        IRepository<RoadMapModel> RoadMaps { get; }
        IRepository<TravelModel> Travels { get; }
        IDbContextTransaction BeginTransaction();
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
