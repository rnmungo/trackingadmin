using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Domain.TrackingAdmin.Models;
using DAL.TrackingAdmin.Contracts;
using DAL.TrackingAdmin.Exceptions;
using DAL.TrackingAdmin.Repositories;

namespace DAL.TrackingAdmin.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly AppDbContext _context;
        private IRepository<LocationModel> _locationsRepository;
        private IRepository<DistanceModel> _distancesRepository;
        private IRepository<TruckModel> _trucksRepository;
        private IRepository<RoadMapModel> _roadMapsRepository;
        private IRepository<TravelModel> _travelsRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public DbContext Context => _context;

        public IRepository<LocationModel> Locations => _locationsRepository ??= new EntityFrameworkCoreRepository<LocationModel>(_context);

        public IRepository<DistanceModel> Distances => _distancesRepository ??= new EntityFrameworkCoreRepository<DistanceModel>(_context);

        public IRepository<TruckModel> Trucks => _trucksRepository ??= new EntityFrameworkCoreRepository<TruckModel>(_context);

        public IRepository<RoadMapModel> RoadMaps => _roadMapsRepository ??= new EntityFrameworkCoreRepository<RoadMapModel>(_context);

        public IRepository<TravelModel> Travels => _travelsRepository ??= new EntityFrameworkCoreRepository<TravelModel>(_context);

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new EFCoreDataException(ex.Message, ex);
            }
        }
    }
}
