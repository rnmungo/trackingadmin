using Microsoft.EntityFrameworkCore;
using Domain.TrackingAdmin.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.TrackingAdmin.TypeBuilders
{
    public sealed class TravelTypeBuilder : IEntityTypeConfiguration<TravelModel>
    {
        public void Configure(EntityTypeBuilder<TravelModel> builder)
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

            builder.Property(entity => entity.StartDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(entity => entity.Status)
                .IsRequired(true)
                .HasColumnType("varchar(20)");

            builder.Property(entity => entity.OrderNumber)
                .IsRequired(true)
                .HasColumnType("int");

            builder.Property(entity => entity.DistanceId)
                .IsRequired(true)
                .HasColumnType("uniqueidentifier");

            builder.Property(entity => entity.RoadMapId)
                .IsRequired(true)
                .HasColumnType("uniqueidentifier");

            builder.HasOne(entity => entity.Distance)
                .WithMany(relation => relation.Travels)
                .HasForeignKey(entity => entity.DistanceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(entity => entity.RoadMap)
                .WithMany(relation => relation.Travels)
                .HasForeignKey(entity => entity.RoadMapId);

            builder.ToTable("Travels");
        }
    }
}
