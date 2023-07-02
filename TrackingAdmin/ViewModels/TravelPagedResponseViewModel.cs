namespace TrackingAdmin.ViewModels
{
    public class TravelPagedResponseViewModel
    {
        public Guid Id { get; set; }
        public DateTime? StartDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int OrderNumber { get; set; }
        public DistancePagedResponseViewModel Distance { get; set; }
    }
}