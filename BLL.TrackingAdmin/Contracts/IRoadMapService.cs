using Domain.TrackingAdmin.Entities;

namespace BLL.TrackingAdmin.Contracts
{
    public interface IRoadMapService : IService<RoadMap>
    {
        Paged<RoadMap> Search(int size, int currentPage, string license, string status);
        void MoveForward(Guid id);
    }
}
