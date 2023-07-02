using Domain.TrackingAdmin.Entities;

namespace BLL.TrackingAdmin.Contracts
{
    public interface ITruckService : IService<Truck>
    {
        List<Truck> GetAllowedTrucks();
    }
}
