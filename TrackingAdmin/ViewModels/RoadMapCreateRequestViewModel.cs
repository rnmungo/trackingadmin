namespace TrackingAdmin.ViewModels
{
    public class RoadMapCreateRequestViewModel
    {
        public Guid TruckId { get; set; }
        public List<TravelCreateRequestViewModel> Travels { get; set; }
    }
}
