namespace TrackingAdmin.ViewModels
{
    public class DistanceRequestViewModel
    {
        public Guid OriginId { get; set; }
        public Guid[] DestinationIds { get; set; }
    }
}
