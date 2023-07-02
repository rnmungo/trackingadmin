namespace TrackingAdmin.ViewModels
{
    public class RoadMapPagedResponseViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public TruckPagedResponseViewModel Truck { get; set; }
        public List<TravelPagedResponseViewModel> Travels { get; set; } = new List<TravelPagedResponseViewModel>();
    }
}
