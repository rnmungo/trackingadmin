using Domain.TrackingAdmin.Entities;

namespace BLL.TrackingAdmin.Contracts
{
    public interface IDistanceService
    {
        List<Distance> GetBestRoute(Guid originId, params Guid[] locationIds);
    }
}
