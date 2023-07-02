using Domain.TrackingAdmin.Enums;

namespace Domain.TrackingAdmin.Entities
{
    public class Travel : BaseEntity
    {
        public DateTime? StartDate { get; set; }
        public TravelStatusEnum Status { get; set; } = TravelStatusEnum.Created;
        public int OrderNumber { get; set; }
        public Guid DistanceId { get; set; }
        public Guid RoadMapId { get; set; }

        public Distance Distance { get; set; } = null!;
        public RoadMap RoadMap { get; set; } = null!;
    }
}
