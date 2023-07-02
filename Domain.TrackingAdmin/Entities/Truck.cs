namespace Domain.TrackingAdmin.Entities
{
    public class Truck : BaseEntity
    {
        public string LicensePlate { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public List<RoadMap> RoadMaps { get; set; } = new List<RoadMap>();
    }
}
