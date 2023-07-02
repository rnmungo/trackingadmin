namespace TrackingAdmin.ViewModels
{
    public class DistancePagedResponseViewModel
    {
        public Guid Id { get; set; }
        public decimal DistanceInKm { get; set; }
        public LocationPagedResponseViewModel OriginLocation { get; set; }
        public LocationPagedResponseViewModel DestinationLocation { get; set; }
    }
}