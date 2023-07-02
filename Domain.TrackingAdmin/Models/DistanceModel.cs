namespace Domain.TrackingAdmin.Models
{
    public class DistanceModel : BaseEntity
    {
        public Guid OriginLocationId { get; set; }
        public Guid DestinationLocationId { get; set; }
        public decimal DistanceInKm { get; set; }

        public virtual LocationModel OriginLocation { get; set; } = null!;
        public virtual LocationModel DestinationLocation { get; set; } = null!;
        public virtual ICollection<TravelModel> Travels { get; set; } = new HashSet<TravelModel>();
    }
}
