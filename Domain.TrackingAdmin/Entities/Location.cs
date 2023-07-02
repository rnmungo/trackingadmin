namespace Domain.TrackingAdmin.Entities
{
    public class Location : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public List<Distance> OriginDistances { get; set; } = new List<Distance>();
        public List<Distance> DestinationDistances { get; set; } = new List<Distance>();
    }
}
