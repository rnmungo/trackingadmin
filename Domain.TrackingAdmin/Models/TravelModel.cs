namespace Domain.TrackingAdmin.Models
{
    public class TravelModel : BaseEntity
    {
        public DateTime? StartDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int OrderNumber { get; set; }
        public Guid DistanceId { get; set; }
        public Guid RoadMapId { get; set; }

        public virtual DistanceModel Distance { get; set; } = null!;
        public virtual RoadMapModel RoadMap { get; set; } = null!;
    }
}
