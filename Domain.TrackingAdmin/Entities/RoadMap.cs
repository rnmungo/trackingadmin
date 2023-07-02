using Domain.TrackingAdmin.Enums;

namespace Domain.TrackingAdmin.Entities
{
    public class RoadMap : BaseEntity
    {
        public int Number { get; set; }
        public RoadMapStatusEnum Status { get; set; } = RoadMapStatusEnum.Created;
        public Guid TruckId { get; set; }

        public Truck Truck { get; set; } = null!;
        public List<Travel> Travels { get; set; } = new List<Travel>();
    }
}
