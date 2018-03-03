using System.Collections.Generic;
using System.Threading.Tasks;
using dashboard.Core.Models;

namespace dashboard.Core
{
    public interface IVehicleRepository
    {
        Task<QueryResult<Vehicle>> GetVehicleCount();
        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj);
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);

        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
    }
}