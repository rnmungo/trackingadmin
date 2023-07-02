namespace Domain.TrackingAdmin.Models
{
    public class TruckModel : BaseEntity
    {
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public virtual ICollection<RoadMapModel> RoadMaps { get; set; } = new HashSet<RoadMapModel>();
    }
}
