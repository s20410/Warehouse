using WarehouseABPD.Models;

namespace WarehouseABPD.Services;

    public abstract class IWarehouseDbService2
    {
        public abstract Task<int> PostWarehouse(Warehouse warehouse);
    }

