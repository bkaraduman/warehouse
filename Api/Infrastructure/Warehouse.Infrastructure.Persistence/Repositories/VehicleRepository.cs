using Warehouse.Api.Domain.Models;
using Warehouse.Infrastructure.Persistence.Context;
using Warehouse.Infrastructure.Persistence.Repositories.Interface;

namespace Warehouse.Infrastructure.Persistence.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(WarehouseContext context) : base(context)
        {
        }
    }
}
