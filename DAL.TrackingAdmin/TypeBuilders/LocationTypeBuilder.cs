using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.TrackingAdmin.Models;

namespace DAL.TrackingAdmin.TypeBuilders
{
    public sealed class LocationTypeBuilder : IEntityTypeConfiguration<LocationModel>
    {
        public void Configure(EntityTypeBuilder<LocationModel> builder)
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

            builder.Property(entity => entity.Name)
                .IsRequired(true)
                .HasColumnType("varchar(50)");

            builder.Property(entity => entity.Latitude)
                .IsRequired(true)
                .HasColumnType("float");

            builder.Property(entity => entity.Longitude)
                .IsRequired(true)
                .HasColumnType("float");

            builder.HasIndex(entity => entity.Name).IsUnique();

            builder.ToTable("Locations");
        }
    }
}
