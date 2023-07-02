using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.TrackingAdmin;
using Domain.TrackingAdmin.Contracts;

namespace DAL.TrackingAdmin
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source=localhost;Initial Catalog=DbTrackingAdmin;User ID=sa;Password=BxCLM8xter343.n;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BaseEntity>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IBaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var body = Expression.Equal(
                        Expression.Property(parameter, "DeletedAt"),
                        Expression.Constant(null, typeof(DateTime?)));
                    var lambda = Expression.Lambda(body, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

        public override int SaveChanges()
        {
            SetDateTimes();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetDateTimes();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetDateTimes()
        {
            SetCreatedAt();
            SetUpdatedAt();
            SetDeletedAt();
        }

        private void SetUpdatedAt()
        {
            var modifiedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Metadata.FindProperty("UpdatedAt") != null &&
                            e.State == EntityState.Modified);

            foreach (var entry in modifiedEntries)
            {
                entry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
            }
        }

        private void SetCreatedAt()
        {
            var addedEntries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in addedEntries)
            {
                DateTime now = DateTime.UtcNow;
                entry.Property("CreatedAt").CurrentValue = now;
                entry.Property("UpdatedAt").CurrentValue = now;
            }
        }

        private void SetDeletedAt()
        {
            var deletedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Metadata.FindProperty("DeletedAt") != null &&
                            e.State == EntityState.Deleted);

            foreach (var entry in deletedEntries)
            {
                entry.Property("DeletedAt").CurrentValue = DateTime.UtcNow;
                entry.State = EntityState.Modified;
            }
        }
    }
}
