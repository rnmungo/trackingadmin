using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.TrackingAdmin.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DAL.TrackingAdmin.TypeBuilders
{
    public sealed class RoadMapTypeBuilder : IEntityTypeConfiguration<RoadMapModel>
    {
        public void Configure(EntityTypeBuilder<RoadMapModel> builder)
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

            builder.Property(entity => entity.Number)
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Property(entity => entity.Status)
                .IsRequired(true)
                .HasColumnType("varchar(20)");

            builder.HasMany(entity => entity.Travels)
                .WithOne(relation => relation.RoadMap)
                .HasForeignKey(relation => relation.RoadMapId);

            builder.ToTable("RoadMaps");
        }
    }
}
