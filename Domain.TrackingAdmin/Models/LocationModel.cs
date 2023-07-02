namespace Domain.TrackingAdmin.Models
{
    public class LocationModel : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public virtual ICollection<DistanceModel> OriginDistances { get; set; } = new HashSet<DistanceModel>();
        public virtual ICollection<DistanceModel> DestinationDistances { get; set; } = new HashSet<DistanceModel>();
    }
}
