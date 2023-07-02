using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.TrackingAdmin.Models;

namespace DAL.TrackingAdmin.TypeBuilders
{
    public sealed class TruckTypeBuilder : IEntityTypeConfiguration<TruckModel>
    {
        public void Configure(EntityTypeBuilder<TruckModel> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .IsRequired(true)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("newid()");

            builder.Property(entity => entity.CreatedAt)
                .IsRequired(true)
                .HasColumnType("datetime2")
                .HasDefaultValueSql("getdate()");

            builder.Property(entity => entity.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(entity => entity.DeletedAt)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(entity => entity.LicensePlate)
                .IsRequired(true)
                .HasColumnType("varchar(10)");

            builder.Property(entity => entity.Model)
                .IsRequired(true)
                .HasColumnType("varchar(50)");

            builder.HasMany(entity => entity.RoadMaps)
                .WithOne(relation => relation.Truck)
                .HasForeignKey(relation => relation.TruckId);

            builder.HasIndex(entity => entity.LicensePlate).IsUnique();

            builder.ToTable("Trucks");
        }
    }
}
