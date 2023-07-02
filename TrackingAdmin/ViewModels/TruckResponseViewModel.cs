namespace TrackingAdmin.ViewModels
{
    public class TruckResponseViewModel
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
    }
}
