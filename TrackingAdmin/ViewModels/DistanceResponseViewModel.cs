namespace TrackingAdmin.ViewModels
{
    public class DistanceResponseViewModel
    {
        public Guid Id { get; set; }
        public decimal DistanceInKm { get; set; }
        public LocationResponseViewModel OriginLocation { get; set; } = null!;
        public LocationResponseViewModel DestinationLocation { get; set; } = null!;
    }
}
