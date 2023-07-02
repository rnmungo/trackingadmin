using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.TrackingAdmin.Models;

namespace DAL.TrackingAdmin.TypeBuilders
{
    public sealed class DistanceTypeBuilder : IEntityTypeConfiguration<DistanceModel>
    {
        public void Configure(EntityTypeBuilder<DistanceModel> builder)
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

            builder.Property(entity => entity.OriginLocationId)
                .IsRequired(true)
                .HasColumnType("uniqueidentifier");

            builder.Property(entity => entity.DestinationLocationId)
                .IsRequired(true)
                .HasColumnType("uniqueidentifier");

            builder.Property(entity => entity.DistanceInKm)
                .IsRequired(true)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(entity => entity.OriginLocation)
                .WithMany(relation => relation.OriginDistances)
                .HasForeignKey(entity => entity.OriginLocationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entity => entity.DestinationLocation)
                .WithMany(relation => relation.DestinationDistances)
                .HasForeignKey(entity => entity.DestinationLocationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(entity => entity.Travels)
                .WithOne(relation => relation.Distance)
                .HasForeignKey(relation => relation.DistanceId);

            builder.HasIndex(entity => new { entity.OriginLocationId, entity.DestinationLocationId }).IsUnique();

            builder.ToTable("Distances");
        }
    }
}
