using Domain.TrackingAdmin.Enums;

namespace Domain.TrackingAdmin.Models
{
    public class RoadMapModel : BaseEntity
    {
        public int Number { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid TruckId { get; set; }

        public virtual TruckModel Truck { get; set; } = null!;
        public virtual ICollection<TravelModel> Travels { get; set; } = new HashSet<TravelModel>();
    }
}
