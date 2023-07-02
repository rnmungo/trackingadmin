namespace Domain.TrackingAdmin.Entities
{
    public class Distance : BaseEntity
    {
        public Guid OriginLocationId { get; set; }
        public Guid DestinationLocationId { get; set; }
        public decimal DistanceInKm { get; set; }

        public Location OriginLocation { get; set; } = null!;
        public Location DestinationLocation { get; set; } = null!;
        public List<Travel> Travels { get; set; } = new List<Travel>();
    }
}
